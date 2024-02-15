using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.SignalR.Management;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using ToggleOn.Query.Client;
using ToggleOn.Client.Contract;
using ToggleOn.Query.Contract.FeatureToggle;
using ToggleOn.Query.Contract.FeatureGroup;

namespace ToggleOn.Functions;

public class SignalRFunctions
{
    private readonly IHubContextStore _hubContextStore;
    private readonly IToggleOnQueryClient _client;
    private readonly ILogger _logger;

    private ServiceHubContext? HubContext => _hubContextStore.HubContext;

    public SignalRFunctions(IHubContextStore hubContextStore, IToggleOnQueryClient client, ILogger<SignalRFunctions> logger)
    {
        _hubContextStore = hubContextStore;
        _client = client;
        _logger = logger;
    }

    [Function("OnConnected")]
    public Task OnConnected([SignalRTrigger("feature", "connections", "connected")] SignalRInvocationContext invocationContext)
    {
        _logger.LogInformation($"{invocationContext.ConnectionId} has connected");

        return Task.CompletedTask;
    }

    [Function(nameof(Initialize))]
    [SignalROutput(HubName = "feature")]
    public async Task Initialize([SignalRTrigger("feature", "messages", "initialize", "project", "environment")] 
        SignalRInvocationContext invocationContext,
        string project, 
        string environment, 
        CancellationToken cancellationToken)
    {
        try
        {
            await HubContext!.Groups.AddToGroupAsync(invocationContext.ConnectionId, $"{project}-{environment}", cancellationToken);

            var featureToggles = await _client.ExecuteAsync(new GetAllFeatureTogglesByNameQuery(project, environment));
            var featureGroups = await _client.ExecuteAsync(new GetAllFeatureGroupsQuery());
            await HubContext!.Clients.Client(invocationContext.ConnectionId)
                .SendAsync(
                "initialize", 
                new InitializeDto(
                    featureToggles.Select(t => new FeatureToggleDto(
                    t.Name,
                    t.Enabled,
                    t.Filters.Select(f => new FeatureFilterDto(
                            f.Name,
                            f.Parameters.ToDictionary(p => p.Name, p => p.Value))
                        ).ToList())
                    ).ToList(),
                    featureGroups.Select(g => new FeatureGroupDto(
                        g.Name,
                        g.UserIds,
                        g.IpAddresses
                    )).ToList()
                ), 
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SignalR function invocation failed: {function}", nameof(Initialize));
            throw;
        }
    }
}
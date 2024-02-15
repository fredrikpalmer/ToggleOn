using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;
using ToggleOn.Client.Contract;
using ToggleOn.MassTransit;
using ToggleOn.Messaging.Contract;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.FeatureToggle;

namespace ToggleOn.Functions.Consumers;

[SubscriptionEndpoint("%WEBSITE_SITE_NAME%", Constants.FeatureToggleUpdatedTopic)]
public class FeatureToggleUpdatedConsumer : IConsumer<FeatureToggleUpdated>
{
    private readonly IToggleOnQueryClient _client;
    private readonly IHubContextStore _hubContextStore;

    public ServiceHubContext HubContext => _hubContextStore.HubContext!;

    public FeatureToggleUpdatedConsumer(IToggleOnQueryClient client, IHubContextStore hubContextStore)
    {
        _client = client;
        _hubContextStore = hubContextStore;
    }

    public async Task Consume(ConsumeContext<FeatureToggleUpdated> context)
    {
        var featureToggle = await _client.ExecuteAsync(new GetFeatureToggleQuery(context.Message.Id));

        await HubContext.Clients.Group($"{featureToggle.ProjectName}-{featureToggle.EnvironmentName}").SendAsync(
            "feature-toggle-updated", 
            new FeatureToggleDto(
                featureToggle.Name,  
                featureToggle.Enabled,
                featureToggle.Filters
                    .Select(f => new FeatureFilterDto(
                        f.Name, 
                        f.Parameters.ToDictionary(p => p.Name, p => p.Value)
                    )).ToList()
                ), 
            context.CancellationToken
        );
    }
}

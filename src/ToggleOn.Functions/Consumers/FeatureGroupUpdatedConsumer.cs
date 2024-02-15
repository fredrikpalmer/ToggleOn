using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;
using ToggleOn.Client.Contract;
using ToggleOn.MassTransit;
using ToggleOn.Messaging.Contract;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.FeatureGroup;

namespace ToggleOn.Functions.Consumers;

[SubscriptionEndpoint("%WEBSITE_SITE_NAME%", Constants.FeatureGroupUpdatedTopic)]
public class FeatureGroupUpdatedConsumer : IConsumer<FeatureGroupUpdated>
{
    private readonly IToggleOnQueryClient _client;
    private readonly IHubContextStore _hubContextStore;

    public ServiceHubContext HubContext => _hubContextStore.HubContext!;

    public FeatureGroupUpdatedConsumer(IToggleOnQueryClient client, IHubContextStore hubContextStore)
    {
        _client = client;
        _hubContextStore = hubContextStore;
    }

    public async Task Consume(ConsumeContext<FeatureGroupUpdated> context)
    {
        var featureGroup = await _client.ExecuteAsync(new GetFeatureGroupQuery(context.Message.Id)) ?? throw new InvalidOperationException($"Feature group missing: {context.Message.Id}");
        await HubContext.Clients.All.SendAsync(
            "feature-group-updated",
            new FeatureGroupDto(featureGroup.Name, featureGroup.UserIds, featureGroup.IpAddresses),
            context.CancellationToken
        );
    }
}
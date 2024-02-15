using Azure.Messaging.ServiceBus;
using MassTransit.Transports;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ToggleOn.Functions.Consumers;
using ToggleOn.MassTransit;

namespace ToggleOn.Functions;

public class FeatureGroupUpdatedFunction
{
    private readonly IReceiveEndpointDispatcher<FeatureToggleUpdatedConsumer> _dispatcher;
    private readonly ILogger<FeatureGroupUpdatedFunction> _logger;

    public FeatureGroupUpdatedFunction(IReceiveEndpointDispatcher<FeatureToggleUpdatedConsumer> dispatcher, ILogger<FeatureGroupUpdatedFunction> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    [Function(nameof(FeatureGroupUpdatedFunction))]
    public async Task Run([ServiceBusTrigger(Constants.FeatureToggleUpdatedTopic, "%WEBSITE_SITE_NAME%", Connection = "ServiceBusConnection")] ServiceBusReceivedMessage message, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);

            await _dispatcher.Dispatch(message.Body.ToArray(), message.ApplicationProperties, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ServiceBus function trigger failed: {function}", nameof(FeatureGroupUpdatedFunction));
            throw;
        }
    }
}
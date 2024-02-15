using MassTransit;
using Microsoft.Extensions.Logging;
using ToggleOn.Data.Abstractions;
using ToggleOn.Messaging.Contract;

namespace ToggleOn.MassTransit;

[SubscriptionEndpoint("%WEBSITE_SITE_NAME%", Constants.EnvironmentCreatedTopic)]
public class EnvironmentCreatedConsumer : IConsumer<EnvironmentCreated>
{
    private readonly IFeatureToggleRepository _repository;
    private readonly ILogger<EnvironmentCreatedConsumer> _logger;

    public EnvironmentCreatedConsumer(IFeatureToggleRepository repository, ILogger<EnvironmentCreatedConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EnvironmentCreated> context)
    {
        _logger.LogInformation("Environment: {Environment} created", context.Message.EnvironmentId);

        var featureToggles = await _repository.GetAllAsync(context.Message.ProjectId, cancellationToken: context.CancellationToken);

        foreach (var toggle in featureToggles.DistinctBy(t => t.Feature.Name))
        {
            await context.Send(new Uri($"queue:{Constants.CreateFeatureToggleQueue}"), new CreateFeatureToggle(toggle.Feature.Id, toggle.Feature.Name, context.Message.ProjectId, context.Message.EnvironmentId));
        }
    }
}

using MassTransit;
using ToggleOn.Data.Abstractions;
using ToggleOn.Messaging.Contract;

namespace ToggleOn.MassTransit;

[SubscriptionEndpoint("%WEBSITE_SITE_NAME%", Constants.FeatureToggleCreatedTopic)]
public class FeatureToggleCreatedConsumer : IConsumer<FeatureToggleCreated>
{
    private readonly IFeatureToggleRepository _repository;
    private readonly IEnvironmentRepository _envRepository;

    public FeatureToggleCreatedConsumer(IFeatureToggleRepository repository, IEnvironmentRepository envRepository)
    {
        _repository = repository;
        _envRepository = envRepository;
    }

    public async Task Consume(ConsumeContext<FeatureToggleCreated> context)
    {
        var featureToggle = await _repository.GetAsync(context.Message.Id, context.CancellationToken);
        var environments = await _envRepository.GetAllAsync(context.Message.ProjectId, context.CancellationToken);

        var tasks = environments
            .Where(e => e.Id != context.Message.EnvironmentId)
            .Select(e => context.Send(new Uri($"queue:{Constants.CreateFeatureToggleQueue}"), new CreateFeatureToggle(
                featureToggle.Feature.Id,
                featureToggle.Feature.Name,
                featureToggle.Feature.ProjectId,
                e.Id)));

        await Task.WhenAll(tasks);
    }
}

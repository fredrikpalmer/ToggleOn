using MassTransit;
using Microsoft.Extensions.Logging;
using ToggleOn.Data.Abstractions;
using ToggleOn.Messaging.Contract;

namespace ToggleOn.MassTransit;

[ReceiveEndpoint(Constants.CreateFeatureToggleQueue, Constants.CreateFeatureToggleTopic)]
public class CreateFeatureToggleConsumer : IConsumer<CreateFeatureToggle>
{
    private readonly IFeatureToggleRepository _repository;
    private readonly ILogger<CreateFeatureToggleConsumer> _logger;

    public CreateFeatureToggleConsumer(IFeatureToggleRepository repository, ILogger<CreateFeatureToggleConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateFeatureToggle> context)
    {
        _logger.LogInformation("Creating toggle for project: {Project} env: {Environment} feature: {Feature}", context.Message.ProjectId, context.Message.EnvironmentId, context.Message.FeatureId);

        var feature = await _repository.GetAsync(context.Message.ProjectId, context.Message.Name) ?? throw new InvalidOperationException($"Feature missing: {context.Message.Name}");

        await _repository.CreateAsync(new Domain.FeatureToggle(feature.Id, context.Message.EnvironmentId), context.CancellationToken);
    }
}

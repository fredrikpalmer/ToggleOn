using ToggleOn.Abstractions;
using ToggleOn.Command.Contract.FeatureToggle;
using ToggleOn.Domain;
using ToggleOn.Data.Abstractions;
using ToggleOn.Messaging.Contract;
using ToggleOn.Contract;

namespace ToggleOn.Command.Client.FeatureToggle;

internal class CreateFeatureToggleCommandHandler : ICommandHandler<CreateFeatureToggleCommand, FeatureToggleDto>
{
    private readonly IFeatureToggleRepository _repository;
    private readonly IToggleOnEventPublisher _publisher;

    public CreateFeatureToggleCommandHandler(IFeatureToggleRepository repository, IToggleOnEventPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<FeatureToggleDto> HandleAsync(CreateFeatureToggleCommand command, CancellationToken cancellationToken = default)
    {
        if(command is null) throw new ArgumentNullException(nameof(command));

        //TODO: Add validation

        var feature = await _repository.GetAsync(command.ProjectId, command.Name, cancellationToken) ?? new Feature(command.FeatureId, command.ProjectId, command.Name);

        var toggle = new Domain.FeatureToggle(command.Id, feature.Id, command.EnvironmentId);
        feature.Toggles.Add(toggle);

        if (feature.Id != command.FeatureId)
        {
            await _repository.UpdateAsync(feature, cancellationToken);
        }
        else
        {
            await _repository.CreateAsync(feature, cancellationToken);

            await _publisher.Publish(new FeatureToggleCreated(toggle.Id, toggle.EnvironmentId, feature.ProjectId), cancellationToken);
        }

        return new(toggle.Id, feature.Id, feature.ProjectId, toggle.EnvironmentId, feature.Name, toggle.Enabled, new List<FeatureFilterDto>());
    }
}

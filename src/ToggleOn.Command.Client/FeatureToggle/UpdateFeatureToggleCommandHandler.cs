using ToggleOn.Abstractions;
using ToggleOn.Command.Contract;
using ToggleOn.Command.Contract.FeatureToggle;
using ToggleOn.Data.Abstractions;
using ToggleOn.Domain;
using ToggleOn.Messaging.Contract;

namespace ToggleOn.Command.Client.FeatureToggle;

internal class UpdateFeatureToggleCommandHandler : ICommandHandler<UpdateFeatureToggleCommand, VoidResult>
{
    private readonly IFeatureToggleRepository _repository;
    private readonly IToggleOnEventPublisher _publisher;

    public UpdateFeatureToggleCommandHandler(IFeatureToggleRepository repository, IToggleOnEventPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<VoidResult> HandleAsync(UpdateFeatureToggleCommand command, CancellationToken cancellationToken = default)
    {
        //TODO: Validation

        var featureToggle = new Domain.FeatureToggle(command.Id, command.FeatureId, command.EnvironmentId)
        {
            Enabled = command.Enabled,
        };

        foreach (var commandFilter in command.Filters)
        {
            var filter = new FeatureFilter(commandFilter.Id, commandFilter.FeatureToggleId, commandFilter.Name);

            foreach (var p in commandFilter.Parameters)
            {
                filter.Parameters.Add(new FeatureFilterParameter(p.Id, p.FeatureFilterId, p.Name, p.Value));
            }

            featureToggle.Filters.Add(filter);
        }

        await _repository.UpdateAsync(featureToggle, cancellationToken);

        await _publisher.Publish(new FeatureToggleUpdated(command.Id), cancellationToken);

        return new();
    }
}

using ToggleOn.Abstractions;
using ToggleOn.Command.Contract;
using ToggleOn.Command.Contract.FeatureToggle;
using ToggleOn.Data.Abstractions;
using ToggleOn.Messaging.Contract;

namespace ToggleOn.Command.Client.FeatureToggle;

internal class UpdateFeatureToggleTargetingCommandHandler : ICommandHandler<UpdateFeatureToggleTargetingCommand, VoidResult>
{
    private readonly IFeatureToggleRepository _repository;
    private readonly IToggleOnEventPublisher _publisher;

    public UpdateFeatureToggleTargetingCommandHandler(IFeatureToggleRepository repository, IToggleOnEventPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<VoidResult> HandleAsync(UpdateFeatureToggleTargetingCommand command, CancellationToken cancellationToken = default)
    {
        //TODO: Add validation

        await _repository.UpdateEnabledAsync(command.FeatureToggleId, command.Enabled, cancellationToken);

        await _publisher.Publish(new FeatureToggleUpdated(command.FeatureToggleId), cancellationToken);

        return new();
    }
}

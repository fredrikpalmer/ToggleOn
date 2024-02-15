using ToggleOn.Abstractions;
using ToggleOn.Command.Contract;
using ToggleOn.Command.Contract.FeatureGroup;
using ToggleOn.Data.Abstractions;
using ToggleOn.Messaging.Contract;

namespace ToggleOn.Command.Client.FeatureGroup;

internal class UpdateFeatureGroupCommandHandler : ICommandHandler<UpdateFeatureGroupCommand, VoidResult>
{
    private readonly IFeatureGroupRepository _repository;
    private readonly IToggleOnEventPublisher _publisher;

    public UpdateFeatureGroupCommandHandler(IFeatureGroupRepository repository, IToggleOnEventPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<VoidResult> HandleAsync(UpdateFeatureGroupCommand command, CancellationToken cancellationToken = default)
    {
        //TODO: Validation

        await _repository.UpdateAsync(command.Id, command.UserIds, command.IpAddresses, cancellationToken);

        await _publisher.Publish(new FeatureGroupUpdated(command.Id));

        return new();
    }
}

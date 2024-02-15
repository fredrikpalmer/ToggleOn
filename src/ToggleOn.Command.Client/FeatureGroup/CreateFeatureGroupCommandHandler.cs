using FluentValidation;
using ToggleOn.Abstractions;
using ToggleOn.Command.Contract;
using ToggleOn.Command.Contract.FeatureGroup;
using ToggleOn.Data.Abstractions;

namespace ToggleOn.Command.Client.FeatureGroup;

internal class CreateFeatureGroupCommandHandler : ICommandHandler<CreateFeatureGroupCommand, VoidResult>
{
    private readonly IValidator<CreateFeatureGroupCommand> _validator;
    private readonly IFeatureGroupRepository _repository;

    public CreateFeatureGroupCommandHandler(IValidator<CreateFeatureGroupCommand> validator, IFeatureGroupRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<VoidResult> HandleAsync(CreateFeatureGroupCommand command, CancellationToken cancellationToken = default)
    {
        if(command is null) throw new ArgumentNullException(nameof(command));

        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        await _repository.CreateAsync(new Domain.FeatureGroup(command.Id, command.Name, command.UserIds, command.IpAddresses), cancellationToken);

        return new();
    }
}

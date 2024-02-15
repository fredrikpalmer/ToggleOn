using FluentValidation;
using ToggleOn.Abstractions;
using ToggleOn.Command.Contract;
using ToggleOn.Command.Contract.Project;
using ToggleOn.Data.Abstractions;

namespace ToggleOn.Command.Client.Project;

internal class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand, VoidResult>
{
    private readonly IProjectRepository _repository;
    private readonly IValidator<DeleteProjectCommand> _validator;

    public DeleteProjectCommandHandler(IProjectRepository repository, IValidator<DeleteProjectCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<VoidResult> HandleAsync(DeleteProjectCommand command, CancellationToken cancellationToken = default)
    {
        if(command is null) throw new ArgumentNullException(nameof(command));

        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        await _repository.DeleteAsync(command.Id, cancellationToken);

        return new();
    }
}

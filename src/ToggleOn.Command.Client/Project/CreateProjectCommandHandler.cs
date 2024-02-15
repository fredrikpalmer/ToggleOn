using FluentValidation;
using ToggleOn.Abstractions;
using ToggleOn.Command.Contract.Project;
using ToggleOn.Contract;
using ToggleOn.Data.Abstractions;

namespace ToggleOn.Command.Client.Project;

internal class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _repository;
    private readonly IValidator<CreateProjectCommand> _validator;

    public CreateProjectCommandHandler(IProjectRepository repository, IValidator<CreateProjectCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<ProjectDto> HandleAsync(CreateProjectCommand command, CancellationToken cancellationToken = default)
    {
        if(command is null) throw new ArgumentNullException(nameof(command));

        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var project = new Domain.Project(command.Id, command.Name);
        await _repository.CreateAsync(project, cancellationToken).ConfigureAwait(false);

        return new ProjectDto(project.Id, project.Name);
    }
}

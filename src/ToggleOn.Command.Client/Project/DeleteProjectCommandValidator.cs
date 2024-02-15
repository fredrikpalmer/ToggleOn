using FluentValidation;
using ToggleOn.Command.Contract.Project;

namespace ToggleOn.Command.Client.Project;

internal class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}
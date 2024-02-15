using FluentValidation;
using ToggleOn.Command.Contract.Project;

namespace ToggleOn.Command.Client.Project;

internal class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Name).NotEmpty().MinimumLength(1);
    }
}
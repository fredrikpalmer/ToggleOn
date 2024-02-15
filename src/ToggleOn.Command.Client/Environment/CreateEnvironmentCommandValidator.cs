using FluentValidation;
using ToggleOn.Command.Contract.Environment;

namespace ToggleOn.Command.Client.Environment;

internal class CreateEnvironmentCommandValidator : AbstractValidator<CreateEnvironmentCommand>
{
    public CreateEnvironmentCommandValidator()
    {
        RuleFor(e => e.Id).NotEmpty();
        RuleFor(e => e.ProjectId).NotEmpty();
        RuleFor(e => e.Name).NotEmpty().MinimumLength(1);    
    }
}
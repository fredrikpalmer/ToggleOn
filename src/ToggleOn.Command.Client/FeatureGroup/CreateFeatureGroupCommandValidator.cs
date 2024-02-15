using FluentValidation;
using ToggleOn.Command.Contract.FeatureGroup;

namespace ToggleOn.Command.Client.FeatureGroup;

internal class CreateFeatureGroupCommandValidator : AbstractValidator<CreateFeatureGroupCommand>
{
    public CreateFeatureGroupCommandValidator()
    {
        RuleFor(g => g.Id).NotEmpty();
        RuleFor(g => g.Name).NotEmpty();
    }
}

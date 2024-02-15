using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToggleOn.Api.Extensions
{
    public static class ModelStateExtensions
    {
        public static ModelStateDictionary AddValidationErrors(this ModelStateDictionary modelState, IEnumerable<ValidationFailure> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return modelState;
        }
    }
}

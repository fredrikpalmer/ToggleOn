using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToggleOn.Api.Extensions;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.FeatureToggle;

namespace ToggleOn.Api.FeatureToggle;

[Route("featuretoggles")]
[ApiController]
public class UpdateFeatureToggleEnabledController : ControllerBase
{
    private readonly IToggleOnCommandClient _client;

    public UpdateFeatureToggleEnabledController(IToggleOnCommandClient client)
    {
        _client = client;
    }

    [HttpPatch("{id}/enabled")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateFeatureToggleTargetingCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _client.ExecuteAsync(command, cancellationToken);

            return Accepted(result);
        }
        catch (ValidationException ex)
        {
            return ValidationProblem(ModelState.AddValidationErrors(ex.Errors));
        }
    }
}

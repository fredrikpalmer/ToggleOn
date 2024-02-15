using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToggleOn.Api.Extensions;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.FeatureToggle;

namespace ToggleOn.Api.FeatureToggle;

[Route("featuretoggles")]
[ApiController]
public class UpdateFeatureToggleController : ControllerBase
{
    private readonly IToggleOnCommandClient _client;

    public UpdateFeatureToggleController(IToggleOnCommandClient client)
    {
        _client = client;
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateFeatureToggleCommand command, CancellationToken cancellationToken)
    {
		try
		{
            return Ok(await _client.ExecuteAsync(command, cancellationToken));
		}
		catch (ValidationException ex)
		{
			return ValidationProblem(ModelState.AddValidationErrors(ex.Errors));
		}
    }
}

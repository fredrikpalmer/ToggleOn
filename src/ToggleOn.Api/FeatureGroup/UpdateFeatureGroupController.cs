using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToggleOn.Api.Extensions;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.FeatureGroup;

namespace ToggleOn.Api.FeatureGroup;

[Route("featuregroups")]
[ApiController]
public class UpdateFeatureGroupController : ControllerBase
{
    private readonly IToggleOnCommandClient _client;

    public UpdateFeatureGroupController(IToggleOnCommandClient client)
    {
        _client = client;
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateFeatureGroupCommand command, CancellationToken cancellationToken)
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

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToggleOn.Api.Extensions;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.FeatureToggle;

namespace ToggleOn.Api.FeatureToggle;

[Route("featuretoggles")]
[ApiController]
public class CreateFeatureToggleController : ControllerBase
{
    private readonly IToggleOnCommandClient _client;

    public CreateFeatureToggleController(IToggleOnCommandClient client)
    {
        _client = client;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateFeatureToggleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var toggle = await _client.ExecuteAsync(command, cancellationToken);
            return Created($"featuretoggles/{toggle.Id}", toggle);
        }
        catch (ValidationException ex)
        {
            return ValidationProblem(ModelState.AddValidationErrors(ex.Errors));
        }
    }
}

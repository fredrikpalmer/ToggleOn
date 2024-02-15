using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToggleOn.Api.Extensions;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.FeatureGroup;
using ToggleOn.Contract;

namespace ToggleOn.Api.FeatureGroup;

[Route("featuregroups")]
[ApiController]
public class CreateFeatureGroupController : ControllerBase
{
    private readonly IToggleOnCommandClient _client;

    public CreateFeatureGroupController(IToggleOnCommandClient client)
    {
        _client = client;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateFeatureGroupCommand command, CancellationToken cancellationToken)
    {
		try
		{
            await _client.ExecuteAsync(command, cancellationToken);

            return Created($"featuregroups/{command.Id}", new FeatureGroupDto(command.Id, command.Name, command.UserIds, command.IpAddresses));
		}
		catch (ValidationException ex)
		{
			return ValidationProblem(ModelState.AddValidationErrors(ex.Errors));
		}
    }
}

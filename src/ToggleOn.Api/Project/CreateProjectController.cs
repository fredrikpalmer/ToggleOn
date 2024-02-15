using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToggleOn.Api.Extensions;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.Project;

namespace ToggleOn.Api.Project;

[Route("projects")]
[ApiController]
public class CreateProjectController : ControllerBase
{
    private readonly IToggleOnCommandClient _client;
    private readonly ILogger<CreateProjectController> _logger;

    public CreateProjectController(IToggleOnCommandClient client, ILogger<CreateProjectController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProjectCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var project = await _client.ExecuteAsync(command, cancellationToken);

            return Created($"projects/{project?.Id}", project);
        }
        catch (ValidationException ex)
        {
            return ValidationProblem(ModelState.AddValidationErrors(ex.Errors));
        }
    }
}

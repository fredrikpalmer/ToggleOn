using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToggleOn.Api.Extensions;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.Project;

namespace ToggleOn.Api.Project;

[Route("projects")]
[ApiController]
public class DeleteProjectController : ControllerBase
{
    private readonly IToggleOnCommandClient _client;
    private readonly ILogger<DeleteProjectController> _logger;

    public DeleteProjectController(IToggleOnCommandClient client, ILogger<DeleteProjectController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _client.ExecuteAsync(new DeleteProjectCommand(id), cancellationToken);

            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return ValidationProblem(ModelState.AddValidationErrors(ex.Errors));
        }
    }
}

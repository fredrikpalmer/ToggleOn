using Microsoft.AspNetCore.Mvc;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.Project;

namespace ToggleOn.Api.Project;

[Route("projects")]
[ApiController]
public class GetProjectController : ControllerBase
{
    private readonly IToggleOnQueryClient _client;
    private readonly ILogger<GetProjectController> _logger;

    public GetProjectController(IToggleOnQueryClient client, ILogger<GetProjectController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAllAsync(Guid id, CancellationToken cancellationToken)
    {
        var project = await _client.ExecuteAsync(new GetProjectQuery(id), cancellationToken);
        if(project is null) return NotFound();

        return Ok(project);
    }
}
using Microsoft.AspNetCore.Mvc;
using ToggleOn.Contract;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.Project;

namespace ToggleOn.Api.Project;

[Route("projects")]
[ApiController]
public class GetAllProjectsController : ControllerBase
{
    private readonly IToggleOnQueryClient _client;
    private readonly ILogger<GetAllProjectsController> _logger;

    public GetAllProjectsController(IToggleOnQueryClient client, ILogger<GetAllProjectsController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllProjectsQuery query, CancellationToken cancellationToken)
    {
        return Ok(await _client.ExecuteAsync(query, cancellationToken) ?? new List<ProjectDto>());
    }
}

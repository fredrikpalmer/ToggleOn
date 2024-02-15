using Microsoft.AspNetCore.Mvc;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.FeatureToggle;

namespace ToggleOn.Api.FeatureToggle;

[Route("featuretoggles")]
[ApiController]
public class GetAllFeatureTogglesByIdController : ControllerBase
{
    private readonly IToggleOnQueryClient _client;

    public GetAllFeatureTogglesByIdController(IToggleOnQueryClient client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(Guid projectId, Guid environmentId, CancellationToken cancellationToken)
    {
        return Ok(await _client.ExecuteAsync(new GetAllFeatureTogglesByIdQuery(projectId, environmentId), cancellationToken));
    }
}

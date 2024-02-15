using Microsoft.AspNetCore.Mvc;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.FeatureToggle;

namespace ToggleOn.Api.FeatureToggle;

[Route("api/featuretoggles")]
[ApiController]
public class GetAllFeatureTogglesByNameController : ControllerBase
{
    private readonly IToggleOnQueryClient _client;

    public GetAllFeatureTogglesByNameController(IToggleOnQueryClient client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(string project, string environment, CancellationToken cancellationToken)
    {
        return Ok(await _client.ExecuteAsync(new GetAllFeatureTogglesByNameQuery(project, environment), cancellationToken));
    }
}

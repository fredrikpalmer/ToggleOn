using Microsoft.AspNetCore.Mvc;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.FeatureToggle;

namespace ToggleOn.Api.FeatureToggle;

[Route("featuretoggles")]
[ApiController]
public class GetFeatureToggleController : ControllerBase
{
    private readonly IToggleOnQueryClient _client;

    public GetFeatureToggleController(IToggleOnQueryClient client)
    {
        _client = client;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _client.ExecuteAsync(new GetFeatureToggleQuery(id), cancellationToken));
    }
}

using Microsoft.AspNetCore.Mvc;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.FeatureGroup;

namespace ToggleOn.Api.FeatureGroup;

[Route("featuregroups")]
[ApiController]
public class GetAllFeatureGroupsController : ControllerBase
{
    private readonly IToggleOnQueryClient _client;

    public GetAllFeatureGroupsController(IToggleOnQueryClient client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllFeatureGroupsQuery query, CancellationToken cancellationToken)
    {
        return Ok(await _client.ExecuteAsync(query, cancellationToken));
    }
}

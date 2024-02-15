using Microsoft.AspNetCore.Mvc;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.FeatureGroup;

namespace ToggleOn.Api.FeatureGroup;

[Route("featuregroups")]
[ApiController]
public class GetFeatureGroupController : ControllerBase
{
    private readonly IToggleOnQueryClient _client;

    public GetFeatureGroupController(IToggleOnQueryClient client)
    {
        _client = client;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var featureGroup = await _client.ExecuteAsync(new GetFeatureGroupQuery(id), cancellationToken);
        if(featureGroup is null) return NotFound();

        return Ok(featureGroup);    
    }
}

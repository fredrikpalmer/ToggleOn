using Microsoft.AspNetCore.Mvc;
using ToggleOn.Query.Client;
using ToggleOn.Query.Contract.Environment;

namespace ToggleOn.Api.Environment
{
    [Route("environments")]
    [ApiController]
    public class GetAllEnvironmentsController : ControllerBase
    {
        private readonly IToggleOnQueryClient _client;

        public GetAllEnvironmentsController(IToggleOnQueryClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid projectId, CancellationToken cancellationToken)
        {
            return Ok(await _client.ExecuteAsync(new GetAllEnvironmentsQuery(projectId)));
        }
    }
}

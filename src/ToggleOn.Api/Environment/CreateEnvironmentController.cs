using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToggleOn.Api.Extensions;
using ToggleOn.Command.Client;
using ToggleOn.Command.Contract.Environment;

namespace ToggleOn.Api.Environment
{
    [Route("environments")]
    [ApiController]
    public class CreateEnvironmentController : ControllerBase
    {
        private readonly IToggleOnCommandClient _client;
        private readonly ILogger<CreateEnvironmentController> _logger;

        public CreateEnvironmentController(IToggleOnCommandClient client, ILogger<CreateEnvironmentController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEnvironmentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var env = await _client.ExecuteAsync(command, cancellationToken);

                return Created($"environments/{env.Id}", env);
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(ModelState.AddValidationErrors(ex.Errors));
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ToggleOn.Api.Error;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError() => Problem();
}

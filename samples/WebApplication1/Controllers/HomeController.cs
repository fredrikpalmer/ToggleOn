using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToggleOn.Client.Abstractions;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICheckoutFeature _feature;

        public HomeController(ILogger<HomeController> logger, ICheckoutFeature feature)
        {
            _logger = logger;
            _feature = feature;
        }

        public async Task<IActionResult> Index()
        {
            if (await _feature.IsEnabledAsync()) return Redirect($"/home/privacy/{Request.QueryString.Value}");

            return View();
        }

        public async Task<IActionResult> Privacy([FromServices] IFeatureToggleBuilder builder)
        {
            var toggle = builder
                .WithName("test")
                .WithOptions(options => options.FilterRequirement = FilterRequirement.Any)
                .Build();

            if (await toggle.IsEnabledAsync()) return NotFound();


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

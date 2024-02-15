using ToggleOn.Admin.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ToggleOn.Core.Extensions;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddToggleOn(conf => conf.AddHttpClient(builder.Configuration["ApiBaseAddress"]!));

if (builder.HostEnvironment.IsDevelopment())
{
    builder.Logging
        .SetMinimumLevel(LogLevel.Debug)
        .AddFilter("Microsoft.AspNetCore.Components.RenderTree.*", LogLevel.None);
}

await builder.Build().RunAsync();


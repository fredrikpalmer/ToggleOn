using ToggleOn.AspNetCore.SignalR.Client.Extensions;
using ToggleOn.Client.Abstractions;
using ToggleOn.Client.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddToggleOn(conf =>
{
    conf
        .ConfigureProvider(builder.Configuration.GetSection("ToggleOn"))
        .UseSignalR(builder.Configuration["Azure:SignalR:Endpoint"]!, builder.Configuration["Azure:SignalR:AccessKey"]!);
});

builder.Services.AddFeatureToggle<ICheckoutFeature, CheckoutFeature>(conf =>
{
    conf.WithName("checkout");
    conf.WithOptions(options => options.FilterRequirement = FilterRequirement.Any);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public interface ICheckoutFeature
{
    Task<bool> IsEnabledAsync(bool defaultValue = false);
}

public class CheckoutFeature(IFeatureToggle toggle) : ICheckoutFeature
{
    public async Task<bool> IsEnabledAsync(bool defaultValue = false)
    {
        return await toggle.IsEnabledAsync(defaultValue).ConfigureAwait(false);
    }
}

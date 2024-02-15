using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Abstractions;
using ToggleOn.Command.Client.Extensions;
using ToggleOn.Query.Client.Extensions;

namespace ToggleOn.Core;

internal class DefaultToggleOnAdminConfigurator : IToggleOnAdminConfigurator
{
    public IServiceCollection Services { get; }

    public DefaultToggleOnAdminConfigurator(IServiceCollection services)
    {
        Services = services;
    }

    public void AddHttpClient(string baseAddress)
    {
        Services.AddHttpClient<ISendHandler, HttpSendHandler>(client =>
        {
            client.BaseAddress = new Uri(baseAddress);
        });
    }

    public IToggleOnInProcessConfigurator AddInProcessClient()
    {
        Services.AddSingleton<ISendHandler, InProcSendHandler>();

        Services.AddQueries();
        Services.AddCommands();

        return new DefaultToggleOnInProcessConfigurator(Services);
    }
}

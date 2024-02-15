using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Abstractions;

namespace ToggleOn.Core;

internal class DefaultToggleOnInProcessConfigurator : IToggleOnInProcessConfigurator
{
    public IServiceCollection Services { get; }

    public DefaultToggleOnInProcessConfigurator(IServiceCollection services)
    {
        Services = services;
    }
}

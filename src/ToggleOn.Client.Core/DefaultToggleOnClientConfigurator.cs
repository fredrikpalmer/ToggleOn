using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

internal class DefaultToggleOnClientConfigurator : IToggleOnClientConfigurator
{
    public IServiceCollection Services { get; }

    public DefaultToggleOnClientConfigurator(IServiceCollection services)
    {
        Services = services;
    }

    public IToggleOnClientConfigurator ConfigureProvider(IConfiguration configuration)
    {
        Services.AddOptions<ToggleOnProviderOptions>().Bind(configuration);
        return this;
    }

    public IToggleOnClientConfigurator AddEnricher<TImplementation>() where TImplementation : class, IFeatureEnricher
    {
        Services.TryAddSingleton<IFeatureEnricher, TImplementation>();

        return this;
    }
}
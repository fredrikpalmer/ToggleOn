using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToggleOn.Client.Abstractions;

public interface IToggleOnClientConfigurator
{
    IServiceCollection Services { get; }

    IToggleOnClientConfigurator ConfigureProvider(IConfiguration configuration);
    IToggleOnClientConfigurator AddEnricher<TImplementation>() where TImplementation : class, IFeatureEnricher;
}

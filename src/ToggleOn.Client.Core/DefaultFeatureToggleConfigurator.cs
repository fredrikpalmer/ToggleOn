using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

internal class DefaultFeatureToggleConfigurator : IFeatureToggleConfigurator
{
    private string? _name;
    private Action<FeatureToggleEvaluatorOptions>? _configure;

    public IServiceCollection Services { get; }

    public DefaultFeatureToggleConfigurator(IServiceCollection services)
    {
        Services = services;
    }

    public IFeatureToggleConfigurator WithName(string name)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        return this;
    }

    public IFeatureToggleConfigurator WithOptions(Action<FeatureToggleEvaluatorOptions> configure)
    {
        _configure = configure ?? throw new ArgumentNullException(nameof(configure));
        return this;
    }

    public void AddFeatureToggle<TClient, TImplementation>() where TClient : class where TImplementation : class, TClient
    {
        Services.Configure(typeof(TImplementation).FullName, _configure ?? (options => { }));

        Services.AddSingleton<TClient>(sp => (TImplementation)Activator.CreateInstance(typeof(TImplementation), new DefaultFeatureToggle(
            new DefaultEvaluationContextAccessor(
                _name!, 
                sp.GetRequiredService<IToggleOnDataProvider>()
            ),
            new DefaultFeatureToggleEvaluator(
                sp.GetRequiredService<IOptionsMonitor<FeatureToggleEvaluatorOptions>>().Get(typeof(TImplementation).FullName),
                sp.GetRequiredService<IEnumerable<IFeatureFilter>>()))
            )!
        );
    }
}

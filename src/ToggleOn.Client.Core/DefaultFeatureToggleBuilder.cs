using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

internal class DefaultFeatureToggleBuilder : IFeatureToggleBuilder
{
    private string _name = string.Empty;
    private FeatureToggleEvaluatorOptions _options = new();

    private readonly IServiceProvider _serviceProvider;

    public DefaultFeatureToggleBuilder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IFeatureToggleBuilder WithName(string name)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));

        return this;
    }

    public IFeatureToggleBuilder WithOptions(Action<FeatureToggleEvaluatorOptions> configure)
    {
        if (configure is null) throw new ArgumentNullException(nameof(configure));
        configure.Invoke(_options);

        return this;
    }

    public IFeatureToggle Build()
    {
        return new DefaultFeatureToggle(
            new DefaultEvaluationContextAccessor(
                _name, 
                _serviceProvider.GetRequiredService<IToggleOnDataProvider>()
            ), 
            new DefaultFeatureToggleEvaluator(
                _options,
                _serviceProvider.GetRequiredService<IEnumerable<IFeatureFilter>>()
            )
        );
    }
}

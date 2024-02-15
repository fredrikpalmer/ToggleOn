using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

internal class DefaultEvaluationContextAccessor : IFeatureToggleEvaluationContextAccessor
{
    private readonly string _name;
    private readonly IToggleOnDataProvider _provider;

    public FeatureToggleEvaluationContext? EvaluationContext => _provider.GetEvaluationContext(_name);

    public DefaultEvaluationContextAccessor(string name, IToggleOnDataProvider provider)
    {
        _name = name;
        _provider = provider;
    }
}

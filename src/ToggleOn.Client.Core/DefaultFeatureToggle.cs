using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

public class DefaultFeatureToggle : IFeatureToggle
{
    private readonly IFeatureToggleEvaluationContextAccessor _contextAccessor;
    private readonly IFeatureToggleEvaluator _evaluator;

    public DefaultFeatureToggle(IFeatureToggleEvaluationContextAccessor contextAccessor, IFeatureToggleEvaluator evaluator)
    {
        _contextAccessor = contextAccessor;
        _evaluator = evaluator;
    }

    public Task<bool> IsEnabledAsync(bool defaultValue = false)
    {
        if (_contextAccessor.EvaluationContext is null) return Task.FromResult(defaultValue);

        return _evaluator.IsEnabledAsync(_contextAccessor.EvaluationContext, defaultValue);
    }
}

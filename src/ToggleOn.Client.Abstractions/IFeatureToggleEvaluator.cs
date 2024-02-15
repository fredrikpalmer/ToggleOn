namespace ToggleOn.Client.Abstractions;

public interface IFeatureToggleEvaluator
{
    Task<bool> IsEnabledAsync(FeatureToggleEvaluationContext context, bool defaultValue = false);
}

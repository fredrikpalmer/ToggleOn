namespace ToggleOn.Client.Abstractions;

public interface IFeatureToggleEvaluationContextAccessor
{
    FeatureToggleEvaluationContext? EvaluationContext { get; }
}

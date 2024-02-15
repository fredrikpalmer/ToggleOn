namespace ToggleOn.Client.Abstractions;

public interface IFeatureFilter
{
    string Name { get; }

    bool IsShortCircuit { get; }

    Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context);
}

namespace ToggleOn.Client.Abstractions;

public interface IFeatureToggleBuilder
{
    IFeatureToggleBuilder WithName(string name);
    IFeatureToggleBuilder WithOptions(Action<FeatureToggleEvaluatorOptions> configure);
    IFeatureToggle Build();
}

namespace ToggleOn.Client.Abstractions;

public interface IFeatureToggleConfigurator
{
    IFeatureToggleConfigurator WithName(string name);
    IFeatureToggleConfigurator WithOptions(Action<FeatureToggleEvaluatorOptions> configure);
}

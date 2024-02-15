using ToggleOn.Client.Contract;

namespace ToggleOn.Client.Abstractions;

public interface IToggleOnDataProvider
{
    FeatureToggleEvaluationContext? GetEvaluationContext(string name);

    FeatureGroupConfiguration? GetFeatureGroupConfiguration(string name);
}

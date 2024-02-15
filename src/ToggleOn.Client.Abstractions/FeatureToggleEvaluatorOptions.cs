namespace ToggleOn.Client.Abstractions;

public class FeatureToggleEvaluatorOptions
{
    public FilterRequirement FilterRequirement { get; set; } = FilterRequirement.All;
}

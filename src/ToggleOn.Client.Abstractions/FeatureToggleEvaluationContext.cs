namespace ToggleOn.Client.Abstractions;

public class FeatureToggleEvaluationContext
{
    public string Name { get; set; }
    public bool Enabled { get; set; }
    public IList<FeatureFilterConfiguration> Filters { get; set; }
}

namespace ToggleOn.Client.Abstractions;

public class FeatureFilterEvaluationContext
{
    public string Name { get; set; }
    public IDictionary<string, string> Parameters { get; set; }
}

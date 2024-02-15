namespace ToggleOn.Client.Abstractions;

public class FeatureFilterConfiguration
{
    public string Name { get; }
    public IDictionary<string, string> Parameters { get; }
    public FeatureFilterConfiguration(string name, IDictionary<string, string> parameters)
    {
        Name = name;
        Parameters = parameters;
    }
}

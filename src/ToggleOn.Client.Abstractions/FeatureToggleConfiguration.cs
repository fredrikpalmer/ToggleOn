namespace ToggleOn.Client.Abstractions;

public class FeatureToggleConfiguration
{
    public string Name { get; }
    public bool Enabled { get; set; }
    public IList<FeatureFilterConfiguration> Filters { get; }

    public FeatureToggleConfiguration(string name, bool enabled, IList<FeatureFilterConfiguration> filters)
    {
        Name = name;
        Enabled = enabled;
        Filters = filters;
    }
}

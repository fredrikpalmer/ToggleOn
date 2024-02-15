namespace ToggleOn.Domain;

public class FeatureFilterParameter
{
    public Guid Id { get; private set; }
    public Guid FeatureFilterId { get; private set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public FeatureFilter? Filter { get; set; }

    public FeatureFilterParameter(Guid id, Guid featureFilterId, string name, string value)
    {
        Id = id;
        FeatureFilterId = featureFilterId;
        Name = name;
        Value = value;
    }
}
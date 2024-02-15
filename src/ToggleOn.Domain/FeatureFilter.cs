namespace ToggleOn.Domain;

public class FeatureFilter
{
    public Guid Id { get; private set; }
    public Guid FeatureToggleId { get; private set; }
    public string Name { get; set; }
    public ISet<FeatureFilterParameter> Parameters { get; } = new HashSet<FeatureFilterParameter>();
    public FeatureToggle? FeatureToggle { get; set; }
    public FeatureFilter(Guid id, Guid featureToggleId, string name) => 
        (Id, FeatureToggleId, Name) = (id, featureToggleId, name ?? throw new ArgumentNullException(nameof(name)));
}

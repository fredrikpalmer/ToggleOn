namespace ToggleOn.Domain;

public class FeatureToggle
{
    public Guid Id { get; private set; }
    public Guid FeatureId { get; private set; }
    public Guid EnvironmentId { get; private set; }
    public bool Enabled { get; set; }
    public Feature Feature { get; set; }
    public Environment? Environment { get; set; }
    public ICollection<FeatureFilter> Filters { get; } = new List<FeatureFilter>();

    public FeatureToggle(Guid featureId, Guid environmentId) : this(Guid.NewGuid(), featureId, environmentId, new List<FeatureFilter>()) { }

    public FeatureToggle(Guid id, Guid featureId, Guid environmentId) : this(id, featureId, environmentId, new List<FeatureFilter>()) { }

    public FeatureToggle(Guid id, Guid featureId, Guid environmentId, ICollection<FeatureFilter> filters) => 
        (Id, FeatureId, EnvironmentId, Filters) = (id, featureId, environmentId, filters);
}

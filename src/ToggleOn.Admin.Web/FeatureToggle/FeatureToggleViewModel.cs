namespace ToggleOn.Admin.Web.FeatureToggle;

public class FeatureToggleViewModel
{
    public Guid Id { get; private set; }
    public Guid FeatureId { get; private set; }
    public Guid EnvironmentId { get; private set; }
    public string Name { get; private set; }
    public bool Enabled { get; set; }
    public IList<FilterViewModel> Filters { get; }

    public FeatureToggleViewModel(Guid id, Guid featureId, Guid environmentId, string name, bool enabled, IList<FilterViewModel> filters)
    {
        Id = id;
        FeatureId = featureId;
        EnvironmentId = environmentId;
        Name = name;
        Enabled = enabled;
        Filters = filters;
    }
}

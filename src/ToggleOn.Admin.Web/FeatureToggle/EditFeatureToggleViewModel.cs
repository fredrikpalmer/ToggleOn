namespace ToggleOn.Admin.Web.FeatureToggle;

public class EditFeatureToggleViewModel
{
    public Guid Id { get; set; }
    public Guid FeatureId { get; set; }
    public Guid EnvironmentId { get; set; }
    public string Name { get; set; }
    public bool Enabled { get; set; }
    public IList<AddFilterViewModel> Filters { get; set; } = new List<AddFilterViewModel>();
}

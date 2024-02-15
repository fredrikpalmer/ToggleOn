namespace ToggleOn.Admin.Web.FeatureToggle;

public class FilterViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FeatureToggleId { get; set; }
    public string Name { get; set; }
    public List<FilterParameterViewModel> Parameters { get; set; } = new List<FilterParameterViewModel>();

    public FilterViewModel(Guid id, Guid featureToggleId, string name, List<FilterParameterViewModel> parameters)
    {
        Id = id;
        FeatureToggleId = featureToggleId;
        Name = name;
        Parameters = parameters;
    }
}

namespace ToggleOn.Admin.Web.FeatureToggle;

public class AddFilterParameterViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FilterId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

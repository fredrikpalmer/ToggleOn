namespace ToggleOn.Admin.Web.FeatureToggle;

public class FilterParameterViewModel
{
    public Guid Id { get; set; }
    public Guid FilterId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }

    public FilterParameterViewModel(Guid id, Guid filterId, string name, string value)
    {
        Id = id;
        FilterId = filterId;
        Name = name;
        Value = value;
    }
}
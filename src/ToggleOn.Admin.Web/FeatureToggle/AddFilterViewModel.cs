using System.ComponentModel.DataAnnotations;

namespace ToggleOn.Admin.Web.FeatureToggle;

public class AddFilterViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FeatureToggleId { get; set; }

    [Required]
    public string Name { get; set; }
    public List<AddFilterParameterViewModel> Parameters { get; set; } = new();
}

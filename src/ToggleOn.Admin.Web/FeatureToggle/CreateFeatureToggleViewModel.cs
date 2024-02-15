using System.ComponentModel.DataAnnotations;

namespace ToggleOn.Admin.Web.FeatureToggle;

public class CreateFeatureToggleViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FeatureId { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;
}

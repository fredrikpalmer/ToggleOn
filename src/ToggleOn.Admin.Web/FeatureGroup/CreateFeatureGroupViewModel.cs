using System.ComponentModel.DataAnnotations;

namespace ToggleOn.Admin.Web.FeatureGroup;

public class CreateFeatureGroupViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;
}

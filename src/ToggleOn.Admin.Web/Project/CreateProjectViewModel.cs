using System.ComponentModel.DataAnnotations;

namespace ToggleOn.Admin.Web.Project;

public class CreateProjectViewModel
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;
}
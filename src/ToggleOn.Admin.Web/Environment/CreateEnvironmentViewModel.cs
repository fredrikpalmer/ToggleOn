using System.ComponentModel.DataAnnotations;

namespace ToggleOn.Admin.Web.Environment;

public class CreateEnvironmentViewModel
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid ProjectId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}
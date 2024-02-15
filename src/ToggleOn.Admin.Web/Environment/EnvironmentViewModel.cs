namespace ToggleOn.Admin.Web.Environment;

public class EnvironmentViewModel
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; }

    public EnvironmentViewModel(Guid id, Guid projectId, string name)
    {
        Id = id;
        ProjectId = projectId;
        Name = name;
    }
}

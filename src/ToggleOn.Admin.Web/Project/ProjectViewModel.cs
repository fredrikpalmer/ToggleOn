namespace ToggleOn.Admin.Web.Project;

public class ProjectViewModel
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public ProjectViewModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

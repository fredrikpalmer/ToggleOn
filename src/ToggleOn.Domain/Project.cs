namespace ToggleOn.Domain;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public ICollection<Environment> Environments { get; } = new List<Environment>();
    public ICollection<Feature> Features { get; } = new List<Feature>();

    public Project(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

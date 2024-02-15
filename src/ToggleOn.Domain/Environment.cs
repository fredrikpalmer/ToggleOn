namespace ToggleOn.Domain;

public class Environment
{
    public Guid Id { get; private set; } 
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; } 
    public Project? Project { get; set; }
    public ICollection<FeatureToggle> FeatureToggles { get; } = new List<FeatureToggle>();

    public Environment(Guid id, Guid projectId, string name)
    {
        Id = id;
        ProjectId = projectId;
        Name = name;
    }
}

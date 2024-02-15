namespace ToggleOn.Domain;

public class Feature
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; set; }
    public string Name { get; private set; }
    public Project? Project { get; set; }
    public ICollection<FeatureToggle> Toggles { get; } = new List<FeatureToggle>();
    public Feature(Guid id, Guid projectId, string name) => 
        (Id, ProjectId, Name) = (id, projectId, name ?? throw new ArgumentNullException(nameof(name)));

}

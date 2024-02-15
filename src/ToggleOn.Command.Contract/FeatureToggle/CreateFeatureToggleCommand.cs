using System.Text.Json.Serialization;
using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Command.Contract.FeatureToggle;

[ApiEndpoint("featuretoggles")]
public class CreateFeatureToggleCommand : ToggleOnCommand<FeatureToggleDto>
{
    public Guid Id { get; }
    public Guid FeatureId { get; set; }
    public Guid ProjectId { get; }
    public Guid EnvironmentId { get; set; }
    public string Name { get; }
    public bool Enabled { get; }

    [JsonConstructor]
    public CreateFeatureToggleCommand(Guid id, Guid featureId, Guid projectId, Guid environmentId, string name, bool enabled)
    {
        Id = id;
        FeatureId = featureId;
        ProjectId = projectId;
        EnvironmentId = environmentId;
        Name = name;
        Enabled = enabled;
    }
}

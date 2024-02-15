using System.Text.Json.Serialization;
using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Command.Contract.Project;

[ApiEndpoint("projects")]
public class CreateProjectCommand : ToggleOnCommand<ProjectDto>
{
    public Guid Id { get; }
    public string Name { get; }

    [JsonConstructor]
    public CreateProjectCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

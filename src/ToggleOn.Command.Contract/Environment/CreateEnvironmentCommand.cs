using System.Text.Json.Serialization;
using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Command.Contract.Environment
{
    [ApiEndpoint("environments")]
    public class CreateEnvironmentCommand : ToggleOnCommand<EnvironmentDto>
    {
        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }

        [JsonConstructor]
        public CreateEnvironmentCommand(Guid id, Guid projectId, string name)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
        }
    }
}

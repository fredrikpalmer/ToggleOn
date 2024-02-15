using ToggleOn.Http;

namespace ToggleOn.Command.Contract.Project;

[ApiEndpoint("projects/{0}", nameof(Id))]
[UseHttpMethod("DELETE")]
public class DeleteProjectCommand : ToggleOnCommand<VoidResult>
{
    public Guid Id { get; init; }

    private DeleteProjectCommand() { }
    public DeleteProjectCommand(Guid id)
    {
        Id = id;
    }
}

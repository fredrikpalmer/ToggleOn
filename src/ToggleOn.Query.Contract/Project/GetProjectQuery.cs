using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Query.Contract.Project;

[ApiEndpoint("projects/{0}", nameof(Id))]
public class GetProjectQuery : ToggleOnQuery<ProjectDto>
{
    public Guid Id { get; }

    public GetProjectQuery(Guid id)
    {
        Id = id;
    }
}

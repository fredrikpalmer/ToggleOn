using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Query.Contract.Environment;

[ApiEndpoint("environments")]
public class GetAllEnvironmentsQuery : ToggleOnQuery<IList<EnvironmentDto>>
{
    public GetAllEnvironmentsQuery(Guid projectId)
    {
        ProjectId = projectId;
    }

    public Guid ProjectId { get; set; }
}

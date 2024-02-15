using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Query.Contract.FeatureToggle;

[ApiEndpoint("featuretoggles")]
public class GetAllFeatureTogglesByIdQuery : ToggleOnQuery<IList<FeatureToggleDto>>
{
    public Guid ProjectId { get; }
    public Guid EnvironmentId { get; }

    public GetAllFeatureTogglesByIdQuery(Guid projectId, Guid environmentId)
    {
        ProjectId = projectId;
        EnvironmentId = environmentId;
    }
}

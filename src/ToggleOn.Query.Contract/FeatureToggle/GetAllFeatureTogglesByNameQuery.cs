using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Query.Contract.FeatureToggle;

[ApiEndpoint("api/featuretoggles")]
public class GetAllFeatureTogglesByNameQuery : ToggleOnQuery<IList<FeatureToggleDto>>
{
    public string Project { get; }
    public string Environment { get; }

    public GetAllFeatureTogglesByNameQuery(string project, string environment)
    {
        Project = project;
        Environment = environment;
    }
}

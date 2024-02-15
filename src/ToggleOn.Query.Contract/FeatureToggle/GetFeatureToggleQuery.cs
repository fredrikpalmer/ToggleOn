using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Query.Contract.FeatureToggle;

[ApiEndpoint("featuretoggles/{0}", nameof(Id))]
public class GetFeatureToggleQuery : ToggleOnQuery<FeatureToggleDto>
{
    public Guid Id { get; set; }

    public GetFeatureToggleQuery(Guid id)
    {
        Id = id;
    }
}

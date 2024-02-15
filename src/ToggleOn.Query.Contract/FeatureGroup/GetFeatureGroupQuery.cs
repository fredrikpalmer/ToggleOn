using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Query.Contract.FeatureGroup;

[ApiEndpoint("featuregroups/{0}", nameof(Id))]
public class GetFeatureGroupQuery : ToggleOnQuery<FeatureGroupDto?>
{
    public Guid Id { get; set; }

    public GetFeatureGroupQuery(Guid id)
    {
        Id = id;
    }
}

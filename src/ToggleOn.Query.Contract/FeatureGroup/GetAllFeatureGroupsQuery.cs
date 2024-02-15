using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Query.Contract.FeatureGroup;

[ApiEndpoint("featuregroups")]
public class GetAllFeatureGroupsQuery : ToggleOnQuery<IList<FeatureGroupDto>> { }
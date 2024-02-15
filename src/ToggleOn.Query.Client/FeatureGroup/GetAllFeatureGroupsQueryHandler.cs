using ToggleOn.Abstractions;
using ToggleOn.Contract;
using ToggleOn.Data.Abstractions;
using ToggleOn.Query.Contract.FeatureGroup;

namespace ToggleOn.Query.Client.FeatureGroup;

internal class GetAllFeatureGroupsQueryHandler : IQueryHandler<GetAllFeatureGroupsQuery, IList<FeatureGroupDto>>
{
    private readonly IFeatureGroupRepository _repository;

    public GetAllFeatureGroupsQueryHandler(IFeatureGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<FeatureGroupDto>> HandleAsync(GetAllFeatureGroupsQuery query, CancellationToken cancellationToken = default)
    {
        if(query is null) throw new ArgumentNullException(nameof(query));

        var featureGroups = await _repository.GetAllAsync(cancellationToken);

        return featureGroups.Select(g => new FeatureGroupDto(g.Id, g.Name, g.UserIds, g.IpAddresses)).ToList();
    }
}

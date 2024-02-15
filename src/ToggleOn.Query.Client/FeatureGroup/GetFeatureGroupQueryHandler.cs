using ToggleOn.Abstractions;
using ToggleOn.Contract;
using ToggleOn.Data.Abstractions;
using ToggleOn.Query.Contract.FeatureGroup;

namespace ToggleOn.Query.Client.FeatureGroup;

internal class GetFeatureGroupQueryHandler : IQueryHandler<GetFeatureGroupQuery, FeatureGroupDto?>
{
    private readonly IFeatureGroupRepository _repository;

    public GetFeatureGroupQueryHandler(IFeatureGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<FeatureGroupDto?> HandleAsync(GetFeatureGroupQuery query, CancellationToken cancellationToken = default)
    {
        if (query is null) throw new ArgumentNullException(nameof(query));

        var featureGroup = await _repository.GetAsync(query.Id, cancellationToken);
        if(featureGroup is null) return null;

        return new(featureGroup.Id, featureGroup.Name, featureGroup.UserIds, featureGroup.IpAddresses);
    }
}

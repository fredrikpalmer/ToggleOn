using ToggleOn.Abstractions;
using ToggleOn.Contract;
using ToggleOn.Data.Abstractions;
using ToggleOn.Query.Contract.FeatureToggle;

namespace ToggleOn.Query.Client.FeatureToggle;

internal class GetAllFeatureTogglesByIdQueryHandler : IQueryHandler<GetAllFeatureTogglesByIdQuery, IList<FeatureToggleDto>>
{
    private readonly IFeatureToggleRepository _repository;

    public GetAllFeatureTogglesByIdQueryHandler(IFeatureToggleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<FeatureToggleDto>> HandleAsync(GetAllFeatureTogglesByIdQuery query, CancellationToken cancellationToken = default)
    {
        if (query is null) throw new ArgumentNullException(nameof(query));

        var featureToggles = await _repository.GetAllAsync(query.ProjectId, query.EnvironmentId, cancellationToken);

        return featureToggles
                    .Select(t => new FeatureToggleDto(
                                 t.Id,
                                 t.FeatureId,
                                 t.Feature.ProjectId, 
                                 t.EnvironmentId, 
                                 t.Feature.Name, 
                                 t.Enabled, 
                                 t.Filters.Select(filter => new FeatureFilterDto(
                                     filter.Id, 
                                     filter.FeatureToggleId, 
                                     filter.Name, 
                                     filter.Parameters
                                         .Select(p => new FeatureFilterParameterDto(
                                             p.Id, 
                                             p.FeatureFilterId, 
                                             p.Name, 
                                             p.Value)).ToHashSet()))
                                         .ToList())).ToList();
    }
}

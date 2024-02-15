using ToggleOn.Abstractions;
using ToggleOn.Contract;
using ToggleOn.Data.Abstractions;
using ToggleOn.Query.Contract.FeatureToggle;

namespace ToggleOn.Query.Client.FeatureToggle;

internal class GetFeatureToggleQueryHandler : IQueryHandler<GetFeatureToggleQuery, FeatureToggleDto>
{
    private readonly IFeatureToggleRepository _repository;

    public GetFeatureToggleQueryHandler(IFeatureToggleRepository repository)
    {
        _repository = repository;
    }

    public async Task<FeatureToggleDto> HandleAsync(GetFeatureToggleQuery query, CancellationToken cancellationToken = default)
    {
        if(query is null) throw new ArgumentNullException(nameof(query));

        var toggle = await _repository.GetAsync(query.Id, cancellationToken);

        return new FeatureToggleDto(
                    toggle.Id,
                    toggle.Feature.Id,
                    toggle.Feature.ProjectId,
                    toggle.EnvironmentId,
                    toggle.Feature.Name,
                    toggle.Enabled,
                    toggle.Filters.Select(f => new FeatureFilterDto(
                            f.Id,
                            f.FeatureToggleId,
                            f.Name,
                            f.Parameters.Select(p => new FeatureFilterParameterDto(
                                    p.Id,
                                    p.FeatureFilterId,
                                    p.Name,
                                    p.Value
                                )
                        ).ToHashSet())
                    ).ToList()
                )
        {
            ProjectName = toggle.Feature.Project?.Name,
            EnvironmentName = toggle.Environment?.Name
        };
    }
}

namespace ToggleOn.Contract;

public record FeatureToggleDto(Guid Id, Guid FeatureId, Guid ProjectId, Guid EnvironmentId, string Name, bool Enabled, IList<FeatureFilterDto> Filters)
{
    public string? ProjectName { get; init; }
    public string? EnvironmentName { get; init; }
}

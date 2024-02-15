namespace ToggleOn.Contract;

public record FeatureFilterDto(Guid Id, Guid FeatureToggleId, string Name, ISet<FeatureFilterParameterDto> Parameters);

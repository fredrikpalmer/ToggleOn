namespace ToggleOn.Client.Contract;

public record FeatureToggleDto(string Name, bool Enabled, IList<FeatureFilterDto> Filters);

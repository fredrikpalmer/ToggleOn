namespace ToggleOn.Client.Contract;

public record InitializeDto(IList<FeatureToggleDto> FeatureToggles, IList<FeatureGroupDto> FeatureGroups);
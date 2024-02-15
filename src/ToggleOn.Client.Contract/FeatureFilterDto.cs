namespace ToggleOn.Client.Contract;

public record FeatureFilterDto(string Name, IDictionary<string, string> Parameters);
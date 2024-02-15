namespace ToggleOn.Messaging.Contract;

public record CreateFeatureToggle(Guid FeatureId, string Name, Guid ProjectId, Guid EnvironmentId);

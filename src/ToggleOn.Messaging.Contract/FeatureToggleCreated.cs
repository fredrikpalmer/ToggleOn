namespace ToggleOn.Messaging.Contract;

public record FeatureToggleCreated(Guid Id, Guid EnvironmentId, Guid ProjectId);

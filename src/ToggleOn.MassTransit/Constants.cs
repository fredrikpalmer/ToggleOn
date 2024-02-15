namespace ToggleOn.MassTransit;

public static class Constants
{
    public const string EnvironmentCreatedTopic = "sbt-environment-created";
    public const string FeatureToggleCreatedTopic = "sbt-feature-toggle-created";
    public const string FeatureToggleUpdatedTopic = "sbt-feature-toggle-updated";
    public const string CreateFeatureToggleTopic = "sbt-create-feature-toggle";
    public const string CreateFeatureToggleQueue = "sbq-create-feature-toggle";
    public const string FeatureGroupUpdatedTopic = "sbt-feature-group-updated";
}

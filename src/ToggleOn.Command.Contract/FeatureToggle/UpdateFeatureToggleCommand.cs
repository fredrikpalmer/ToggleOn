using System.Text.Json.Serialization;
using ToggleOn.Contract;
using ToggleOn.Http;

namespace ToggleOn.Command.Contract.FeatureToggle;

[ApiEndpoint("featuretoggles/{0}", nameof(Id))]
[UseHttpMethod("PUT")]
public class UpdateFeatureToggleCommand : ToggleOnCommand<VoidResult>
{
    public Guid Id { get; }
    public Guid FeatureId { get; }
    public Guid EnvironmentId { get; }
    public bool Enabled { get; }
    public IList<FeatureFilterDto> Filters { get; }

    [JsonConstructor]
    public UpdateFeatureToggleCommand(Guid id, Guid featureId, Guid environmentId, bool enabled, IList<FeatureFilterDto> filters)
    {
        Id = id;
        FeatureId = featureId;
        EnvironmentId = environmentId;
        Enabled = enabled;
        Filters = filters;
    }
}

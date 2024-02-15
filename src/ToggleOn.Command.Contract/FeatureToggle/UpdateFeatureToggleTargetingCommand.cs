using System.Text.Json.Serialization;
using ToggleOn.Http;

namespace ToggleOn.Command.Contract.FeatureToggle;

[ApiEndpoint("featuretoggles/{0}/enabled", nameof(FeatureToggleId))]
[UseHttpMethod("PATCH")]
public class UpdateFeatureToggleTargetingCommand : ToggleOnCommand<VoidResult>
{
    public Guid FeatureToggleId { get; }
    public bool Enabled { get; }

    [JsonConstructor]
    public UpdateFeatureToggleTargetingCommand(Guid featureToggleId, bool enabled)
    {
        FeatureToggleId = featureToggleId;
        Enabled = enabled;
    }
}

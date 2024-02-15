using System.Text.Json.Serialization;
using ToggleOn.Http;

namespace ToggleOn.Command.Contract.FeatureGroup;

[ApiEndpoint("featuregroups/{0}", nameof(Id))]
[UseHttpMethod("PUT")]
public class UpdateFeatureGroupCommand : ToggleOnCommand<VoidResult>
{
    public Guid Id { get; }
    public string? UserIds { get; set; }
    public string? IpAddresses { get; set; }

    [JsonConstructor]
    public UpdateFeatureGroupCommand(Guid id, string? userIds, string? ipAddresses)
    {
        Id = id;
        UserIds = userIds;
        IpAddresses = ipAddresses;
    }
}

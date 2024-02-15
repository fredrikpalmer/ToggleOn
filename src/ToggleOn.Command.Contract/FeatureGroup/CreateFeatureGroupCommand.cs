using System.Text.Json.Serialization;
using ToggleOn.Http;

namespace ToggleOn.Command.Contract.FeatureGroup;

[ApiEndpoint("featuregroups")]
public class CreateFeatureGroupCommand : ToggleOnCommand<VoidResult>
{
    public Guid Id { get; }
    public string Name { get; }
    public string? UserIds { get; set; }
    public string? IpAddresses { get; set; }

    [JsonConstructor]
    public CreateFeatureGroupCommand(Guid id, string name, string? userIds, string? ipAddresses)
    {
        Id = id;
        Name = name;
        UserIds = userIds;
        IpAddresses = ipAddresses;
    }
}

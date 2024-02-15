namespace ToggleOn.Client.Abstractions;

public class FeatureGroupConfiguration
{
    public string Name { get; }
    public string? UserIds { get; set; }
    public string? IpAddresses { get; set; }

    public FeatureGroupConfiguration(string name, string? userIds, string? ipAddresses)
    {
        Name = name;
        UserIds = userIds;
        IpAddresses = ipAddresses;
    }
}

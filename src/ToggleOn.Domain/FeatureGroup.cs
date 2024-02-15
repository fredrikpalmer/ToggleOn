namespace ToggleOn.Domain;

public class FeatureGroup
{
    public Guid Id { get; private set; } 
    public string Name { get; private set; }
    public string? UserIds { get; set; }
    public string? IpAddresses { get; set; }

    public FeatureGroup(Guid id, string name, string? userIds, string? ipAddresses) =>
        (Id, Name, UserIds, IpAddresses) = (id, name ?? throw new ArgumentNullException(nameof(name)), userIds, ipAddresses);
}

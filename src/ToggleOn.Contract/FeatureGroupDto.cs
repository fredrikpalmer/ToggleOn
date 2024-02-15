namespace ToggleOn.Contract;

public class FeatureGroupDto
{
    public Guid Id { get; }
    public string Name { get; }
    public string? UserIds { get; }
    public string? IpAddresses { get; }

    public FeatureGroupDto(Guid id, string name, string? userIds, string? ipAddresses)
    {
        Id = id;
        Name = name;
        UserIds = userIds;
        IpAddresses = ipAddresses;
    }
}
namespace ToggleOn.Admin.Web.FeatureGroup
{
    public class FeatureGroupViewModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? UserIds { get; set; }
        public string? IpAddresses { get; set; }

        public FeatureGroupViewModel(Guid id, string name, string? userIds, string? ipAddresses)
        {
            Id = id;
            Name = name;
            UserIds = userIds;
            IpAddresses = ipAddresses;
        }
    }
}

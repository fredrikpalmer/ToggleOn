using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

internal class DefaultFeatureGroupConfigurationAccessor : IFeatureGroupConfigurationAccessor
{
    private readonly IToggleOnDataProvider _provider;

    public DefaultFeatureGroupConfigurationAccessor(IToggleOnDataProvider provider)
    {
        _provider = provider;
    }

    public FeatureGroupConfiguration? GetFeatureGroup(string name)
    {
        return _provider.GetFeatureGroupConfiguration(name);
    }
}

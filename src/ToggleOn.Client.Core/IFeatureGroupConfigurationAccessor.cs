using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core;

public interface IFeatureGroupConfigurationAccessor
{
    FeatureGroupConfiguration? GetFeatureGroup(string name);
}

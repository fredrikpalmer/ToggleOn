namespace ToggleOn.Client.Abstractions;

public interface IFeatureToggle
{
    Task<bool> IsEnabledAsync(bool defaultValue = false);
}

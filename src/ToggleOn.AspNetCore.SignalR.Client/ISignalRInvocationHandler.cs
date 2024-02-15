using ToggleOn.Client.Contract;

namespace ToggleOn.AspNetCore.SignalR.Client;

public interface ISignalRInvocationHandler
{
    event EventHandler Initialized;
    Task InitializeAsync(InitializeDto initialize);
    Task UpdateAsync(FeatureToggleDto featureToggle);
    Task UpdateAsync(FeatureGroupDto featureGroup);
}

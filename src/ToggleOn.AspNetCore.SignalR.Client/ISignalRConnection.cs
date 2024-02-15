using ToggleOn.Client.Contract;

namespace ToggleOn.AspNetCore.SignalR.Client;

public interface ISignalRConnection
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task InitializeAsync(string project, string environment, CancellationToken cancellationToken = default);
    void OnInitialize(Func<InitializeDto, Task> handler);
    void OnUpdateToggle(Func<FeatureToggleDto, Task> handler);
    void OnUpdateGroup(Func<FeatureGroupDto, Task> handler);
    ValueTask DisposeAsync();
}

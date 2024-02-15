using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using ToggleOn.Client.Contract;

namespace ToggleOn.AspNetCore.SignalR.Client;

internal class SignalRConnection : ISignalRConnection
{
    private readonly HubConnection _connection;

    public SignalRConnection(IOptions<SignalRConnectionOptions> options, ITokenGenerator generator)
    {
        var url = $"{options.Value.Endpoint}/client/?hub=feature";
        _connection = new HubConnectionBuilder()
            .WithUrl(url, conn => conn.AccessTokenProvider = () => Task.FromResult(generator.Generate(url, options.Value.AccessKey!))!)
            .WithAutomaticReconnect()
            .Build();
    }

    public Task InitializeAsync(string project, string environment, CancellationToken cancellationToken = default)
    {
        return _connection.InvokeAsync("initialize", project, environment, cancellationToken);
    }

    public void OnInitialize(Func<InitializeDto, Task> handler)
    {
        _connection.On("initialize", handler);
    }

    public void OnUpdateToggle(Func<FeatureToggleDto, Task> handler)
    {
        _connection.On("feature-toggle-updated", handler);
    }

    public void OnUpdateGroup(Func<FeatureGroupDto, Task> handler)
    {
        _connection.On("feature-group-updated", handler);
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        return _connection.StartAsync(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _connection.DisposeAsync();
    }
}

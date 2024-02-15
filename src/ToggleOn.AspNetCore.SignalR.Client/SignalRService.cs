using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToggleOn.Client.Abstractions;
using ToggleOn.Client.Contract;

namespace ToggleOn.AspNetCore.SignalR.Client;

internal class SignalRService : IHostedLifecycleService
{
    private readonly ILogger<SignalRService> _logger;
    private readonly ISignalRConnection _connection;
    private readonly ISignalRInvocationHandler _handler;
    private readonly ToggleOnProviderOptions _options;
    //private readonly TaskCompletionSource _initializedSource = new();

    public SignalRService(IOptions<ToggleOnProviderOptions> options, ISignalRConnection connection, ISignalRInvocationHandler handler, ILogger<SignalRService> logger)
    {
        if (options is null) throw new ArgumentNullException(nameof(options));
        if(connection is null) throw new ArgumentNullException(nameof(connection));
        if(handler is null) throw new ArgumentNullException(nameof(handler));
        if (logger is null) throw new ArgumentNullException(nameof(logger));

        _options = options.Value;
        _logger = logger;
        _connection = connection;
        _handler = handler;
    }

    public Task StartingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application starting.");

        _connection.OnInitialize(_handler.InitializeAsync);
        _connection.OnUpdateToggle(_handler.UpdateAsync);
        _connection.OnUpdateGroup(_handler.UpdateAsync);

        return Task.CompletedTask;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application start.");

        try
        {
            //_handler.Initialized += (object? sender, EventArgs e) => _initializedSource.SetResult();

            await _connection.StartAsync(cancellationToken);
            await _connection.InitializeAsync(_options.Project, _options.Environment, cancellationToken);

            //using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            //var cancelledSource = new TaskCompletionSource();

            //cts.Token.Register(() => cancelledSource.SetResult());

            //await Task.WhenAny(_initializedSource.Task, cancelledSource.Task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start connection to SignalR");
            throw;
        }
    }


    public Task StartedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application started.");

        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken) 
    {
        _logger.LogInformation("Application stopping.");

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application stop.");

        await _connection.DisposeAsync();
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Application stopped.");

        return Task.CompletedTask;
    }
}

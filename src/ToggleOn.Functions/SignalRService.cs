using Microsoft.Azure.SignalR.Management;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ToggleOn.Functions;

public class SignalRService : IHostedService, IHubContextStore
{
    private const string Hub = "feature";
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;

    public ServiceHubContext? HubContext { get; private set; }

    public SignalRService(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _loggerFactory = loggerFactory;
    }

    async Task IHostedService.StartAsync(CancellationToken cancellationToken)
    {
        using var serviceManager = new ServiceManagerBuilder()
            .WithOptions(o => o.ConnectionString = _configuration["AzureSignalRConnectionString"])
            .WithLoggerFactory(_loggerFactory)
            .BuildServiceManager();
        HubContext = await serviceManager.CreateHubContextAsync(Hub, cancellationToken);
    }

    Task IHostedService.StopAsync(CancellationToken cancellationToken)
    {
        return Task.WhenAll(Dispose(HubContext));
    }

    private static Task Dispose(ServiceHubContext? hubContext)
    {
        if (hubContext is null)
        {
            return Task.CompletedTask;
        }
        return hubContext.DisposeAsync();
    }
}
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToggleOn.Core.Extensions;
using ToggleOn.Functions;
using ToggleOn.Functions.Consumers;
using ToggleOn.MassTransit.Extensions;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<IHubContextStore, SignalRService>();
        services.AddHostedService(sp => (sp.GetRequiredService<IHubContextStore>() as SignalRService)!);

        services.AddToggleOn(conf =>
        {
            conf.AddMassTransit(hostContext.Configuration["ServiceBusConnection:fullyQualifiedNamespace"]!, bus =>
            {
                bus.AddSubscription<FeatureToggleUpdatedConsumer>(hostContext.Configuration["WEBSITE_SITE_NAME"]!);
                bus.AddSubscription<FeatureGroupUpdatedConsumer>(hostContext.Configuration["WEBSITE_SITE_NAME"]!);
            });
            conf.AddHttpClient(hostContext.Configuration["ApiBaseAddress"]!);
        });
    })
    .Build();

host.Run();

using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Client.Abstractions;

namespace ToggleOn.AspNetCore.SignalR.Client.Extensions;

public static class ToggleOnClientConfiguratorExtensions
{
    public static void UseSignalR(this IToggleOnClientConfigurator configurator, string endpoint, string accessKey)
    {
        if(endpoint is null) throw new ArgumentNullException(nameof(endpoint));
        if(accessKey is null) throw new ArgumentNullException(nameof(accessKey));

        configurator.Services.Configure<SignalRConnectionOptions>(options =>
        {
            options.Endpoint = endpoint;
            options.AccessKey = accessKey;
        });
        configurator.Services.AddSingleton<ITokenGenerator, DefaultTokenGenerator>();
        configurator.Services.AddSingleton<ISignalRConnection, SignalRConnection>();
        configurator.Services.AddSingleton<IToggleOnDataProvider, SignalRDataProvider>();
        configurator.Services.AddSingleton(sp => (ISignalRInvocationHandler)sp.GetRequiredService<IToggleOnDataProvider>());
        configurator.Services.AddHostedService<SignalRService>();
    }
}

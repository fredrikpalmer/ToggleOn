using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Abstractions;
using ToggleOn.Command.Client.Extensions;
using ToggleOn.Query.Client.Extensions;

namespace ToggleOn.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddToggleOn(this IServiceCollection services, Action<IToggleOnAdminConfigurator> configurator)
    {
        if (configurator is null) throw new ArgumentNullException(nameof(configurator));

        var instance = new DefaultToggleOnAdminConfigurator(services);

        configurator.Invoke(instance);

        services.AddQueryClient();
        services.AddCommandClient();

        return services;
    }
}
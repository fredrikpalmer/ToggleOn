using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Client.Abstractions;

namespace ToggleOn.Client.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddToggleOn(this IServiceCollection services, Action<IToggleOnClientConfigurator> configurator)
    {
        if (configurator is null) throw new ArgumentNullException(nameof(configurator));

        var instance = new DefaultToggleOnClientConfigurator(services);

        configurator.Invoke(instance);

        services.AddHttpContextAccessor();
        services.AddSingleton<IFeatureEnricher, HttpContextEnricher>();

        services.AddSingleton<IFeatureGroupConfigurationAccessor, DefaultFeatureGroupConfigurationAccessor>();
        services.AddSingleton<IFeatureToggleBuilder, DefaultFeatureToggleBuilder>();

        services.AddSingleton<IFeatureFilter, PercentageFilter>();
        services.AddSingleton<IFeatureFilter, FeatureGroupFilter>();
        services.AddSingleton<IFeatureFilter, QueryStringFilter>();

        return services;
    }

    public static IServiceCollection AddFeatureToggle<TClient, TImplementation>(this IServiceCollection services, Action<IFeatureToggleConfigurator> configure) where TClient : class where TImplementation : class, TClient
    {
        if (configure is null) throw new ArgumentNullException(nameof(configure));

        var configurator = new DefaultFeatureToggleConfigurator(services);

        configure.Invoke(configurator);

        configurator.AddFeatureToggle<TClient, TImplementation>();

        return services;
    }
}
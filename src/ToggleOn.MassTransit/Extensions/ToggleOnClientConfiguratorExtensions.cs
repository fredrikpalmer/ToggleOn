using Azure.Identity;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Abstractions;
using ToggleOn.Messaging.Contract;

namespace ToggleOn.MassTransit.Extensions;

public static partial class ToggleOnClientConfiguratorExtensions
{
    public static void AddMassTransit(this IToggleOnAdminConfigurator configurator, string serviceBusHostname, Action<IMassTransitBusConfigurator> configure)
    {
        if (serviceBusHostname is null) throw new ArgumentNullException(nameof(serviceBusHostname));
        if (configure is null) throw new ArgumentNullException(nameof(configure));

        configurator.Services.AddScoped<IToggleOnEventPublisher, MassTransitToggleOnEventPublisher>();
        configurator.Services.AddScoped<IToggleOnMessageSender, MassTransitToggleOnMessageSender>();

        configurator.Services.AddMassTransit(conf =>
        {
            var configurator = new DefaultMassTransitBusConfigurator(conf);
            configure.Invoke(configurator);

            conf.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(includeNamespace: false));

            conf.UsingAzureServiceBus((context, sb) =>
            {
                sb.Host(new Uri($"sb://{serviceBusHostname}"));

                sb.Message<EnvironmentCreated>(m => m.SetEntityName(Constants.EnvironmentCreatedTopic));
                sb.Message<FeatureToggleCreated>(m => m.SetEntityName(Constants.FeatureToggleCreatedTopic));
                sb.Message<CreateFeatureToggle>(m => m.SetEntityName(Constants.CreateFeatureToggleTopic));
                sb.Message<FeatureToggleUpdated>(m => m.SetEntityName(Constants.FeatureToggleUpdatedTopic));
                sb.Message<FeatureGroupUpdated>(m => m.SetEntityName(Constants.FeatureGroupUpdatedTopic));  

                foreach (var (consumerType, subscription) in configurator.Subscriptions)
                {
                    var method = configurator.GetType().GetMethod(nameof(configurator.ConfigureSubscription)) ?? throw new InvalidOperationException();
                    method.MakeGenericMethod(consumerType).Invoke(configurator, new object[] { context, sb, subscription });
                }

                foreach (var consumerType in configurator.Receivers)
                {
                    var method = configurator.GetType().GetMethod(nameof(configurator.ConfigureReceive)) ?? throw new InvalidOperationException();
                    method.MakeGenericMethod(consumerType).Invoke(configurator, new object[] { context, sb });
                }
            });

        });
    }
}

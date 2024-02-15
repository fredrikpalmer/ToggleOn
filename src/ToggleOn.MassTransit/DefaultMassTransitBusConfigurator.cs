using MassTransit;
using System.Reflection;

namespace ToggleOn.MassTransit;

internal class DefaultMassTransitBusConfigurator : IMassTransitBusConfigurator
{
    private readonly IBusRegistrationConfigurator _conf;
    public IList<(Type consumerType, string subscriptionName)> Subscriptions { get; } = new List<(Type, string)>();
    public IList<Type> Receivers { get; } = new List<Type>();

    public DefaultMassTransitBusConfigurator(IBusRegistrationConfigurator configurator)
    {
        _conf = configurator;
    }

    public void AddSubscription<TConsumer>(string subscription) where TConsumer : class, IConsumer
    {
        if(subscription is null) throw new ArgumentNullException(nameof(subscription));

        _conf.AddConsumer<TConsumer>();

        Subscriptions.Add((typeof(TConsumer), subscription));
    }

    public void AddReceiver<TConsumer>() where TConsumer : class, IConsumer
    {
        _conf.AddConsumer<TConsumer>();

        Receivers.Add(typeof(TConsumer));
    }

    public void ConfigureSubscription<TConsumer>(IBusRegistrationContext context, IServiceBusBusFactoryConfigurator sb, string subscription)
        where TConsumer : class, IConsumer
    {
        var attribute = typeof(TConsumer).GetCustomAttribute<SubscriptionEndpointAttribute>() ?? throw new InvalidOperationException();
        sb.SubscriptionEndpoint(subscription, attribute.TopicName, e => e.ConfigureConsumer<TConsumer>(context));
    }

    public void ConfigureReceive<TConsumer>(IBusRegistrationContext context, IServiceBusBusFactoryConfigurator sb)
        where TConsumer : class, IConsumer
    {
        var attribute = typeof(TConsumer).GetCustomAttribute<ReceiveEndpointAttribute>() ?? throw new InvalidOperationException();
        sb.ReceiveEndpoint(attribute.QueueName, e =>
        {
            e.Subscribe(attribute.TopicName, attribute.QueueName);
            e.ConfigureConsumer<TConsumer>(context);
        });
    }
}

using MassTransit;

namespace ToggleOn.MassTransit;

public interface IMassTransitBusConfigurator
{
    void AddSubscription<TConsumer>(string subscription) where TConsumer : class, IConsumer;
    void AddReceiver<TConsumer>() where TConsumer : class, IConsumer;
}


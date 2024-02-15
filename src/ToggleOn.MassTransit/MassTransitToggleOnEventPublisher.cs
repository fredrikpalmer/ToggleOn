using MassTransit;
using ToggleOn.Abstractions;

namespace ToggleOn.MassTransit;

internal class MassTransitToggleOnEventPublisher : IToggleOnEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitToggleOnEventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}

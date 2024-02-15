using MassTransit;
using ToggleOn.Abstractions;

namespace ToggleOn.MassTransit;

internal class MassTransitToggleOnMessageSender : IToggleOnMessageSender
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public MassTransitToggleOnMessageSender(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task Send<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class
    {
        await _sendEndpointProvider.Send(message, cancellationToken);
    }
}

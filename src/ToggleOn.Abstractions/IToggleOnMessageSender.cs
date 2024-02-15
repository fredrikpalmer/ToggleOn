namespace ToggleOn.Abstractions;

public interface IToggleOnMessageSender
{
    Task Send<TMessage>(TMessage message, CancellationToken cancellationToken= default) where TMessage : class;
}
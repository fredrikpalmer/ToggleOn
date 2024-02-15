namespace ToggleOn.Abstractions;

public interface IToggleOnEventPublisher
{
    Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class;
}

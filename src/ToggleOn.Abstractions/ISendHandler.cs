namespace ToggleOn.Abstractions;

public interface ISendHandler
{
    Task<TResult> HandleAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);

    Task<TResult> HandleAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
}

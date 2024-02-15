using ToggleOn.Abstractions;
using ToggleOn.Command.Contract;

namespace ToggleOn.Command.Client;

internal class DefaultToggleOnCommandClient : IToggleOnCommandClient
{
    private ISendHandler _handler;

    public DefaultToggleOnCommandClient(ISendHandler handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    public Task<TResult> ExecuteAsync<TResult>(ToggleOnCommand<TResult> command, CancellationToken cancellationToken = default)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));

        return _handler.HandleAsync(command, cancellationToken);
    }
}
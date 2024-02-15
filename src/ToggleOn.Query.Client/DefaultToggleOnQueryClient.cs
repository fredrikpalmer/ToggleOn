using ToggleOn.Abstractions;
using ToggleOn.Query.Contract;

namespace ToggleOn.Query.Client;

internal class DefaultToggleOnQueryClient : IToggleOnQueryClient
{
    private readonly ISendHandler _handler;

    public DefaultToggleOnQueryClient(ISendHandler handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    public Task<TResult> ExecuteAsync<TResult>(ToggleOnQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        if (query == null) throw new ArgumentNullException(nameof(query));

        return _handler.HandleAsync(query, cancellationToken);
    }
}

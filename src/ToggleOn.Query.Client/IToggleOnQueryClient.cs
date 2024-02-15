using ToggleOn.Query.Contract;

namespace ToggleOn.Query.Client;

public interface IToggleOnQueryClient
{
    Task<TResult> ExecuteAsync<TResult>(ToggleOnQuery<TResult> query, CancellationToken cancellationToken = default);
}

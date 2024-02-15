using ToggleOn.Abstractions;
using ToggleOn.Command.Contract;

namespace ToggleOn.Command.Client;

public interface IToggleOnCommandClient
{
    Task<TResult> ExecuteAsync<TResult>(ToggleOnCommand<TResult> command, CancellationToken cancellationToken = default);
}
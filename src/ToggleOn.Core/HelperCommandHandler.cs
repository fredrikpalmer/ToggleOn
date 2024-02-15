using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Abstractions;

namespace ToggleOn.Core;

internal abstract class HelperCommandHandler
{
    public abstract Task<object?> ExecuteCommandAsync(object command, IServiceProvider serviceProvider, CancellationToken cancellationToken = default);
}

internal abstract class HelperCommandHandler<TResult> : HelperCommandHandler
{
    public abstract Task<TResult> HandleAsync(ICommand<TResult> command, IServiceProvider serviceProvider, CancellationToken cancellationToken = default);
}

internal class HelperCommandHandler<TCommand, TResult> : HelperCommandHandler<TResult> where TCommand : ICommand<TResult>
{
    public override async Task<object?> ExecuteCommandAsync(object command, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        return await HandleAsync((ICommand<TResult>)command, serviceProvider, cancellationToken);
    }

    public override async Task<TResult> HandleAsync(ICommand<TResult> command, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

        return await handler.HandleAsync((TCommand)command, cancellationToken);
    }
}
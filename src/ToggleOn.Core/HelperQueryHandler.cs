using Microsoft.Extensions.DependencyInjection;
using ToggleOn.Abstractions;

namespace ToggleOn.Core;

internal abstract class HelperQueryHandler
{
    public abstract Task<object> HandleAsync(object query, IServiceProvider serviceProvider, CancellationToken cancellationToken = default);
}

internal abstract class HelperQueryHandler<TResult> : HelperQueryHandler
{
    public abstract Task<TResult> HandleAsync(IQuery<TResult> query, IServiceProvider serviceProvider, CancellationToken cancellationToken = default);
}

internal class HelperQueryHandler<TQuery, TResult> : HelperQueryHandler<TResult> where TQuery : IQuery<TResult>
{
    public override async Task<object> HandleAsync(object query, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        return (await HandleAsync((IQuery<TResult>)query, serviceProvider, cancellationToken))!;
    }

    public override async Task<TResult> HandleAsync(IQuery<TResult> query, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.HandleAsync((TQuery)query, cancellationToken);
    }
}
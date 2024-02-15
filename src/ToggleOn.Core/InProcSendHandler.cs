using ToggleOn.Abstractions;

namespace ToggleOn.Core;

internal class InProcSendHandler : ISendHandler
{
    private readonly IServiceProvider _serviceProvider;

    public InProcSendHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public Task<TResult> HandleAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        if (query is null) throw new ArgumentNullException(nameof(query));

        var type = typeof(HelperQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = Activator.CreateInstance(type) as HelperQueryHandler<TResult>;
        if (handler is null) throw new InvalidOperationException(); 

        return handler.HandleAsync(query, _serviceProvider, cancellationToken);
       
    }

    public Task<TResult> HandleAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));

        var type = typeof(HelperCommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        var handler = Activator.CreateInstance(type) as HelperCommandHandler<TResult>;
        if (handler is null) throw new InvalidOperationException();


        return handler.HandleAsync(command, _serviceProvider, cancellationToken);
    }
}
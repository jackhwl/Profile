using Microsoft.Extensions.DependencyInjection;

namespace Wenlin.Application.Contracts.Infrastructure;

public interface IQueryHandler<in TQuery, TQueryResult>
{
    Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
}

public interface IQueryDispatcher
{
    Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation=default);
}

public interface ICommandHandler<in TCommand, TCommandResult>
{
    Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
}

public interface ICommandDispatcher
{
    Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation);
}

class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
        return handler.Handle(query, cancellation);
    }
}
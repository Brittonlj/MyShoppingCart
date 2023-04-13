using Microsoft.Extensions.Logging;

namespace MyShoppingCart.Application.PipelineBehaviors;

public sealed class ExceptionLoggingPipelineBehavior<TRequest, TEntity> :
    IPipelineBehavior<TRequest, Response<TEntity>>
    where TRequest : IRequest<Response<TEntity>>
    where TEntity : class
{
    private readonly ILogger<TRequest> _logger;

    public ExceptionLoggingPipelineBehavior(ILogger<TRequest> logger)
    {
        _logger = Guard.Against.Null(logger);
    }

    public async Task<Response<TEntity>> Handle(
        TRequest request, 
        RequestHandlerDelegate<Response<TEntity>> next, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception was caught.");
            return new ErrorList(ex);
        }
    }
}
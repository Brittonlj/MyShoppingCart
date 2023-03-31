﻿using Microsoft.Extensions.Logging;

namespace MyShoppingCart.Application.PipelineBehaviors;

public sealed class ExceptionLoggingPipelineBehavior<TRequest, TEntity> :
    IPipelineBehavior<TRequest, Response<TEntity>>
    where TRequest : IRequest<Response<TEntity>>
    where TEntity : class
{
    private readonly ILogger<TRequest> _logger;

    public ExceptionLoggingPipelineBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<Response<TEntity>> Handle(
        TRequest request, 
        RequestHandlerDelegate<Response<TEntity>> next, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error caught in pipeline: {message}", ex.Message);
            return new ErrorList(ex);
        }
    }
}
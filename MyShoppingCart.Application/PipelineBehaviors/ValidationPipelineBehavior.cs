namespace MyShoppingCart.Application.PipelineBehaviors;

public sealed class ValidationPipelineBehavior<TRequest, TEntity> :
    IPipelineBehavior<TRequest, Response<TEntity>>
    where TRequest : IQuery<TEntity>
    where TEntity : class

{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<Response<TEntity>> Handle(
        TRequest request, 
        RequestHandlerDelegate<Response<TEntity>> next, 
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var failure = Validate(request);

        if (failure is not null)
        {
            return failure;
        }

        return await next();
    }

    private ValidationFailure? Validate(TRequest request)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationErrors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        if (validationErrors.Any())
        {
            return new ValidationFailure(validationErrors);
        }
        return null;
    }
}

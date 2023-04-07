using FluentValidation.Results;

namespace MyShoppingCart.Application.Orders;

public sealed class CreateOrderQueryValidator : AbstractValidator<CreateOrderQuery>
{
    private readonly IRepository<Product> _productRepository;

    public CreateOrderQueryValidator(IRepository<Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(productRepository));

        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.LineItems).NotEmpty();
    }

    public override async Task<ValidationResult> ValidateAsync(ValidationContext<CreateOrderQuery> context, CancellationToken cancellation = default)
    {
        await CheckForInvalidProductIds(context, cancellation);

        return await base.ValidateAsync(context, cancellation);
    }

    private async Task CheckForInvalidProductIds(ValidationContext<CreateOrderQuery> context, CancellationToken cancellation = default)
    {
        var request = context.InstanceToValidate;
        var productIds = request.LineItems.Select(x => x.ProductId).ToList();
        var query = new QueryAllProductsByProductIds(productIds).WithNoTracking();

        var products = await _productRepository.ListAsync(query, cancellation);

        var missingProductIds = request.LineItems.Where(x => !products.Any(p => p.Id == x.ProductId)).ToList();

        foreach (var missing in missingProductIds)
        {
            context.AddFailure(new FluentValidation.Results.ValidationFailure("ProductId", $"The ProductId '{missing.ProductId}' was not found."));
        }

    }

}

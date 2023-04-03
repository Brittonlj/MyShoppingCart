using FluentValidation.Results;

namespace MyShoppingCart.Application.Orders;

public sealed class CreateOrderQueryValidator : AbstractValidator<CreateOrderQuery>
{
    private readonly IUnitOfWork _context;

    public CreateOrderQueryValidator(IUnitOfWork context)
    {
        _context = context;

        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.LineItems).NotEmpty();
    }


    protected override bool PreValidate(ValidationContext<CreateOrderQuery> context, ValidationResult result)
    {
        CheckForInvalidProductIds(context);

        return base.PreValidate(context, result);
    }

    private void CheckForInvalidProductIds(ValidationContext<CreateOrderQuery> context)
    {
        var query = context.InstanceToValidate;
        var products = _context.Products
            .Where(x => query.LineItems.Select(y => y.ProductId).Contains(x.Id))
            .AsNoTracking()
            .ToList();

        var missingProductIds = query.LineItems.Where(x => !products.Any(p => p.Id == x.ProductId)).ToList();

        foreach (var missing in missingProductIds)
        {
            context.AddFailure(new FluentValidation.Results.ValidationFailure("ProductId", $"The ProductId '{missing.ProductId}' was not found."));
        }

    }

}

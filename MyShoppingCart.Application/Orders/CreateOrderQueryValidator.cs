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
        CheckForDuplicateProductIds(context);
        CheckForInvalidProductIds(context);

        return base.PreValidate(context, result);
    }

    private void CheckForDuplicateProductIds(ValidationContext<CreateOrderQuery> context)
    {
        var dupes = context.InstanceToValidate.LineItems.GroupBy(x => x.ProductId).Where(x => x.Count() > 1);

        foreach (var dupe in dupes)
        {
            context.AddFailure(new FluentValidation.Results.ValidationFailure("LineItems", $"The ProductId ('{dupe.Key}') is contained in more than one line item."));
        }
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

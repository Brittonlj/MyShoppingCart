﻿namespace MyShoppingCart.Application.Orders;

public sealed class UpdateOrderQueryValidator : AbstractValidator<UpdateOrderQuery>
{
    public UpdateOrderQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.OrderId).NotEmpty();
        RuleForEach(x => x.LineItems).NotEmpty().SetValidator(new LineItemValidator());
    }
}

namespace MyShoppingCart.Application.Customers;

public sealed record GetCustomerSecurityQuery(Guid CustomerId) : IQueryMany<SecurityClaim>
{
}

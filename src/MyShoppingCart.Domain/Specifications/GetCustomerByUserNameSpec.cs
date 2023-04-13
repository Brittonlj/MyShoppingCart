namespace MyShoppingCart.Domain.Specifications;

public sealed class GetCustomerByUserNameSpec : BaseSpecification<Customer>, ISingleResultSpecification
{
    public GetCustomerByUserNameSpec(string userName)
    {
        Query
            .Where(x => x.UserName == userName)
            .Include(x => x.ShippingAddress)
            .Include(x => x.BillingAddress)
            .AsSplitQuery();
    }
}
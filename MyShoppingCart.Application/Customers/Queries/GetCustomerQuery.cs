namespace MyShoppingCart.Application.Customers.Queries;

public sealed record GetCustomerQuery(Guid CustomerId, Guid? RequestingUser = null) : 
    IQuery<Customer>
{
}

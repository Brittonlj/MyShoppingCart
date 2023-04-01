namespace MyShoppingCart.Application.Customers.Commands;

public sealed record UpdateCustomerCommand(Customer Customer, Guid? RequestingCustomerId = null) : 
    IQuery<Success>
{
}

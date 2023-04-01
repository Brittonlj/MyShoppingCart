namespace MyShoppingCart.Application.Orders.Commands;

public sealed record DeleteCustomerOrderCommand(Guid CustomerId, Guid OrderId, Guid? RequestingCustomerId = null) : IQuery<Success>
{
}

namespace MyShoppingCart.Application.Orders.Commands;

public sealed record DeleteOrderCommand(Guid OrderId, Guid? RequestingCustomerId = null) : IQuery<Success>
{
}

namespace MyShoppingCart.Application.Orders.Queries;

public sealed record GetOrderQuery(Guid OrderId, Guid? CustomerId = null, Guid? RequestingUserId = null) : 
    IRequest<Response<Order>>
{
}

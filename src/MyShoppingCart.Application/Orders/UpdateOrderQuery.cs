using MyShoppingCart.Domain.Models;

namespace MyShoppingCart.Application.Orders;

public sealed record UpdateOrderQuery(
    Guid CustomerId,
    Guid OrderId,
    IReadOnlyList<LineItemModel> LineItems) :
    IQuery<Order>,
    IAuthorizedCustomerRequest
{
}

using MyShoppingCart.Domain.Models;

namespace MyShoppingCart.Application.Orders;

public sealed record CreateOrderQuery(
    Guid CustomerId,
    IReadOnlyList<NewLineItemModel> LineItems) :
    IQuery<Order>,
    IAuthorizedCustomerRequest
{
}

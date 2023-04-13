namespace MyShoppingCart.Application.Orders;

public sealed record CreateOrderQuery(
    Guid CustomerId,
    IReadOnlyList<LineItemModel> LineItems) :
    IQuery<Order>,
    IAuthorizedCustomerRequest
{
}

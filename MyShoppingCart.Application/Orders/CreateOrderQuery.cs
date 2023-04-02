public sealed record CreateOrderQuery(
    Guid CustomerId,
    IReadOnlyList<Guid> ProductIds) :
    IQuery<OrderModel>,
    IAuthorizedCustomerRequest
{
}

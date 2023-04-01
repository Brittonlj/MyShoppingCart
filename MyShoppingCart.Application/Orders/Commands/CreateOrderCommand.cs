public sealed record CreateOrderCommand(Guid CustomerId, Order Order) :
    IQuery<Success>,
    IAuthorizedCustomerRequest
{
}

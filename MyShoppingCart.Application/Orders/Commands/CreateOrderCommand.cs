public sealed record CreateOrderCommand(Order Order, Guid? RequestingCustomerId = null) : 
    IRequest<Response<Success>>
{
}

public sealed record CreateOrderCommand(Order Order, Guid? RequestingCustomerId = null) : 
    IQuery<Success>
{
}

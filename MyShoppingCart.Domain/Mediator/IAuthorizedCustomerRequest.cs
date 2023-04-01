namespace MyShoppingCart.Domain.Mediator;

public interface IAuthorizedCustomerRequest
{
    public Guid CustomerId { get; init; }
}

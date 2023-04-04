namespace MyShoppingCart.Application.PipelineBehaviors;

public interface IAuthorizedCustomerRequest
{
    public Guid CustomerId { get; init; }
}

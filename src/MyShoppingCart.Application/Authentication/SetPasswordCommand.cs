namespace MyShoppingCart.Application.Authentication;

public sealed record SetPasswordCommand(
    Guid CustomerId,
    string Password) :
    ICommand, IAuthorizedCustomerRequest
{
}

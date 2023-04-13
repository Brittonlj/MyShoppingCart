namespace MyShoppingCart.Application.Authentication;

public sealed record ChangePasswordCommand(
    Guid CustomerId,
    string CurrentPassword,
    string NewPassword) :
    ICommand, IAuthorizedCustomerRequest
{
}

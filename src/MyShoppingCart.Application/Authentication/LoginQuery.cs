namespace MyShoppingCart.Application.Authentication;

public sealed record LoginQuery(
    string UserName,
    string Password) : 
    IQuery<AuthenticationResponseModel>
{
}

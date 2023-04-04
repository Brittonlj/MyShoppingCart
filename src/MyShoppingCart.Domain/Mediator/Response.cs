using OneOf;

namespace MyShoppingCart.Domain.Mediator;

[GenerateOneOf]
public partial class Response<TSuccess> : 
    OneOfBase<TSuccess, Unauthorized, NotFound, ErrorList, ValidationFailure>
    where TSuccess : class
{
}

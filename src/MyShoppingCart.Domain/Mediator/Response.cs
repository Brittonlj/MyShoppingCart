using OneOf;
namespace MyShoppingCart.Domain.Mediator;

[GenerateOneOf]
public partial class Response<TSuccess> : 
    OneOfBase<TSuccess, Unauthorized, NotFound, ErrorList, ValidationFailure>
    where TSuccess : class
{
    public TSuccess Success  => AsT0;
    public Unauthorized Unauthorized => AsT1;
    public NotFound NotFound => AsT2;
    public ErrorList ErrorList => AsT3;
    public ValidationFailure ValidationFailure => AsT4;

    public static Response<TSuccess> FromSuccess(TSuccess success)
    {
        return new Response<TSuccess>(
            OneOf<TSuccess, Unauthorized, NotFound, ErrorList, ValidationFailure>.FromT0(success));
    }

    public static Response<TSuccess> FromUnauthorized(Unauthorized? unauthorized = null) 
    {
        unauthorized ??= Unauthorized.Instance;
        return new Response<TSuccess>(
            OneOf<TSuccess, Unauthorized, NotFound, ErrorList, ValidationFailure>.FromT1(unauthorized));
    }

    public static Response<TSuccess> FromNotFound(NotFound? notFound = null)
    {
        notFound ??= NotFound.Instance;
        return new Response<TSuccess>(
            OneOf<TSuccess, Unauthorized, NotFound, ErrorList, ValidationFailure>.FromT2(notFound));
    }

    public static Response<TSuccess> FromErrorList(ErrorList errors)
    {
        return new Response<TSuccess>(
            OneOf<TSuccess, Unauthorized, NotFound, ErrorList, ValidationFailure>.FromT3(errors));
    }

    public static Response<TSuccess> FromValidationFailure(Dictionary<string, string[]> errors)
    {
        return new Response<TSuccess>(
            OneOf<TSuccess, Unauthorized, NotFound, ErrorList, ValidationFailure>
            .FromT4(new ValidationFailure(errors)));
    }
}

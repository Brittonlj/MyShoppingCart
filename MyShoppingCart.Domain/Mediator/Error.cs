using System.Text.Json;

namespace MyShoppingCart.Domain.Mediator;

public sealed record class Error(string Code, string Message)
{
    public const string NOT_FOUND_CODE = "NotFound";
    public const string UNAUTHORIZED_CODE = "Unauthorized";
    public const string EXCEPTION_CODE = "Exception";

    public readonly static Error CustomerNotFound = new Error(NOT_FOUND_CODE, "Customer Not Found.");
    public readonly static Error InvalidCustomerId = new Error("InvalidCustomerId", "Invalid Customer Id.");

    public readonly static Error OrderNotFound = new Error(NOT_FOUND_CODE, "Order Not Found.");
    public readonly static Error InvalidOrderId = new Error("InvalidOrderId", "Invalid Order Id.");

    public readonly static Error Unauthorized = new Error(UNAUTHORIZED_CODE, "Unauthorized.");
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

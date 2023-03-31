namespace MyShoppingCart.Domain.Mediator;

public sealed class ValidationFailure
{
    public Dictionary<string, string[]> Results { get; }

    public ValidationFailure(Dictionary<string, string[]> results)
    {
        Results = results;
    }
}

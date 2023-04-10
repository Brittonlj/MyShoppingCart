using System.Diagnostics.CodeAnalysis;

namespace MyShoppingCart.Domain.ValueObjects;

public sealed class ValidationFailure
{
    public Dictionary<string, string[]> Results { get; }

    [SetsRequiredMembers]
    public ValidationFailure(Dictionary<string, string[]> results)
    {
        Results = results;
    }
}

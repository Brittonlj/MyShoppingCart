using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MyShoppingCart.Domain.ResponseObjects;

public sealed class ValidationFailure
{
    public Dictionary<string, string[]> Results { get; }

    [SetsRequiredMembers]
    public ValidationFailure(Dictionary<string, string[]> results)
    {
        Results = results;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var key in Results.Keys)
        {
            var errors = Results[key];
            foreach(var error in errors)
            {
                sb.AppendLine($"[{key}] {error}");
            }
        }
        return sb.ToString();
    }
}

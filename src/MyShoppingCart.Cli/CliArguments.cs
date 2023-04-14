namespace MyShoppingCart.Cli;

internal sealed class CliArguments
{
    public string? CustomerId { get; set; }
    public Action Action { get; set; }
}

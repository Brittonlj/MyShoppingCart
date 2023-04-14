namespace MyShoppingCart.Cli.Handlers;

internal interface ICommandHandler
{
    Task<string> Handle(CliArguments args);
}

using Microsoft.Extensions.DependencyInjection;

namespace MyShoppingCart.Cli.Handlers;

internal class CommandHandlerFactory
{
    private readonly IServiceProvider _provider;
public CommandHandlerFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<string> Handle(CliArguments args)
    {
        ICommandHandler handler;

        switch (args.Action)
        {
            case Action.AddCustomer:
                handler = _provider.GetRequiredService<AddCustomerHandler>();
                return await handler.Handle(args);
            case Action.DeleteCustomer:
                handler = _provider.GetRequiredService<DeleteCustomerHandler>();
                return await handler.Handle(args);
            case Action.SetPassword:
                handler = _provider.GetRequiredService<SetPasswordHandler>();
                return await handler.Handle(args);
            default:
                return "Unknown action.";
        }
    }
}

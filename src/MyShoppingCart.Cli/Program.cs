using Fclp;
using MyShoppingCart.Cli;
using MyShoppingCart.Cli.Handlers;
using MyShoppingCart.Cli.Setup;

var (services, configuration) = SetupCli.GetSettingsAndConfiguration(args);

var parser = new FluentCommandLineParser<CliArguments>();

parser.Setup(x => x.Action)
    .As('a', "action")
    .Required();

parser.Setup(x => x.CustomerId)
    .As('i', "id");

var result = parser.Parse(args);

if (result.HasErrors == true)
{
    Console.WriteLine("Usage: MyShoppingCart -a <AddCustomer,DeleteCustomer,SetPassword> [-i CustomerGuid]");
    Console.WriteLine(result.ErrorText);
    return;
}

var factory = new CommandHandlerFactory(services);
var output =  await factory.Handle(parser.Object);

Console.WriteLine(output);


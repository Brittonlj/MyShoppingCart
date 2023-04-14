using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyShoppingCart.Domain.Setup;
using MyShoppingCart.Infrastructure.Setup;
using MyShoppingCart.Application.Setup;
using MyShoppingCart.Cli.Handlers;

namespace MyShoppingCart.Cli.Setup;

internal static class SetupCli
{
    public static (IServiceProvider settings, IConfigurationRoot configuration) 
    GetSettingsAndConfiguration(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.SetupMyShoppingCartDomain();
                services.SetupMyShoppingCartInfrastructure(configuration);
                services.SetupMyShoppingCartApplication(false);
                services.AddScoped<AddCustomerHandler>();
                services.AddScoped<DeleteCustomerHandler>();
                services.AddScoped<SetPasswordHandler>();
            })
            .Build();

        IServiceScope serviceScope = host.Services.CreateScope();
        var services = serviceScope.ServiceProvider;

        
        //await host.RunAsync();

        return (services, configuration);
    }
}

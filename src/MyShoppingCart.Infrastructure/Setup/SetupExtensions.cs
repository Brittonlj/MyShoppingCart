using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShoppingCart.Domain.Repositories;
using MyShoppingCart.Infrastructure.Repositories;

namespace MyShoppingCart.Infrastructure.Setup;

public static class SetupExtensions
{
    public const string CONNECTION_STRING_NAME = "MyShoppingCart";

    public static IServiceCollection SetupMyShoppingCartInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Load the connection string from the appsettings.json file
        var connectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME);

        // Add the MyShoppingCartContext dbContext and put it in DI
        services.AddDbContext<MyShoppingCartContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // Add dependency injection for dependencies in the Infrastructure project
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}

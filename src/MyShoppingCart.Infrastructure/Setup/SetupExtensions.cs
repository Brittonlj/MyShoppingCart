using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShoppingCart.Domain.Data;

namespace MyShoppingCart.Infrastructure.Setup;

public static class SetupExtensions
{
    public const string CONNECTION_STRING_NAME = "MyShoppingCart";

    public static IServiceCollection SetupMyShoppingCartInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<MyShoppingCartContext>(options =>
        {
            var connectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME);
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork, MyShoppingCartContext>();


        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;
using MyShoppingCart.Domain.Utilities;

namespace MyShoppingCart.Domain.Setup;

public static class SetupExtensions
{
    public static IServiceCollection SetupMyShoppingCartDomain(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IMyShoppingCartDomainMarker>();

        services.AddSingleton<IUtcDateTimeProvider, UtcDateTimeProvider>();
    
        return services;
    }
}

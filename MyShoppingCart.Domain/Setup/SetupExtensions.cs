using Microsoft.Extensions.DependencyInjection;

namespace MyShoppingCart.Domain.Setup;

public static class SetupExtensions
{
    public static IServiceCollection SetupMyShoppingCartDomain(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IMyShoppingCartDomainMarker>();

        return services;
    }
}

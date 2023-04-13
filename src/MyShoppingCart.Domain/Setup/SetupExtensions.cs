using Microsoft.Extensions.DependencyInjection;
using MyShoppingCart.Domain.Utilities;

namespace MyShoppingCart.Domain.Setup;

public static class SetupExtensions
{
    public static IServiceCollection SetupMyShoppingCartDomain(this IServiceCollection services)
    {
        // Add fluent validation for the Domain project
        services.AddValidatorsFromAssemblyContaining<IMyShoppingCartDomainMarker>();

        // Add dependency injection for dependencies in the Domain project
        services.AddSingleton<IUtcDateTimeProvider, UtcDateTimeProvider>();
    
        return services;
    }
}

using MyShoppingCart.Api.Endpoints;

namespace MyShoppingCart.Api.Setup;

public static class SetupExtensions
{
    public static WebApplication RegisterMyShoppingCartEndpoints(this WebApplication app)
    {
        CustomerEndpoints.RegisterEndpoints(app);
        OrderEndpoints.RegisterEndpoints(app);
        ProductsEndpoints.RegisterEndpoints(app);

        return app;
    }

    public static IServiceCollection SetupMyShoppingCartApi(
        this IServiceCollection services, 
        ConfigurationManager config)
    {
        services.Configure<MyShoppingCartSettings>(config.GetSection(MyShoppingCartSettings.SECTION_NAME));

        return services;
    }



}

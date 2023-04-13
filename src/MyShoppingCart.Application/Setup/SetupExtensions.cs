using Microsoft.Extensions.DependencyInjection;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Setup;

public static class SetupExtensions
{
    public static IServiceCollection SetupMyShoppingCartApplication(this IServiceCollection services)
    {
        // Add fluent validators for the application tier
        services.AddValidatorsFromAssemblyContaining<IMyShoppingCartApplicationMarker>();

        // Add MediatR for the application tier
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<IMyShoppingCartApplicationMarker>();
            options.RegisterPipelines();
        });

        // Add dependency injection for dependencies in the Application project
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IUserSecurityService, UserSecurityService>();
        services.AddScoped<IUserManagerFacade, UserManagerFacade>();
        services.AddTransient<IMapper, Mapper>();

        // Add Mapster Configuration
        MapsterConfig.AddMapsterConfig();

        return services;
    }


    private static MediatRServiceConfiguration RegisterPipelines(this MediatRServiceConfiguration options)
    {
        // Get the assemblies for the project
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.FullName is not null && x.FullName.StartsWith("MyShoppingCart.")).ToList();

        // Create the delegate for getting IRequest<> types from the assemblies.
        var isIRequest = new Func<Type, bool>(x =>
        x.Namespace == "MediatR" &&
        x.Name == "IRequest`1");

        // Get all types from project assemblies that are IRequest<>
        var requests = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(t => t.IsClass && t.GetInterfaces().Any(isIRequest))
            .ToList();

        // Grab the base types for the objects to be instantiated
        var pipelineInterfaceType = typeof(IPipelineBehavior<,>);
        var exceptionPipelineType = typeof(ExceptionLoggingPipelineBehavior<,>);
        var validationPipelineType = typeof(ValidationPipelineBehavior<,>);
        var authorizedUserPipelineType = typeof(AuthorizedCustomerPipelineBehavior<,>);

        foreach (var request in requests)
        {
            // Grab the actual interfaces for the IRequest<> types with generic arguments
            var iqueryInterface = request.GetInterfaces().First(isIRequest);
            var response = iqueryInterface.GetGenericArguments().First();
            var payload = response.GetGenericArguments().First();
            
            // Decorate the types to instantiate with the generic arguments
            var interfaceToInject = pipelineInterfaceType.MakeGenericType(new Type[] { request, response });
            var concreteExceprion = exceptionPipelineType.MakeGenericType(new Type[] { request, payload });
            var concreteValidation = validationPipelineType.MakeGenericType(new Type[] { request, payload });
            var concreteAuthorized = authorizedUserPipelineType.MakeGenericType(new Type[] { request, payload });

            // Add the dependency injections for those genericized interfaces and concrete classes.
            options.AddBehavior(interfaceToInject, concreteExceprion);
            options.AddBehavior(interfaceToInject, concreteValidation);
            options.AddBehavior(interfaceToInject, concreteAuthorized);
        }

        return options;
    }
}
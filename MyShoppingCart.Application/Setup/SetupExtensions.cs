using Microsoft.Extensions.DependencyInjection;
using MyShoppingCart.Application.Authentication;
using MyShoppingCart.Application.PipelineBehaviors;

namespace MyShoppingCart.Application.Setup;

public static class SetupExtensions
{
    public static IServiceCollection SetupMyShoppingCartApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IMyShoppingCartApplicationMarker>();

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<IMyShoppingCartApplicationMarker>();
            options.RegisterPipelines();
        });

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    private static MediatRServiceConfiguration RegisterPipelines(this MediatRServiceConfiguration options)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.FullName is not null && x.FullName.StartsWith("MyShoppingCart.")).ToList();

        var isIRequest = new Func<Type, bool>(x =>
        x.Namespace == "MediatR" &&
        x.Name == "IRequest`1");

        var requests = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(t => t.IsClass && t.GetInterfaces().Any(isIRequest))
            .ToList();

        var pipelineInterfaceType = typeof(IPipelineBehavior<,>);
        var exceptionPipelineType = typeof(ExceptionLoggingPipelineBehavior<,>);
        var validationPipelineType = typeof(ValidationPipelineBehavior<,>);
        var authorizedUserPipelineType = typeof(AuthorizedCustomerPipelineBehavior<,>);

        foreach (var request in requests)
        {
            var iqueryInterface = request.GetInterfaces().First(isIRequest);
            var response = iqueryInterface.GetGenericArguments().First();
            var payload = response.GetGenericArguments().First();
            
            var interfaceToInject = pipelineInterfaceType.MakeGenericType(new Type[] { request, response });
            var concreteExceprion = exceptionPipelineType.MakeGenericType(new Type[] { request, payload });
            var concreteValidation = validationPipelineType.MakeGenericType(new Type[] { request, payload });
            var concreteAuthorized = authorizedUserPipelineType.MakeGenericType(new Type[] { request, payload });

            options.AddBehavior(interfaceToInject, concreteExceprion);
            options.AddBehavior(interfaceToInject, concreteValidation);
            options.AddBehavior(interfaceToInject, concreteAuthorized);
        }

        return options;
    }
}
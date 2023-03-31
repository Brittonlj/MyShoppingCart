using Microsoft.Extensions.DependencyInjection;
using MyShoppingCart.Application.Authentication;
using MyShoppingCart.Application.Authentication.Queries;
using MyShoppingCart.Application.Customers.Commands;
using MyShoppingCart.Application.Customers.Queries;
using MyShoppingCart.Application.Orders.Commands;
using MyShoppingCart.Application.Orders.Queries;
using MyShoppingCart.Application.PipelineBehaviors;
using MyShoppingCart.Application.Products.Queries;

namespace MyShoppingCart.Application.Setup;

public static class SetupExtensions
{
    public static IServiceCollection SetupMyShoppingCartApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IMyShoppingCartApplicationMarker>();

        services.AddMediatR(options =>
        {
            // Validation

            options.RegisterServicesFromAssemblyContaining<IMyShoppingCartApplicationMarker>();
            options.AddBehavior(
                typeof(IPipelineBehavior<CreateCustomerCommand, Response<Success>>),
                typeof(ValidationPipelineBehavior<CreateCustomerCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<DeleteCustomerCommand, Response<Success>>),
                typeof(ValidationPipelineBehavior<DeleteCustomerCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<UpdateCustomerCommand, Response<Success>>),
                typeof(ValidationPipelineBehavior<UpdateCustomerCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<GetCustomerQuery, Response<Customer>>),
                typeof(ValidationPipelineBehavior<GetCustomerQuery, Customer>));
            options.AddBehavior(
                typeof(IPipelineBehavior<GetCustomersQuery, Response<IReadOnlyList<Customer>>>),
                typeof(ValidationPipelineBehavior<GetCustomersQuery, IReadOnlyList<Customer>>));

            options.AddBehavior(
                typeof(IPipelineBehavior<CreateOrderCommand, Response<Success>>),
                typeof(ValidationPipelineBehavior<CreateOrderCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<DeleteOrderCommand, Response<Success>>),
                typeof(ValidationPipelineBehavior<DeleteOrderCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<UpdateOrderCommand, Response<Success>>),
                typeof(ValidationPipelineBehavior<UpdateOrderCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<GetOrderQuery, Response<Order>>),
                typeof(ValidationPipelineBehavior<GetOrderQuery, Order>));
            options.AddBehavior(
                typeof(IPipelineBehavior<GetOrdersByCustomerIdQuery, Response<IReadOnlyList<Order>>>),
                typeof(ValidationPipelineBehavior<GetOrdersByCustomerIdQuery, IReadOnlyList<Order>>));

            options.AddBehavior(
                typeof(IPipelineBehavior<GetProductsQuery, Response<IReadOnlyList<Product>>>),
                typeof(ValidationPipelineBehavior<GetProductsQuery, IReadOnlyList<Product>>));

            options.AddBehavior(
                typeof(IPipelineBehavior<JwtTokenQuery, Response<string>>),
                typeof(ValidationPipelineBehavior<JwtTokenQuery, string>));




            // Eror logging

            options.AddBehavior(
                typeof(IPipelineBehavior<CreateCustomerCommand, Response<Success>>),
                typeof(ExceptionLoggingPipelineBehavior<CreateCustomerCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<DeleteCustomerCommand, Response<Success>>),
                typeof(ExceptionLoggingPipelineBehavior<DeleteCustomerCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<UpdateCustomerCommand, Response<Success>>),
                typeof(ExceptionLoggingPipelineBehavior<UpdateCustomerCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<GetCustomerQuery, Response<Customer>>),
                typeof(ExceptionLoggingPipelineBehavior<GetCustomerQuery, Customer>));
            options.AddBehavior(
                typeof(IPipelineBehavior<GetCustomersQuery, Response<IReadOnlyList<Customer>>>),
                typeof(ExceptionLoggingPipelineBehavior<GetCustomersQuery, IReadOnlyList<Customer>>));

            options.AddBehavior(
                typeof(IPipelineBehavior<CreateOrderCommand, Response<Success>>),
                typeof(ExceptionLoggingPipelineBehavior<CreateOrderCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<DeleteOrderCommand, Response<Success>>),
                typeof(ExceptionLoggingPipelineBehavior<DeleteOrderCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<UpdateOrderCommand, Response<Success>>),
                typeof(ExceptionLoggingPipelineBehavior<UpdateOrderCommand, Success>));
            options.AddBehavior(
                typeof(IPipelineBehavior<GetOrderQuery, Response<Order>>),
                typeof(ExceptionLoggingPipelineBehavior<GetOrderQuery, Order>));
            options.AddBehavior(
                typeof(IPipelineBehavior<GetOrdersByCustomerIdQuery, Response<IReadOnlyList<Order>>>),
                typeof(ExceptionLoggingPipelineBehavior<GetOrdersByCustomerIdQuery, IReadOnlyList<Order>>));

            options.AddBehavior(
                typeof(IPipelineBehavior<GetProductsQuery, Response<IReadOnlyList<Product>>>),
                typeof(ExceptionLoggingPipelineBehavior<GetProductsQuery, IReadOnlyList<Product>>));

            options.AddBehavior(
                typeof(IPipelineBehavior<JwtTokenQuery, Response<string>>),
                typeof(ExceptionLoggingPipelineBehavior<JwtTokenQuery, string>));
        });



        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}

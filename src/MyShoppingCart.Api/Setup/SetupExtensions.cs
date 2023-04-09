using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyShoppingCart.Api.Endpoints;
using MyShoppingCart.Domain.Configuration;
using System.Security.Claims;
using System.Text;

namespace MyShoppingCart.Api.Setup;

public static class SetupExtensions
{
    public static WebApplication RegisterMyShoppingCartEndpoints(this WebApplication app)
    {
        CustomerEndpoints.RegisterEndpoints(app);
        OrderEndpoints.RegisterEndpoints(app);
        ProductEndpoints.RegisterEndpoints(app);
        AuthenticationEndpoints.RegisterEndpoints(app);
        return app;
    }

    public static IServiceCollection SetupMyShoppingCartApi(
        this IServiceCollection services,
        ConfigurationManager config)
    {
        AddSwaggerGen(services);

        services
            .AddOptions<MyShoppingCartSettings>()
            .BindConfiguration(MyShoppingCartSettings.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<JwtSettings>()
            .BindConfiguration(JwtSettings.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        AddAuthentication(services, config);

        services.AddHttpContextAccessor();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.AdminAccess, policy => policy
            .RequireRole(Roles.Admin)
            .RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.NameIdentifier));

            options.AddPolicy(Policies.CustomerAccess, policy =>
            {
                policy.RequireAssertion(context =>
                    context.User.IsInRole(Roles.Customer) ||
                    context.User.IsInRole(Roles.Admin))
                .RequireAuthenticatedUser()
                .RequireClaim(ClaimTypes.NameIdentifier);
            });
        });

        return services;
    }

    private static void AddSwaggerGen(IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }

    private static void AddAuthentication(IServiceCollection services, ConfigurationManager config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            JwtSettings jwtConfig = new JwtSettings();
            config.GetSection(JwtSettings.SECTION_NAME).Bind(jwtConfig);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
            };
        });
    }
}

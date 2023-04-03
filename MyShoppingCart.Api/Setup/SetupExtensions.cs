using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyShoppingCart.Api.Endpoints;
using MyShoppingCart.Application.Configuration;
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

        services.Configure<MyShoppingCartSettings>(config.GetSection(MyShoppingCartSettings.SECTION_NAME));
        services.Configure<JwtConfig>(config.GetSection(JwtConfig.SECTION_NAME));

        AddAuthentication(services, config);

        services.AddHttpContextAccessor();

        services.AddAuthorization();

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
            var validIssuer = config["Jwt:Issuer"];
            var validAudience = config["Jwt:Audience"];
            var key = config["key"];

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = validIssuer,
                ValidAudience = validAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        });
    }
}

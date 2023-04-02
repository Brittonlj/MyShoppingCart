using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyShoppingCart.Api.Endpoints;
using MyShoppingCart.Application.Authentication;
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
        services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
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

        services.Configure<MyShoppingCartSettings>(config.GetSection(MyShoppingCartSettings.SECTION_NAME));
        services.Configure<JwtConfig>(config.GetSection(JwtConfig.SECTION_NAME));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
            };
        });

        services.AddHttpContextAccessor();

        services.AddAuthorization();

        return services;
    }
}

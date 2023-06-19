using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyShoppingCart.Api.Endpoints;
using MyShoppingCart.Domain.Entities;
using MyShoppingCart.Infrastructure;
using System.Security.Claims;
using System.Text;

namespace MyShoppingCart.Api.Setup;

public static class SetupExtensions
{
    public const string CORS_POLICY = "_origins";

    public static WebApplication RegisterMyShoppingCartEndpoints(this WebApplication app)
    {
        // Register all endpoints
        CustomerEndpoints.RegisterEndpoints(app);
        OrderEndpoints.RegisterEndpoints(app);
        ProductEndpoints.RegisterEndpoints(app);
        AuthenticationEndpoints.RegisterEndpoints(app);
        CategoryEndpoints.RegisterEndpoints(app);
        return app;
    }

    public static IServiceCollection SetupMyShoppingCartApi(
        this IServiceCollection services,
        ConfigurationManager config)
    {
        // Setup the defaults for Swagger
        services.SetupSwaggerGen();

        // Setup the IOptions<JwtSettings> object and put it in DI
        services
            .AddOptions<JwtSettings>()
            .BindConfiguration(JwtSettings.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // Add Identity Framework with the Customer type and the MyShoppingCartContext dbContext
        services.AddIdentity<Customer, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<MyShoppingCartContext>();

        // Read the JwtSettings from the appsettings.json file
        JwtSettings jwtConfig = new JwtSettings();
        config.GetSection(JwtSettings.SECTION_NAME).Bind(jwtConfig);

        // Add JWT Token Authentication and set options
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
                ClockSkew = TimeSpan.FromSeconds(10)
            };
        });

        // Add the HttpContextAccessor to DI
        services.AddHttpContextAccessor();

        // Setup the default password options
        services.Configure<IdentityOptions>(opts => {
            opts.Password.RequiredLength = 8;
            opts.Password.RequireLowercase = true;
            opts.Password.RequireUppercase = true;
            opts.Password.RequireDigit = true;
            opts.Password.RequireNonAlphanumeric = true;
            opts.User.RequireUniqueEmail = true;
        });

        // Setup the default authorization policies (Admin and Customer)
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.AdminAccess, policy => policy
            .RequireRole(Roles.Admin)
            .RequireClaim(ClaimTypes.NameIdentifier));

            options.AddPolicy(Policies.CustomerAccess, policy =>
            {
                policy.RequireAssertion(context =>
                    context.User.IsInRole(Roles.Customer) ||
                    context.User.IsInRole(Roles.Admin))
                .RequireClaim(ClaimTypes.NameIdentifier);
            });
        });

        // Add CORS so our web front-end can communicate with the API
        services.AddCors(options =>
        {
            options.AddPolicy(CORS_POLICY,
                policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        return services;
    }

    public static WebApplication SetupUseCors(this WebApplication app)
    {
        // Tell CORS to use our policies
        app.UseCors(CORS_POLICY);

        return app;
    }

    private static IServiceCollection SetupSwaggerGen(this IServiceCollection services)
    {
        // Setup Swagger with options for JWT Token authentication
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

        return services;
    }
}

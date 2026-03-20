namespace GroceryStore.Bootstrap;

using System.Security.Claims;
using System.Text;
using Dapper;
using Database;
using Database.Entities.User;
using Features.Admin.Products.UpdateProduct;
using Features.Auth.Login;
using Features.Auth.Register;
using FluentValidation;
using Infrastructure.Services;
using Mappers.Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ServiceScan.SourceGenerator;
using Shared.Consts;
using Shared.Consts.Endpoints;
using Shared.Interfaces;
using Shared.Models.Optional;
using Shared.Options;

public static partial class DependencyInjection
{
    public static IServiceCollection AddBasicServices(this IServiceCollection services)
    {
        services.AddOpenApi(options => ConfigureSecurity(options));

        services.AddOpenApi(EndpointGroups.Auth, options =>
        {
            options.ShouldInclude = desc => desc.GroupName == EndpointGroups.Auth;
            ConfigureSecurity(options);
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "GroceryStore Auth API",
                    Version = "v1",
                };

                return Task.CompletedTask;
            });
        });

        services.AddOpenApi(EndpointGroups.Admin, options =>
        {
            options.ShouldInclude = desc => desc.GroupName == EndpointGroups.Admin;
            ConfigureSecurity(options);
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "GroceryStore Admin API",
                    Version = "v1",
                };

                return Task.CompletedTask;
            });
        });

        services.AddOpenApi(EndpointGroups.User, options =>
        {
            options.ShouldInclude = desc => desc.GroupName == EndpointGroups.User;
            ConfigureSecurity(options);
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "GroceryStore User API",
                    Version = "v1",
                };

                return Task.CompletedTask;
            });
        });

        services.AddMemoryCache();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new OptionalJsonConverterFactory());
        });

        return services;
    }


    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
                         ?? throw new InvalidOperationException("JWT settings not found");

        // Identity
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


        // JWT
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
                options.DefaultScheme = "Bearer";
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                };
            });

        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
        });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddDatabaseServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new JsonMetadataMapper());

        return services;
    }

    public static IServiceCollection AddFeatureServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryAttributeValueNormalizer>();
        services.AddScoped<ProductSkuGenerationService>();
        services.AddScoped<TokenService>();

        services.AddScoped<UpdateProductHandler>();

        services.AddScoped<LoginHandler>();

        services.AddScoped<RegisterHandler>();

        return services;
    }

    private static void ConfigureSecurity(OpenApiOptions options)
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            var bearerScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = OpenApiBearerScheme.SchemeName,
                BearerFormat = OpenApiBearerScheme.BearerFormat,
                In = ParameterLocation.Header,
                Description = OpenApiBearerScheme.Description,
            };

            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
            document.Components.SecuritySchemes.TryAdd(OpenApiBearerScheme.Id, bearerScheme);

            return Task.CompletedTask;
        });


        options.AddOperationTransformer((operation, context, cancellationToken) =>
        {
            var metadata = context.Description.ActionDescriptor.EndpointMetadata;


            var isPublic = metadata.Any(m => m is IAllowAnonymous);

            if (isPublic)
            {
                return Task.CompletedTask;
            }

            var schemeReference = new OpenApiSecuritySchemeReference(OpenApiBearerScheme.Id, context.Document);

            operation.Security ??= new List<OpenApiSecurityRequirement>();
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [schemeReference] = [],
            });

            return Task.CompletedTask;
        });
    }

    [GenerateServiceRegistrations(AssignableTo = typeof(IEndpoint), CustomHandler = "MapEndpoint")]
    public static partial IEndpointRouteBuilder MapEndpointsGenerated(this IEndpointRouteBuilder endpoints);

    [GenerateServiceRegistrations(AssignableTo = typeof(IValidator<>), Lifetime = ServiceLifetime.Scoped)]
    public static partial IServiceCollection RegisterValidators(this IServiceCollection services);

    [GenerateServiceRegistrations(AssignableTo = typeof(IRepository), AsSelf = true, Lifetime = ServiceLifetime.Scoped)]
    public static partial IServiceCollection RegisterRepositories(this IServiceCollection services);
}
using Common.Extensions.Configuration;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Common.Extensions.DependencyInjection;

public static partial class ServiceCollectionsExtensions
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services, ApiServicesConfiguration apiServicesConfiguration)
    {
        services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        services.AddEndpointsApiExplorer();

        if (apiServicesConfiguration.AddAuthentication)
        {
            services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
        }
        if (apiServicesConfiguration.AddAuthorization)
        {
            //services.AddAuthorization();
            services.AddAuthorizationBuilder();
        }

        //.AddProblemDetails()

        if (apiServicesConfiguration.AddFastEndpoints)
        {
            services.AddFastEndpoints();
        }

        if (apiServicesConfiguration.AddSwagger)
        {
            if(apiServicesConfiguration.AddFastEndpoints)
            {
                services.SwaggerDocument();
            }
            else
            {
                if(apiServicesConfiguration.SwaggerGenOptions is not null)
                {
                    services.AddSwaggerGen(apiServicesConfiguration.SwaggerGenOptions);
                }
                else
                {
                    services.AddSwaggerGen();
                }
            }
        }

        if (apiServicesConfiguration.CorsConfiguration is not null)
        {
            services.ConfigursCORSPolicies(apiServicesConfiguration.CorsConfiguration!);
        }

        return services;
    }

    private static void ConfigursCORSPolicies(this IServiceCollection services, CORSConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                name: configuration!.PolicyName,
                policy => policy
                    .WithOrigins(configuration.Origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
        });
    }
}

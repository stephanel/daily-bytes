using Common.Extensions.Configuration;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Common.Extensions.DependencyInjection;

public static partial class ServiceCollectionsExtensions
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services, ApiServicesConfiguration apiServicesConfiguration)
    {
        services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        services.AddEndpointsApiExplorer();

        if (apiServicesConfiguration.AddAuthorization)
        {
            services.AddAuthorization();

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
                services.AddSwaggerGen();
            }
        }

        if (apiServicesConfiguration.CorsConfiguration is not null)
        {
            services.ConfigursCORSPolicies(apiServicesConfiguration.CorsConfiguration!);
        }

        return services;
    }

    public static void ConfigursCORSPolicies(this IServiceCollection services, CORSConfiguration configuration)
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

using Common.Extensions.Configuration;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Common.Extensions.DependencyInjection;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services, CORSConfiguration? corsConfiguration)
    {
        services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        services.AddEndpointsApiExplorer()
            .AddAuthorization()
            //.AddProblemDetails()
            .AddFastEndpoints()
            .AddSwagger()
            ;

        if (corsConfiguration is not null)
        {
            services.ConfigursCORSPolicies(corsConfiguration!);
        }

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.SwaggerDocument();
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

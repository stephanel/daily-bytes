using Common.Extensions.Configuration;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Common.Extensions.DependencyInjection.ServiceCollectionsExtensions;

namespace Common.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ConfigureApi(this IApplicationBuilder app, ApiServicesConfiguration apiServicesConfiguration)
    {
        MapHealthChecks(app as WebApplication);

        app.UseHttpsRedirection();

            //.UseExceptionHandler()

        if(apiServicesConfiguration.AddAuthorization)
        {
            app.UseAuthorization();
        }

        if(apiServicesConfiguration.AddFastEndpoints)
        {
            app.UseFastEndpoints(c =>
            {
                c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
                c.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });
        }

        if(apiServicesConfiguration.AddSwagger)
        {
            if (apiServicesConfiguration.AddFastEndpoints)
            {
                app.UseSwaggerGen();
            } 
            else
            {
                //app.UseSwagger();
                app.UseSwaggerUi();
            }
        }

        if (apiServicesConfiguration.CorsConfiguration is not null)
        {
            app.UseCors(apiServicesConfiguration.CorsConfiguration.PolicyName);
        }

        return app;
    }

    public static void MapHealthChecks(WebApplication? app)
    {
        if(app is not null)
        {
            app!.MapHealthChecks("/api/health");
            app.MapHealthChecks("/api/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });
        }
    }
}
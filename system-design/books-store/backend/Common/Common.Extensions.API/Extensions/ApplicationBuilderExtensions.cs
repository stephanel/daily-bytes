using Common.Extensions.Configuration;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ConfigureApi(this IApplicationBuilder app, CORSConfiguration corsConfiguration)
    {
        MapHealthChecks(app as WebApplication);

        app.UseHttpsRedirection()
            //.UseExceptionHandler()
            .UseAuthorization()
            .UseFastEndpoints(c =>
            {
                c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
                c.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            })
            .UseSwaggerGen();

        if(corsConfiguration is not null)
        {
            app.UseCors(corsConfiguration.PolicyName);
        }

        return app;
    }

    public static void MapHealthChecks(WebApplication? app)
    {
        if(app is not null)
        {
            app!.MapHealthChecks("/health");
            app.MapHealthChecks("/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });
        }
    }
}
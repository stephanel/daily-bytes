using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ConfigureApi(this IApplicationBuilder app, ApiServicesConfiguration apiServicesConfiguration)
    {
        MapHealthChecks(app as WebApplication);

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
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }

        if (apiServicesConfiguration.CorsConfiguration is not null)
        {
            app.UseCors(apiServicesConfiguration.CorsConfiguration.PolicyName);
        }

        app.UseHttpsRedirection();

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
    public static void ApplyMigrations<TDbContext>(this IApplicationBuilder builder) 
        where TDbContext : DbContext
    {
        using IServiceScope scope = builder.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        dbContext.Database.Migrate();
    }
}
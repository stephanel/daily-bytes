using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookStore.Common.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ConfigureApi(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection()
           .UseAuthorization()
           .UseFastEndpoints(c =>
           {
               c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
               c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
               c.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
           })
           .UseSwaggerGen();

        return app;
    }
}

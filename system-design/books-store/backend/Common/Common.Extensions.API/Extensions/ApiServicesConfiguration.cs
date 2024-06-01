using Common.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Extensions.DependencyInjection;

public record ApiServicesConfiguration
{
    private ApiServicesConfiguration() { }

    public bool AddAuthentication { get; init; } = true;
    public bool AddAuthorization { get; init; } = true;
    public bool AddFastEndpoints { get; init; } = true;
    public bool AddSwagger { get; init; } = true;
    public CORSConfiguration? CorsConfiguration { get; init; }
    public Action<SwaggerGenOptions>? SwaggerGenOptions { get; init; }

    public static ApiServicesConfiguration Default => new();
}

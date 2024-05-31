using Common.Extensions.Configuration;

namespace Common.Extensions.DependencyInjection;

public record ApiServicesConfiguration
{
    private ApiServicesConfiguration() { }

    public bool AddAuthentication { get; init; } = true;
    public bool AddAuthorization { get; init; } = true;
    public bool AddFastEndpoints { get; init; } = true;
    public bool AddSwagger { get; init; } = true;
    public CORSConfiguration? CorsConfiguration { get; init; } = null;

    public static ApiServicesConfiguration Default => new();
}

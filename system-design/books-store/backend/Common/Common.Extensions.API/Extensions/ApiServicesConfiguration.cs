using Common.Extensions.Configuration;

namespace Common.Extensions.DependencyInjection;

public static partial class ServiceCollectionsExtensions
{
    public class ApiServicesConfiguration
    {
        public bool AddAuthorization { get; set; } = true;
        public bool AddFastEndpoints { get; set; } = true;
        public bool AddSwagger { get; set; } = true;
        public CORSConfiguration? CorsConfiguration { get; set; } = null;
    }
}

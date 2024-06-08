using Microsoft.Extensions.Configuration;

namespace Common.Extensions.DependencyInjection;

public static partial class ServiceCollectionsExtensions
{
    public static T GetConfig<T>(this IConfiguration configuration, string configSectionName)
        => configuration.GetRequiredSection(configSectionName).Get<T>()
        ?? throw new ArgumentNullException($"No configuration found for section '{configSectionName}'");
}

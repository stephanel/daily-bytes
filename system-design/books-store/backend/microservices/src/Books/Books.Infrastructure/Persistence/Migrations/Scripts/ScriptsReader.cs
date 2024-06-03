using System.Reflection;

namespace Books.Infrastructure.Persistence.Migrations.Scripts;

internal static class ScriptsReader
{
    private static Stream ReadEmbeddedResourceContent(string fullyQualifiedEmbeddedResourceName)
    {
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullyQualifiedEmbeddedResourceName)
            ?? throw new InvalidOperationException($"Unable to find any resource from '{fullyQualifiedEmbeddedResourceName}'");
        return stream;
    }

    public static string GetEmbeddedResourceTextContent(string filePath)
    {
        var currentNamespace = typeof(ScriptsReader).Namespace;
        var @namespace = currentNamespace?[..currentNamespace.LastIndexOf(".")];
        var resourcePath = $"{@namespace}.{filePath}";
        using var stream = ReadEmbeddedResourceContent(resourcePath);
        using (stream)
        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}
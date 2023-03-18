using System.Diagnostics;

namespace backend;

public static class DiagnosticsConfig
{
    public const string ServiceName = "BackendRestService";
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName);
}
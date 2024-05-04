using System.Diagnostics;

namespace frontend;

public static class DiagnosticsConfig
{
    public const string ServiceName = "BlazorFrontend";
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName);
}
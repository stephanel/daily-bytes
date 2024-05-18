using System.Diagnostics.Metrics;

namespace Common.Extensions.API.Observability;

internal class APICustomMetrics
{
    private readonly Counter<long> _requestCounter;
    private readonly Histogram<double> _requestDuration;

    public APICustomMetrics(IMeterFactory meterFactory)
    {
        string serviceName = "Books.API";   // FIXME: inject using IConfiguration/IOptions
        var meter = meterFactory.Create(serviceName);

        _requestCounter = meter.CreateCounter<long>($"{serviceName.ToLower()}.requests.count");
        _requestDuration = meter.CreateHistogram<double>($"{serviceName.ToLower()}.requests.duration", "ms");
    }

    public void IncreaseRequestCount()
    {
        _requestCounter.Add(1);
    }

    public TrackedRequestDuration MesureRequestDuration()
    {
        return new TrackedRequestDuration(_requestDuration);
    }
}

public sealed class TrackedRequestDuration : IDisposable
{
    private readonly long _requestStartTime = TimeProvider.System.GetTimestamp();
    private readonly Histogram<double> _requestDuration;

    public TrackedRequestDuration(Histogram<double> requestDuration)
    {
        _requestDuration = requestDuration;
    }

    public void Dispose()
    {
        var elapsed = TimeProvider.System.GetElapsedTime(_requestStartTime);
        _requestDuration.Record(elapsed.TotalMilliseconds);
    }
}

using Microsoft.Extensions.Logging;

namespace Common.TestFramework.Fakes;

public class FakeLogger<T> : ILogger<T> where T : class
{
    // FIXME: move it to Common.TestFramework.UnitTests
    public List<object> Logs { get; } = new();

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => new FakeScope();

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        => Logs.Add(new { LogLevel = logLevel, Message = state!.ToString() });

    private sealed class FakeScope : IDisposable
    {
        public void Dispose()
        {
            // do nothing
        }
    }
}
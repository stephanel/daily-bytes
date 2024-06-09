using Orders.Application.UseCases.AddItemToCurrentOrder;
using Orders.Infrastructure.ExternalServices;

namespace Orders.API.IntegrationTests.TestFramework.Fakes;

internal class FakeSessionIdGenerator : ISessionIdGenerator
{
    private readonly Queue<SessionId> _queue = new();

    public SessionId New() => _queue.Dequeue();

    public void SeedNext(SessionId sessionId) => _queue.Enqueue(sessionId);
}

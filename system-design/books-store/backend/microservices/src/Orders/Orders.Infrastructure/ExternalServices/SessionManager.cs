using Orders.Application.UseCases.AddItemToCurrentOrder;

namespace Orders.Infrastructure.ExternalServices;

internal class SessionManager : ISessionManager
{
    private readonly ICachedSessions _cachedSessions;

    public SessionManager(ICachedSessions cachedSessions)
    {
        _cachedSessions = cachedSessions;
    }

    public Task<SessionId> AddItem(Item item, SessionId? sessionId = null)
    {
        return _cachedSessions.AddToSession(item, sessionId);
    }
}

public interface ICachedSessions
{
    Task<SessionId> AddToSession(Item item, SessionId? sessionId = null);
}

public class CachedSessions : ICachedSessions
{
    // FIXME: temp solution. Move cache to Redis

    private readonly ISessionIdGenerator _sessionIdGenerator;

    private static readonly List<SessionId> Sessions = [];
    private static readonly List<Item> CachedItems = [];

    public CachedSessions(ISessionIdGenerator sessionIdGenerator)
    {
        _sessionIdGenerator = sessionIdGenerator;
    }

    public Task<SessionId> AddToSession(Item item, SessionId? sessionId = null)
    {
        // check if session exist
        //  => if not, create it
        //  => add item to session
        //  => return sessionId

        sessionId = GetOrCreateSession(sessionId);
        CachedItems.Add(item);
        return Task.FromResult(sessionId);
    }

    SessionId GetOrCreateSession(SessionId? sessionId)
    {
        if (sessionId is null)
        {
            sessionId = _sessionIdGenerator.New();
        }

        var session = Sessions.Find(s => s == sessionId);

        if (session is null)
        {
            Sessions.Add(sessionId);
        }

        return sessionId;
    }
}

public interface ISessionIdGenerator
{
   SessionId New();
}

public class SessionIdGenerator : ISessionIdGenerator
{
    public SessionId New() => Guid.NewGuid().ToString();
}
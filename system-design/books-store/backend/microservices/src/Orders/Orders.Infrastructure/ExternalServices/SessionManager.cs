using Orders.Application.UseCases.AddItemToCurrentOrder;

namespace Orders.Infrastructure.ExternalServices;

internal class SessionManager : ISessionManager
{
    public Task<SessionId> AddItem(Item item, SessionId? sessionId = null)
    {
        // check if session exist
        //  => if not, create it
        //  => add item to session
        //  => return sessionId
        throw new NotImplementedException();
    }
}

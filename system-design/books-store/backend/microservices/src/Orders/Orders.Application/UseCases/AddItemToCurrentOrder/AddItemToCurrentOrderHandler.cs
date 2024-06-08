using Common.Extensions.Rop;
using Mediator;

namespace Orders.Application.UseCases.AddItemToCurrentOrder;

internal class AddItemToCurrentOrderHandler(
    IBooksService booksService,
    ISessionManager sessionManager
    ) : IRequestHandler<AddItemToCurrentOrderRequest, Result<AddItemToCurrentOrderResult, Error>>
{
    readonly IBooksService _booksService = booksService;
    readonly ISessionManager _sessionManager = sessionManager;

    public async ValueTask<Result<AddItemToCurrentOrderResult, Error>> Handle(AddItemToCurrentOrderRequest request, CancellationToken cancellationToken)
    {
        var result = await _booksService.GetBookAsync(request.ItemId);

        if(result.IsFailure)
        {
            return result.Error!;
        }

        var sessionId = await _sessionManager.AddItem(result.Value!, request.SessionId);    // how to design SessionManager

        // check item exists - call Books API
        //  => if not exists return failure "bad request"
        //  => if exists
        //      => add to session (creation will be handled internally by session manager)
        //      => return OK

        return new AddItemToCurrentOrderResult(sessionId);
    }
}

public record AddItemToCurrentOrderRequest(long ItemId, SessionId? SessionId = null) 
    : IRequest<Result<AddItemToCurrentOrderResult, Error>>;

public record AddItemToCurrentOrderResult(SessionId SessionId);

public record SessionId
{
    private string Value { get; set; }
    private SessionId(string value) => Value = value;

    public static implicit operator SessionId(string value) => new SessionId(value);
    public static implicit operator string(SessionId sessionId) => sessionId.Value;

    public override string ToString() => Value;
}

public interface IBooksService 
{
    Task<Result<Book, Error>> GetBookAsync(long Id);
}

public record Item(long Id);

public record Book(long Id) : Item(Id);    // TODO: move to Domain?

public interface ISessionManager 
{
    Task<SessionId> AddItem(Item item, SessionId? sessionId = null);
}
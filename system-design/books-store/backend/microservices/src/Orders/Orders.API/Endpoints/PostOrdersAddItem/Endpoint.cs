using FastEndpoints;
using Mediator;
using Orders.Application.UseCases.AddItemToCurrentOrder;
using System.Net;
using static Orders.API.Endpoints.PostOrdersAddItem.Endpoint;

namespace Orders.API.Endpoints.PostOrdersAddItem;

internal sealed class Endpoint(IMediator mediator) : Endpoint<AddItemRequest>
{
    readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post("api/orders/items/{itemId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddItemRequest request, CancellationToken ct)
    {
        var sessionId = ReadSessionId();

        var result = await _mediator.Send(new AddItemToCurrentOrderRequest(request.ItemId, sessionId));

        if(result.IsSuccess)
        {
            AddItemToCurrentOrderResult success = result;
            HttpContext.Response.Cookies.Append("x-session-id", (string)success.SessionId!, new CookieOptions
            {
                HttpOnly = true, // The cookie is not accessible via client-side script
                Secure = true, // The cookie is sent only over HTTPS
                SameSite = SameSiteMode.Strict, // The cookie is sent only to the same site as the one that it's currently on
                Expires = DateTime.Now.AddHours(1) // The cookie expires in 7 days
            });

            await SendOkAsync(ct);
            return;
        }

        await SendErrorsAsync((int)HttpStatusCode.BadRequest, ct);
        return;
    }

    private SessionId? ReadSessionId()
    {
        HttpContext.Request.Cookies.TryGetValue("x-session-id", out string? cookieSessionId);
        return cookieSessionId is null ? default : (SessionId)cookieSessionId;
    }

    internal record AddItemRequest(long ItemId);
}

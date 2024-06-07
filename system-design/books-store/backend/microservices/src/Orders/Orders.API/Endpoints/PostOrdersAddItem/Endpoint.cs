using FastEndpoints;
using Mediator;

namespace Orders.API.Endpoints.PostOrdersAddItem;

internal sealed class Endpoint : Endpoint<AddItemRequest>
{
    public override void Configure()
    {
        Post("api/orders/items/{itemId}");
        AllowAnonymous();
    }

    public override Task HandleAsync(AddItemRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}

internal record AddItemRequest(long ItemId);
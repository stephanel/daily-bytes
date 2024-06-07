using FastEndpoints;

namespace Orders.API.Endpoints.GetOrdersCurrent;

internal class Endpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("api/orders/current");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}

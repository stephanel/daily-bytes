using FastEndpoints;
using Mediator;

namespace Orders.API.Endpoints.GetOrdersCurrent;

internal class Endpoint(IMediator mediator) : EndpointWithoutRequest
{
    readonly IMediator _mediator = mediator;

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

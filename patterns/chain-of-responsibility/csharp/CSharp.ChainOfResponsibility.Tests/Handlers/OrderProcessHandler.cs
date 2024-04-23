using CSharp.ChainOfResponsibility.Tests.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace CSharp.ChainOfResponsibility.Tests.Handlers;

internal class OrderProcessHandler
{
    private readonly IHandler _orderValidation;

    public OrderProcessHandler([FromKeyedServices(nameof(OrderValidation))] IHandler orderValidation)
    {
        _orderValidation = orderValidation;
    }

    public void Handle(OrderReceivedMessage orderReceived)
    {
        _orderValidation.Handle(orderReceived);
    }
}
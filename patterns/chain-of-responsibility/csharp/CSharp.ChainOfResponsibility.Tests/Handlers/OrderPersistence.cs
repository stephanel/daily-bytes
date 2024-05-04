using CSharp.ChainOfResponsibility.Tests.Messages;

namespace CSharp.ChainOfResponsibility.Tests.Handlers;

internal class OrderPersistence : AbstractHandler
{
    public OrderPersistence(IHandler next) : base(next)
    { }

    public override void Handle(object request)
    {
        // persists received order
        var orderReceived = (OrderReceivedMessage)request;
        base.Handle(orderReceived);
    }
}

using CSharp.ChainOfResponsibility.Tests.Messages;

namespace CSharp.ChainOfResponsibility.Tests.Handlers;

internal class OrderValidation : AbstractHandler
{
    public OrderValidation(IHandler next) : base(next)
    { }

    public override void Handle(object request)
    {
        // validate received order
        var orderReceived = (OrderReceivedMessage)request;
        base.Handle(orderReceived);
    }
}

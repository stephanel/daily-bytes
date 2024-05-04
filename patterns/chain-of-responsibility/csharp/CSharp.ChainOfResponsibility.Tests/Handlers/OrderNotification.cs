using CSharp.ChainOfResponsibility.Tests.Messages;

namespace CSharp.ChainOfResponsibility.Tests.Handlers;

public class OrderNotification : AbstractHandler
{
    public OrderNotification() : base()
    { }

    public override void Handle(object request)
    {
        // send notification
        var orderReceived = (OrderReceivedMessage)request;
        base.Handle(orderReceived);
    }
}

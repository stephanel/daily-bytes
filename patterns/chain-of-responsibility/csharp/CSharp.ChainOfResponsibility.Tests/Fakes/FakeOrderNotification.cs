using CSharp.ChainOfResponsibility.Tests.Handlers;

namespace CSharp.ChainOfResponsibility.Tests.Fakes;

internal class FakeOrderNotification : OrderNotification
{
    public object? ReceivedMessage { get; private set; } = null;

    public override void Handle(object request)
    {
        base.Handle(request);
        ReceivedMessage = request;
    }
}

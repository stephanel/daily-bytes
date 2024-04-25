using Modules.Communication.Tests.Asynchronous.Events;
using Modules.Communication.Tests.Asynchronous.UsingRxDotNet.Shared;

namespace Modules.Communication.Tests.Asynchronous.UsingRxDotNet;

public class AsynchronousCommunicationUsingRxDotNet
{
    [Fact]
    public void Publish_Subscribe()
    {
        // arrange
        IEventPublisher eventPublisher = new EventPublisher();

        FakeModuleB moduleB = new(eventPublisher.GetEvent<CustomEvent>());
        var moduleA = new ModuleA(eventPublisher);

        // assert
        var data = "some data";
        moduleA.DoAndPublish(data);

        // act
        moduleB.CaptureEvent.Should().NotBeNull();
        moduleB.CaptureEvent!.Data.Should().Be(data);
    }
}

class FakeModuleB : ModuleB
{
    public CustomEvent? CaptureEvent { get; private set; }

    public FakeModuleB(IObservable<CustomEvent> events) : base(events)
    { }

    protected override void Handle(CustomEvent @event)
    {
        CaptureEvent = @event;
        base.Handle(@event);
    }
}

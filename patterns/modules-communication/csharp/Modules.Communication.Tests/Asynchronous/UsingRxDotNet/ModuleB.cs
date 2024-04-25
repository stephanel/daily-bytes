using Modules.Communication.Tests.Asynchronous.Events;
using Modules.Communication.Tests.Asynchronous.UsingRxDotNet.Shared;

namespace Modules.Communication.Tests.Asynchronous.UsingRxDotNet;

internal class ModuleB : Subscriber<CustomEvent>
{
    public ModuleB(IObservable<CustomEvent> events)
        : base(events)
    { }

    protected override void Handle(CustomEvent @event)
    {
        // process event
    }
}
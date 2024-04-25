using Modules.Communication.Tests.Asynchronous.Events;
using Modules.Communication.Tests.Asynchronous.UsingRxDotNet.Shared;

namespace Modules.Communication.Tests.Asynchronous;

internal record ModuleA(IEventPublisher eventPublisher)
{
    internal void DoAndPublish(string data)
    {
        eventPublisher.Publish(new CustomEvent(data));
    }
}

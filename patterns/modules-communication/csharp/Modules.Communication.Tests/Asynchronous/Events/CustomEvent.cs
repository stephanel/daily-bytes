using Modules.Communication.Tests.Asynchronous.UsingRxDotNet.Shared;

namespace Modules.Communication.Tests.Asynchronous.Events;

public record CustomEvent(string Data) : IEvent
{ }
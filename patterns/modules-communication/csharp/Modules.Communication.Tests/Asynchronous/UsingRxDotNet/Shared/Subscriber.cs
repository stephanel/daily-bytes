namespace Modules.Communication.Tests.Asynchronous.UsingRxDotNet.Shared;

internal abstract class Subscriber<TEvent> where TEvent : IEvent
{
    protected readonly IDisposable subscription;

    protected Subscriber(IObservable<TEvent> events)
    {
        subscription = events
            .Subscribe(Handle);
    }

    protected abstract void Handle(TEvent @event);

    public void Unsubscribe()
    {
        subscription.Dispose();
    }
}

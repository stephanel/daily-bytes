using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Modules.Communication.Tests.Asynchronous.UsingRxDotNet.Shared;

public interface IEventPublisher
{
    void Publish<TEvent>(TEvent evt) where TEvent : IEvent;
    IObservable<TEvent> GetEvent<TEvent>() where TEvent : IEvent;

}

internal class EventPublisher : IEventPublisher
{
    private readonly ConcurrentDictionary<Type, object> subjects
                = new ConcurrentDictionary<Type, object>();

    public IObservable<TEvent> GetEvent<TEvent>()
        where TEvent : IEvent
    {
        var subject =
        (ISubject<TEvent>)subjects.GetOrAdd(typeof(TEvent),
                    t => new Subject<TEvent>());
        return subject.AsObservable();
    }

    public void Publish<TEvent>(TEvent evt)
         where TEvent : IEvent
    {
        if (subjects.TryGetValue(typeof(TEvent), out object? subject))
        {
            ((ISubject<TEvent>)subject)!.OnNext(evt);
        }
    }
}

using System.Text.Json;
using EventSourcing.Domain.Entities;
using EventSourcing.Domain.Events;

namespace EventSourcing.Infrastructure;

internal class EventSourcingRepository
{
    private readonly Dictionary<Guid, List<EventEntry>> _events = new();

    public void Save(IAggregate aggregate)
    {
        var events = aggregate.Events;

        if (!_events.ContainsKey(aggregate.Id))
        {
            _events[aggregate.Id] = [];
        }

        _events[aggregate.Id] = events.Select(Map).ToList();
    }

    public TAggregate Load<TAggregate>(Guid id) where TAggregate : IAggregate
    {
        var events = _events[id].Select(Map).ToList();
        var type = typeof(TAggregate);
        return (TAggregate)Activator.CreateInstance(type, id, events)!;
    }

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        TypeInfoResolver = new DomainEventsTypeResolver()
    };

    private EventEntry Map(IDomainEvent domainEvent)
        => new(domainEvent.GetType().ToString(),
            JsonSerializer.Serialize(domainEvent, _serializerOptions));

    private IDomainEvent Map(EventEntry eventEntry)
        => (IDomainEvent)JsonSerializer.Deserialize(eventEntry.Payload,
            FindType(eventEntry.DotNetType),
            _serializerOptions)!;

    private Type FindType(string typeName)
        => AppDomain.CurrentDomain.GetAssemblies().Select(assembly => assembly.GetType(typeName))
            .Where(type => type is not null)
            .Select(type => type!).First();
}

// EventEntry is meat to mimic how the events would be persisted in a real-world scenario, using a real events storage system.
internal record EventEntry(string DotNetType, string Payload);
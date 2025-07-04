using System.Text.Json;
using System.Text.Json.Serialization;
using EventSourcing.Domain.Core;

namespace EventSourcing.Infrastructure;

internal class InMemoryEventStore()
{
    private readonly Dictionary<Guid, List<EventEntry>> _events = new();

    public void Save(IAggregate aggregate)
    {
        var events = aggregate.Events;

        if (!_events.ContainsKey(aggregate.Id))
        {
            _events[aggregate.Id] = [];
        }

        var maxVersion = _events[aggregate.Id]
            .DefaultIfEmpty(EventEntry.Empty)
            .Max(x => x.Version);

        _events[aggregate.Id].AddRange( events.Select(Map).Where(x => x.Version > maxVersion));
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
        TypeInfoResolver = new DomainEventsTypeResolver(),
        Converters = { new JsonStringEnumConverter() }
    };

    private EventEntry Map(IDomainEvent domainEvent, int version)
        => new(DateTimeOffset.Now, domainEvent.GetType().ToString(),
            JsonSerializer.Serialize(domainEvent, _serializerOptions),
            version + 1);

    private IDomainEvent Map(EventEntry eventEntry)
        => (IDomainEvent)JsonSerializer.Deserialize(eventEntry.Payload,
            FindType(eventEntry.DotNetType),
            _serializerOptions)!;

    private Type FindType(string typeName)
        => AppDomain.CurrentDomain.GetAssemblies().Select(assembly => assembly.GetType(typeName))
            .Where(type => type is not null)
            .Select(type => type!).First();
}
using EventSourcing.Domain.Events;

namespace EventSourcing.Domain.Entities;

public interface IAggregate
{
    Guid Id { get; }
    List<IDomainEvent> Events { get; }
}
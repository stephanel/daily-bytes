namespace EventSourcing.Domain.Core;

public interface IAggregate
{
    Guid Id { get; }
    IReadOnlyList<IDomainEvent> Events { get; }
}
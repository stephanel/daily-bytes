namespace EventSourcing.Domain.Events;

public record class BookedTimeSlotChanged(DateTime StartTime, DateTime EndTime) : IDomainEvent;
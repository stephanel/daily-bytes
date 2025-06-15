using EventSourcing.Domain.Core;

namespace EventSourcing.Domain.Events;

public record BookedTimeSlotChanged(DateTime StartTime, DateTime EndTime) : IDomainEvent;
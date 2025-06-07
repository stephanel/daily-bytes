using EventSourcing.Domain.Enums;

namespace EventSourcing.Domain.Events;

public record class BookingCreated(MeetingSpace MeetingSpace, DateTime StartTime, DateTime EndTime) : IDomainEvent;

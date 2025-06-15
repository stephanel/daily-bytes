using EventSourcing.Domain.Core;
using EventSourcing.Domain.Enums;

namespace EventSourcing.Domain.Events;

public record BookingCreated(MeetingSpace MeetingSpace, DateTime StartTime, DateTime EndTime) : IDomainEvent;

using EventSourcing.Domain.Core;
using EventSourcing.Domain.Entities;
using EventSourcing.Domain.Enums;

namespace EventSourcing.Domain.Events;

public record BookingCreated(MeetingRoom MeetingRoom, DateTime StartTime, DateTime EndTime, IReadOnlyList<Attendee> Attendees) : IDomainEvent;

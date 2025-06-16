using EventSourcing.Domain.Core;
using EventSourcing.Domain.Entities;

namespace EventSourcing.Domain.Events;

public record AttendeeAdded(IReadOnlyList<Attendee> Attendees) : IDomainEvent;
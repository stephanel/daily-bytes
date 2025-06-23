using EventSourcing.Domain.Core;
using EventSourcing.Domain.Entities;

namespace EventSourcing.Domain.Events;

public record AttendeeRemoved(IReadOnlyList<Attendee> Attendees) : IDomainEvent;
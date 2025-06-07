using EventSourcing.Domain.Enums;

namespace EventSourcing.Domain.Events;

public record class MeetingSpaceChanged(MeetingSpace MeetingSpace) : IDomainEvent;
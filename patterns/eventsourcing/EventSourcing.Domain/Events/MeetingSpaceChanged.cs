using EventSourcing.Domain.Core;
using EventSourcing.Domain.Enums;

namespace EventSourcing.Domain.Events;

public record MeetingSpaceChanged(MeetingSpace MeetingSpace) : IDomainEvent;
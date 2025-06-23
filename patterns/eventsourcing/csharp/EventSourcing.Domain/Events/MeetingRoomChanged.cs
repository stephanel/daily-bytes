using EventSourcing.Domain.Core;
using EventSourcing.Domain.Enums;

namespace EventSourcing.Domain.Events;

public record MeetingRoomChanged(MeetingRoom MeetingRoom) : IDomainEvent;
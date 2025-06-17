using EventSourcing.Domain.Core;
using EventSourcing.Domain.Enums;
using EventSourcing.Domain.Events;

namespace EventSourcing.Domain.Entities;

public class Booking : IAggregate
{
    private readonly List<IDomainEvent> _events = [];

    private Booking()
    {
    }

    public Guid Id { get; private init; }
    public MeetingRoom MeetingRoom { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }

    public IReadOnlyList<Attendee> Attendees { get; private set; } = [];

    public IReadOnlyList<IDomainEvent> Events => _events;

    /// <summary>
    /// Creates a new Booking instance with the specified ID, start time, and end time.
    /// Meant to be called from business logic to create a new booking
    /// </summary>
    /// <returns></returns>
    public Booking(Guid id, MeetingRoom meetingRoom, DateTime startTime, DateTime endTime, IReadOnlyList<Attendee> attendees)
    {
        Id = id;
        MeetingRoom = meetingRoom;
        StartTime = startTime;
        EndTime = endTime;
        Attendees = attendees;
        _events.Add(new BookingCreated(meetingRoom, startTime, endTime, attendees));
    }

    /// <summary>
    /// Creates a Booking instance and restore the state from the provided events.
    /// Meant to be called when a booking is fetched from persistence storage
    /// </summary>
    /// <param name="id"></param>
    /// <param name="events"></param>
    /// <returns></returns>
    public Booking(Guid id, List<IDomainEvent> events)
    {
        Id = id;
        Apply(events);
    }

    public void UpdateTimeSlot(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
        _events.Add(new BookedTimeSlotChanged(startTime, endTime));
    }

    public void UpdateMeetingRoom(MeetingRoom meetingRoom)
    {
        MeetingRoom = meetingRoom;
        _events.Add(new MeetingRoomChanged(meetingRoom));
    }

    public void AddAttendee(Attendee newAttendee)
    {
        Attendees = [..Attendees, newAttendee];
        _events.Add(new AttendeeAdded(Attendees));
    }

    public void RemoveAttendee(Attendee attendee)
    {
        Attendees = [..Attendees.Where(x => x != attendee)];
        _events.Add(new AttendeeRemoved(Attendees));
    }

    private void Apply(List<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            _events.Add(@event);
            switch (@event)
            {
                case BookingCreated e: Apply(e); break;
                case BookedTimeSlotChanged e: Apply(e); break;
                case MeetingRoomChanged e: Apply(e); break;
                case AttendeeAdded e: Apply(e); break;
                case AttendeeRemoved e: Apply(e); break;
                default:
                    throw new NotImplementedException(
                        $"No Apply method found for event type '{@event.GetType().Name}'");
            }
        }
    }

    private void Apply(BookingCreated @event)
    {
        MeetingRoom = @event.MeetingRoom;
        StartTime = @event.StartTime;
        EndTime = @event.EndTime;
        Attendees = @event.Attendees;
    }

    private void Apply(BookedTimeSlotChanged @event)
    {
        StartTime = @event.StartTime;
        EndTime = @event.EndTime;
    }

    private void Apply(MeetingRoomChanged @event) =>
        MeetingRoom = @event.MeetingRoom;
    
    private void Apply(AttendeeAdded @event) =>
        Attendees = @event.Attendees;
    
    private void Apply(AttendeeRemoved @event) =>
        Attendees = @event.Attendees;
}

public record Attendee(string FirstName, string LastName, string Email);
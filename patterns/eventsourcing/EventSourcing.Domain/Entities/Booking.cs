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
    public MeetingSpace MeetingSpace { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }

    public List<IDomainEvent> Events => _events;

    /// <summary>
    /// Creates a new Booking instance with the specified ID, start time, and end time.
    /// Meant to be called from business logic to create a new booking
    /// </summary>
    /// <returns></returns>
    public Booking(Guid id, MeetingSpace meetingSpace, DateTime startTime, DateTime endTime)
    {
        Id = id;
        MeetingSpace = meetingSpace;
        StartTime = startTime;
        EndTime = endTime;
        _events.Add(new BookingCreated(meetingSpace, startTime, endTime));
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
        ApplyEvents(events);
    }

    public void Update(BookedTimeSlotChanged bookedTimeSlotChanged)
    {
        StartTime = bookedTimeSlotChanged.StartTime;
        EndTime = bookedTimeSlotChanged.EndTime;
        _events.Add(bookedTimeSlotChanged);
    }

    public void Update(MeetingSpaceChanged meetingSpaceChanged)
    {
        MeetingSpace = meetingSpaceChanged.MeetingSpace;
        _events.Add(meetingSpaceChanged);
    }

    private void ApplyEvents(List<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            _events.Add(@event);
            switch (@event)
            {
                case BookingCreated e: ApplyEvent(e); break;
                case BookedTimeSlotChanged e: ApplyEvent(e); break;
                case MeetingSpaceChanged e: ApplyEvent(e); break;
                default:
                    throw new NotImplementedException(
                        $"No Apply method found for event type '{@event.GetType().Name}'");
            }
        }
    }

    private void ApplyEvent(BookingCreated @event)
    {
        MeetingSpace = @event.MeetingSpace;
        StartTime = @event.StartTime;
        EndTime = @event.EndTime;
    }

    private void ApplyEvent(BookedTimeSlotChanged @event)
    {
        StartTime = @event.StartTime;
        EndTime = @event.EndTime;
    }

    private void ApplyEvent(MeetingSpaceChanged @event) =>
        MeetingSpace = @event.MeetingSpace;
}
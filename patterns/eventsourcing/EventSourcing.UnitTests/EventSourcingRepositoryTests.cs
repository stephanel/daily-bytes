using EventSourcing.Domain.Core;

namespace EventSourcing.UnitTests;

public class EventSourcingRepositoryTests
{
    private readonly EventSourcingRepository _repository = new();
    private readonly DateTime _now = DateTime.Now;

    [Fact]
    public void PersistInitialState()
    {
        // arrange
        var id = Guid.NewGuid();

        // act
        var booking = new Booking(id, MeetingSpace.Helsinki, _now, _now.AddHours(1));
        _repository.Save(booking);

        // arrange
        var loadedBooking = _repository.Load<Booking>(id);
        AssertBooking(booking, loadedBooking);
        AssertEvent(booking.Events[0], loadedBooking.Events[0]);
    }

    [Fact]
    public void PersistUpdatedStates()
    {
        // arrange
        var id = Guid.NewGuid();

        var booking = new Booking(id, MeetingSpace.Helsinki, _now, _now.AddDays(1));
        _repository.Save(booking);

        // act
        booking.Update(new BookedTimeSlotChanged(_now.AddHours(1), _now.AddHours(2)));
        _repository.Save(booking);

        booking.Update(new MeetingSpaceChanged(MeetingSpace.Paris));
        _repository.Save(booking);

        booking.Update(new BookedTimeSlotChanged(_now.AddHours(2), _now.AddHours(3)));
        _repository.Save(booking);

        // arrange
        var loadedBooking = _repository.Load<Booking>(id);
        Assert.Equal(4, loadedBooking.Events.Count);
        AssertBooking(booking, loadedBooking);
        AssertEvent(booking.Events[0], loadedBooking.Events[0]);
        AssertEvent(booking.Events[1], loadedBooking.Events[1]);
        AssertEvent(booking.Events[2], loadedBooking.Events[2]);
        AssertEvent(booking.Events[3], loadedBooking.Events[3]);
    }

    private void AssertBooking(Booking expected, Booking actual)
    {
        Assert.Equal(expected.MeetingSpace, actual.MeetingSpace);
        Assert.Equal(expected.StartTime, actual.StartTime);
        Assert.Equal(expected.EndTime, actual.EndTime);
    }

    private void AssertEvent<TDomainEvent>(TDomainEvent expected, TDomainEvent actual) where TDomainEvent : IDomainEvent
        => Assert.Equivalent(expected, actual);
}
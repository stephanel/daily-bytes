using EventSourcing.Domain.Core;

namespace EventSourcing.UnitTests;

public class InMemoryEventStoreTests
{
    private readonly InMemoryEventStore _repository = new();
    private readonly DateTime _now = DateTime.Now;

    [Fact]
    public void PersistInitialState()
    {
        // arrange
        // act
        var booking = new Booking(Guid.CreateVersion7(), MeetingRoom.Helsinki, _now, _now.AddHours(1));
        _repository.Save(booking);

        // arrange
        var loadedBooking = _repository.Load<Booking>(booking.Id);
        AssertBooking(booking, loadedBooking);
    }

    [Fact]
    public void PersistUpdatedStates()
    {
        // arrange
        var booking = new Booking(Guid.CreateVersion7(), MeetingRoom.Helsinki, _now, _now.AddDays(1));
        _repository.Save(booking);

        // act
        booking.UpdateTimeSlot(_now.AddHours(1), _now.AddHours(2));
        _repository.Save(booking);

        booking.UpdateMeetingRoom(MeetingRoom.Paris);
        _repository.Save(booking);

        booking.UpdateTimeSlot(_now.AddHours(2), _now.AddHours(3));
        _repository.Save(booking);

        // arrange
        var loadedBooking = _repository.Load<Booking>(booking.Id);
        AssertBooking(booking, loadedBooking);
    }

    [Fact]
    public void PreserveEventsOrder()
    {
        // arrange
        var booking = new Booking(Guid.CreateVersion7(), MeetingRoom.Helsinki, _now, _now.AddDays(1));
        booking.UpdateTimeSlot(_now.AddHours(1), _now.AddHours(2));
        booking.UpdateMeetingRoom(MeetingRoom.Paris);
        booking.UpdateTimeSlot(_now.AddHours(2), _now.AddHours(3));

        // act
        _repository.Save(booking);

        // assert
        var loadedBooking = _repository.Load<Booking>(booking.Id);
        Assert.Equal(booking.Events, loadedBooking.Events);
    }

    private void AssertBooking(Booking expected, Booking actual)
    {
        Assert.Equal(expected.MeetingRoom, actual.MeetingRoom);
        Assert.Equal(expected.StartTime, actual.StartTime);
        Assert.Equal(expected.EndTime, actual.EndTime);
    }

    private void AssertEvent<TDomainEvent>(TDomainEvent expected, TDomainEvent actual) where TDomainEvent : IDomainEvent
        => Assert.Equivalent(expected, actual);
}
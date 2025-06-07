namespace EventSourcing.UnitTests;

public class EventSourcingRepositoryTests
{
    private readonly EventSourcingRepository _repository = new();
    private readonly DateTime _now = DateTime.Now;

    [Fact]
    public void AppendInitialDomainEvents()
    {
        // act
        var booking = new Booking(Guid.NewGuid(), MeetingSpace.Helsinki, _now, _now.AddHours(1));
        _repository.Save(booking);

        // arrange
        Assert.Single(booking.Events);
        Assert.IsType<BookingCreated>(booking.Events[0]);
    }

    [Fact]
    public void PersistInitialState()
    {
        // arrange
        var id = Guid.NewGuid();

        // act
        var booking = new Booking(id, MeetingSpace.Helsinki,_now, _now.AddHours(1));
        _repository.Save(booking);

        // arrange
        var loadedBooking = _repository.Load<Booking>(id);
        AssertBooking(booking, loadedBooking);
    }

    [Fact]
    public void PersistUpdatedStates()
    {
        // arrange
        var id = Guid.NewGuid();

        var booking = new Booking(id, MeetingSpace.Helsinki,_now, _now.AddDays(1));
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
    }

    private void AssertBooking(Booking expected, Booking actual)
    {
        Assert.Equal(expected.MeetingSpace, actual.MeetingSpace);
        Assert.Equal(expected.StartTime, actual.StartTime);
        Assert.Equal(expected.EndTime, actual.EndTime);
    }
}

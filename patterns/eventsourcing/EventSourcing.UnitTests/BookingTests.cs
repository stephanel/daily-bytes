using Bogus;
using EventSourcing.Domain.Core;

namespace EventSourcing.UnitTests;

public class BookingTests
{
    private readonly IReadOnlyList<Attendee> _attendees =
        [NewAttendee()];

    [Fact]
    public void CreateNewBooking_Append_BookingCreated()
    {
        // arrange
        var room = MeetingRoom.Helsinki;
        var startTime = DateTime.Now;
        var endTime = startTime.AddHours(1);

        // act
        Booking booking = new Booking(Guid.NewGuid(), room, startTime, endTime, _attendees);

        // assert
        Assert.Equal(room, booking.MeetingRoom);
        Assert.Equal(startTime, booking.StartTime);
        Assert.Equal(endTime, booking.EndTime);
        Assert.Equal(_attendees, booking.Attendees);
        Assert.Single(booking.Events);
        Assert.IsType<BookingCreated>(booking.Events[0]);
    }

    [Fact]
    public void UpdateTimeSlot_Append_BookedTimeSlotChanged()
    {
        // arrange
        var room = MeetingRoom.Helsinki;
        var now = DateTime.Now;
        Booking booking = new Booking(Guid.NewGuid(), room, now, now.AddHours(1), _attendees);

        // act
        var newStartTime = now.AddHours(1);
        var newEndTime = now.AddHours(2);
        booking.UpdateTimeSlot(newStartTime, newEndTime);

        // assert
        Assert.Equal(newStartTime, booking.StartTime);
        Assert.Equal(newEndTime, booking.EndTime);

        Assert.Equal(2, booking.Events.Count);
        Assert.IsType<BookingCreated>(booking.Events[0]);
        Assert.IsType<BookedTimeSlotChanged>(booking.Events[1]);
    }

    [Fact]
    public void UpdateMeetingRoom_Append_MeetingRoomChanged()
    {
        // arrange
        var startTime = DateTime.Now;
        var endTime = startTime.AddHours(1);
        Booking booking = new Booking(Guid.NewGuid(), MeetingRoom.Helsinki, startTime, endTime, _attendees);

        // act
        var newMeetingRoom = MeetingRoom.Paris;
        booking.UpdateMeetingRoom(newMeetingRoom);

        // assert
        Assert.Equal(newMeetingRoom, booking.MeetingRoom);

        Assert.Equal(2, booking.Events.Count);
        Assert.IsType<BookingCreated>(booking.Events[0]);
        Assert.IsType<MeetingRoomChanged>(booking.Events[1]);
    }

    [Fact]
    public void AddNewAttendee_Append_MeetingRoomChanged()
    {
        // arrange
        var startTime = DateTime.Now;
        var endTime = startTime.AddHours(1);
        Booking booking = new Booking(Guid.NewGuid(), MeetingRoom.Helsinki, startTime, endTime, _attendees);

        // act
        var newAttendee = NewAttendee();
        booking.AddAttendee(newAttendee);

        // assert
        Assert.Equal([.._attendees, newAttendee], booking.Attendees);

        Assert.Equal(2, booking.Events.Count);
        Assert.IsType<BookingCreated>(booking.Events[0]);
        Assert.IsType<AttendeeAdded>(booking.Events[1]);
    }

    [Fact]
    public void BookingSubsequentUpdates()
    {
        // arrange
        var now = DateTime.Now;
        Booking booking = new Booking(Guid.NewGuid(), MeetingRoom.Helsinki, now, now.AddHours(1), _attendees);

        // act
        var newRoom = MeetingRoom.Paris;
        booking.UpdateMeetingRoom(newRoom);

        // act
        var newStartTime = now.AddHours(1);
        var newEndTime = now.AddHours(2);
        booking.UpdateTimeSlot(newStartTime, newEndTime);

        // assert
        Assert.Equal(newRoom, booking.MeetingRoom);
        Assert.Equal(newStartTime, booking.StartTime);
        Assert.Equal(newEndTime, booking.EndTime);

        Assert.Equal(3, booking.Events.Count);
        Assert.IsType<BookingCreated>(booking.Events[0]);
        Assert.IsType<MeetingRoomChanged>(booking.Events[1]);
        Assert.IsType<BookedTimeSlotChanged>(booking.Events[2]);
    }

    private static Attendee NewAttendee() =>
        new(new Faker().Person.FirstName, new Faker().Person.LastName, new Faker().Person.Email);
}
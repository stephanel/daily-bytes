namespace EventSourcing.UnitTests;

public class BookingTests
{
    [Fact]
    public void CreateNewBooking_Append_BookingCreated()
    {
        // arrange
        var meetingSpace = MeetingSpace.Helsinki;
        var startTime = DateTime.Now;
        var endTime = startTime.AddHours(1);

        // act
        Booking booking = new Booking(Guid.NewGuid(), meetingSpace, startTime, endTime);

        // assert
        Assert.Equal(meetingSpace, booking.MeetingSpace);
        Assert.Equal(startTime, booking.StartTime);
        Assert.Equal(endTime, booking.EndTime);
        Assert.Single(booking.Events);
        Assert.IsType<BookingCreated>(booking.Events[0]);
    }

    [Fact]
    public void UpdateTimeSlot_Append_BookedTimeSlotChanged()
    {
        // arrange
        var meetingSpace = MeetingSpace.Helsinki;
        var now = DateTime.Now;
        Booking booking = new Booking(Guid.NewGuid(), meetingSpace, now, now.AddHours(1));

        // act
        var newStartTime = now.AddHours(1);
        var newEndTime = now.AddHours(2);
        booking.Update(new BookedTimeSlotChanged(newStartTime, newEndTime));

        // assert
        Assert.Equal(meetingSpace, booking.MeetingSpace);
        Assert.Equal(newStartTime, booking.StartTime);
        Assert.Equal(newEndTime, booking.EndTime);

        Assert.Equal(2, booking.Events.Count);
        Assert.IsType<BookingCreated>(booking.Events[0]);
        Assert.IsType<BookedTimeSlotChanged>(booking.Events[1]);
    }

    [Fact]
    public void UpdateMeetingSpace_Append_MeetingSpaceChanged()
    {
        // arrange
        var startTime = DateTime.Now;
        var endTime = startTime.AddHours(1);
        Booking booking = new Booking(Guid.NewGuid(), MeetingSpace.Helsinki, startTime, endTime);

        // act
        var newMeetingSpace = MeetingSpace.Paris;
        booking.Update(new MeetingSpaceChanged(newMeetingSpace));

        // assert
        Assert.Equal(newMeetingSpace, booking.MeetingSpace);
        Assert.Equal(startTime, booking.StartTime);
        Assert.Equal(endTime, booking.EndTime);

        Assert.Equal(2, booking.Events.Count);
        Assert.IsType<BookingCreated>(booking.Events[0]);
        Assert.IsType<MeetingSpaceChanged>(booking.Events[1]);
    }

    [Fact]
    public void BookingSubsequentUpdates()
    {
        // arrange
        var now = DateTime.Now;
        Booking booking = new Booking(Guid.NewGuid(), MeetingSpace.Helsinki, now, now.AddHours(1));

        // act
        var newMeetingSpace = MeetingSpace.Paris;
        booking.Update(new MeetingSpaceChanged(newMeetingSpace));

        // act
        var newStartTime = now.AddHours(1);
        var newEndTime = now.AddHours(2);
        booking.Update(new BookedTimeSlotChanged(newStartTime, newEndTime));

        // assert
        Assert.Equal(newMeetingSpace, booking.MeetingSpace);
        Assert.Equal(newStartTime, booking.StartTime);
        Assert.Equal(newEndTime, booking.EndTime);

        Assert.Equal(3, booking.Events.Count);
        Assert.IsType<BookingCreated>(booking.Events[0]);
        Assert.IsType<MeetingSpaceChanged>(booking.Events[1]);
        Assert.IsType<BookedTimeSlotChanged>(booking.Events[2]);
    }

}

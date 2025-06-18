module EventSourcing.UnitTests

open System

open Bogus
open Xunit

open EventSourcing.Domain.Enums.MeetingRoom
open EventSourcing.Domain.Entities.Booking

let NewAttendee(): Attendee =
    let faker = new Faker()
    {
        FirstName = faker.Person.FirstName
        LastName = faker.Person.LastName
        Email = faker.Internet.Email()
    }


[<Fact>]
let ``Create new booking appends BookingCreated event`` () =
    // arrange
    let id = Guid.CreateVersion7()
    let room = MeetingRoom.Helsinki
    let startTime = DateTime.Now
    let endTime = startTime.AddHours(1)
    let attendees = ResizeArray<Attendee>([NewAttendee()])
    
    // act
    let booking = Booking(id, room, startTime, endTime, attendees)
    
    // assert
    Assert.Equal(room, booking.MeetingRoom)
    Assert.Equal(startTime, booking.StartTime);
    Assert.Equal(endTime, booking.EndTime);
    Assert.Equal(attendees.ToArray(), booking.Attendees)
    Assert.Single(booking.Events) |> ignore

    match booking.Events[0] with
        | BookingCreated(actualId, actualMeetingRoom, actualStartTime, actualEndTime, actualAttendees) ->
            Assert.Equal(id, actualId)
            Assert.Equal(room, actualMeetingRoom)
            Assert.Equal(startTime, actualStartTime)
            Assert.Equal(endTime, actualEndTime)
            Assert.Equal(attendees.ToArray(), actualAttendees)
        | _ -> Assert.Fail("The event is not a BookingCreated event")

[<Fact>]
let ``Update time slot appends BookedTimeSlotChanged event`` () =
    // arrange
    let initialStartTime = DateTime.Now
    let initialEndTime = initialStartTime.AddHours(1)
    let attendees = ResizeArray<Attendee>([NewAttendee()])
    let booking = Booking(Guid.CreateVersion7(), MeetingRoom.Helsinki, initialStartTime, initialEndTime, attendees)

    // act
    let newStartTime = initialStartTime.AddHours(1)
    let newEndTime = newStartTime.AddHours(2)
    booking.UpdateTimeSlot(newStartTime, newEndTime)
    
    // assert
    Assert.Equal(booking.StartTime, booking.StartTime);
    Assert.Equal(booking.EndTime, booking.EndTime)
    Assert.Equal(2, booking.Events.Count) |> ignore
    match booking.Events[0] with
        | BookingCreated(actualId, actualMeetingRoom, actualStartTime, actualEndTime, actualAttendees) ->
            Assert.Equal(booking.Id, actualId)
            Assert.Equal(booking.MeetingRoom, actualMeetingRoom)
            Assert.Equal(initialStartTime, actualStartTime)
            Assert.Equal(initialEndTime, actualEndTime)
            Assert.Equal(booking.Attendees.ToArray(), actualAttendees)
        | _ -> Assert.Fail("The first event is not a BookingCreated event")
    match booking.Events[1] with
        | BookedTimeSlotChanged(actualStartTime, actualEndTime) ->
            Assert.Equal(newStartTime, actualStartTime)
            Assert.Equal(newEndTime, actualEndTime)
        | _ -> Assert.Fail("The second event is not a BookedTimeSlotChanged event")

    
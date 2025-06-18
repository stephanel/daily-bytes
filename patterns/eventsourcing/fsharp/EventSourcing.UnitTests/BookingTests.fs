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
    let attendees: List<Attendee> = [NewAttendee()]
    
    // act
    let booking = Booking(id, room, startTime, endTime, attendees)
    
    // assert
    Assert.Equal(room, booking.MeetingRoom)
    Assert.Equal(startTime, booking.StartTime);
    Assert.Equal(endTime, booking.EndTime);
    Assert.Equal(attendees, booking.Attendees)
    Assert.Single(booking.Events) |> ignore

    match booking.Events[0] with
        | BookingCreated(actualId, actualMeetingRoom, actualStartTime, actualEndTime, actualAttendees) ->
            Assert.Equal(id, actualId)
            Assert.Equal(room, actualMeetingRoom)
            Assert.Equal(startTime, actualStartTime)
            Assert.Equal(endTime, actualEndTime)
            Assert.Equal(attendees, actualAttendees)
        | _ -> Assert.Fail("The event is not a BookingCreated event")
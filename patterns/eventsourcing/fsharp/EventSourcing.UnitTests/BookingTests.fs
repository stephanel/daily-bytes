// ReSharper disable FSharpRedundantUnionCaseFieldPatterns

module EventSourcing.UnitTests.BookingTests

open System

open Bogus
open Xunit

open EventSourcing.Domain.Enums.MeetingRoom
open EventSourcing.Domain.Entities.Booking

let NewAttendee () : Attendee =
    let faker = Faker()

    { FirstName = faker.Person.FirstName
      LastName = faker.Person.LastName
      Email = faker.Internet.Email() }


[<Fact>]
let ``Create new booking appends BookingCreated event`` () =
    // arrange
    let id = Guid.CreateVersion7()
    let room = MeetingRoom.Helsinki
    let startTime = DateTime.Now
    let endTime = startTime.AddHours(1)
    let attendees = [ NewAttendee() ]

    // act
    let booking = Booking.Create(id, room, startTime, endTime, attendees)

    // assert
    Assert.Equal(room, booking.MeetingRoom)
    Assert.Equal(startTime, booking.StartTime)
    Assert.Equal(endTime, booking.EndTime)
    Assert.Equal<Attendee>(attendees, booking.Attendees)

    let events = booking.Events
    Assert.Single(events) |> ignore

    match events[0] with
    | BookingCreated(actualId, actualMeetingRoom, actualStartTime, actualEndTime, actualAttendees) ->
        Assert.Equal(id, actualId)
        Assert.Equal(room, actualMeetingRoom)
        Assert.Equal(startTime, actualStartTime)
        Assert.Equal(endTime, actualEndTime)
        Assert.Equal<Attendee>(attendees, actualAttendees)
    | _ -> Assert.Fail("The event is not a BookingCreated event")

[<Fact>]
let ``Update time slot appends BookedTimeSlotChanged event`` () =
    // arrange
    let initialStartTime = DateTime.Now
    let initialEndTime = initialStartTime.AddHours(1)
    let attendees = [ NewAttendee() ]

    let booking =
        Booking.Create(Guid.CreateVersion7(), MeetingRoom.Helsinki, initialStartTime, initialEndTime, attendees)

    // act
    let newStartTime = initialStartTime.AddHours(1)
    let newEndTime = newStartTime.AddHours(2)
    let booking = booking.UpdateTimeSlot(newStartTime, newEndTime)

    // assert
    Assert.Equal(booking.StartTime, booking.StartTime)
    Assert.Equal(booking.EndTime, booking.EndTime)

    let events = booking.Events
    Assert.Equal(2, events.Length)

    match events[0] with
    | BookingCreated(_, _, _, _, _) -> ()
    | _ -> Assert.Fail("The first event is not a BookingCreated event")

    match events[1] with
    | BookedTimeSlotChanged(actualStartTime, actualEndTime) ->
        Assert.Equal(newStartTime, actualStartTime)
        Assert.Equal(newEndTime, actualEndTime)
    | _ -> Assert.Fail("The second event is not a BookedTimeSlotChanged event")

[<Fact>]
let ``Update meeting room appends MeetingRoomChanged event`` () =
    // arrange
    let now = DateTime.Now
    let initialRoom = MeetingRoom.Helsinki

    let booking =
        Booking.Create(Guid.CreateVersion7(), initialRoom, now, now.AddHours(1), [ NewAttendee() ])

    // act
    let newRoom = MeetingRoom.Paris
    let booking = booking.UpdateMeetingRoom newRoom

    // assert
    Assert.Equal(newRoom, booking.MeetingRoom)

    let events = booking.Events
    Assert.Equal(2, events.Length)

    match events[0] with
    | BookingCreated(_, _, _, _, _) -> ()
    | _ -> Assert.Fail("The first event is not a BookingCreated event")

    match events[1] with
    | MeetingRoomChanged(actualRoom) -> Assert.Equal(newRoom, actualRoom)
    | _ -> Assert.Fail("The second event is not a MeetingRoomChanged event")

[<Fact>]
let ``Add new attendee appends AttendeeAdded event`` () =
    // arrange
    let now = DateTime.Now
    let attendees = [ NewAttendee() ]

    let booking =
        Booking.Create(Guid.CreateVersion7(), MeetingRoom.Helsinki, now, now.AddHours(1), attendees)

    // act
    let newAttendee = NewAttendee()
    let booking = booking.AddAttendee newAttendee

    // assert
    Assert.Equal<Attendee>(attendees @ [ newAttendee ], booking.Attendees)

    let events = booking.Events
    Assert.Equal(2, events.Length)

    match events[0] with
    | BookingCreated(_, _, _, _, _) -> ()
    | _ -> Assert.Fail("The first event is not a BookingCreated event")

    match events[1] with
    | AttendeeAdded(actualAttendees) -> Assert.Equal<Attendee>(attendees @ [ newAttendee ], actualAttendees)
    | _ -> Assert.Fail("The second event is not a AttendeeAdded event")

[<Fact>]
let ``Remove an attendee appends AttendeeRemoved event`` () =
    // arrange
    let now = DateTime.Now
    let attendees = [ NewAttendee(); NewAttendee() ]

    let booking =
        Booking.Create(Guid.CreateVersion7(), MeetingRoom.Helsinki, now, now.AddHours(1), attendees)

    // act
    let booking = booking.RemoveAttendee(attendees[0])

    // assert
    let expectedAttendees = attendees |> List.filter (fun a -> a <> attendees[0])
    Assert.Equal<Attendee>(expectedAttendees, booking.Attendees)

    let events = booking.Events
    Assert.Equal(2, events.Length)

    match events[0] with
    | BookingCreated(_, _, _, _, _) -> ()
    | _ -> Assert.Fail("The first event is not a BookingCreated event")

    match events[1] with
    | AttendeeRemoved(actualAttendees) -> Assert.Equal<Attendee>(expectedAttendees, actualAttendees)
    | _ -> Assert.Fail("The second event is not a AttendeeRemoved event")

[<Fact>]
let ``Booking subsequent updates`` () =
    // arrange
    let now = DateTime.Now
    let initialAttendee = NewAttendee()

    let booking =
        Booking.Create(
            Guid.CreateVersion7(),
            MeetingRoom.Helsinki,
            now,
            now.AddHours(1),
            [ NewAttendee(); initialAttendee ]
        )

    // act
    let newRoom = MeetingRoom.Paris
    let booking = booking.UpdateMeetingRoom(newRoom)

    // act
    let newStartTime = now.AddHours(1)
    let newEndTime = now.AddHours(2)
    let booking = booking.UpdateTimeSlot(newStartTime, newEndTime)

    // act
    let booking = booking.RemoveAttendee(booking.Attendees.Head)

    // act
    let newAttendee = NewAttendee()
    let booking = booking.AddAttendee(newAttendee)

    // assert
    Assert.Equal(newRoom, booking.MeetingRoom)
    Assert.Equal(newStartTime, booking.StartTime)
    Assert.Equal(newEndTime, booking.EndTime)
    Assert.Equal<Attendee>([ initialAttendee; newAttendee ], booking.Attendees)

    let events = booking.Events
    Assert.Equal(5, events.Length)

    match events[0] with
    | BookingCreated(_, _, _, _, _) -> ()
    | _ -> Assert.Fail("The first event is not a BookingCreated event")

    match events[1] with
    | MeetingRoomChanged _ -> ()
    | _ -> Assert.Fail("The second event is not a MeetingRoomChanged event")

    match events[2] with
    | BookedTimeSlotChanged _ -> ()
    | _ -> Assert.Fail("The third event is not a BookedTimeSlotChanged event")

    match events[3] with
    | AttendeeRemoved _ -> ()
    | _ -> Assert.Fail("The second event is not a AttendeeRemoved event")

    match events[4] with
    | AttendeeAdded _ -> ()
    | _ -> Assert.Fail("The second event is not a AttendeeAdded event")

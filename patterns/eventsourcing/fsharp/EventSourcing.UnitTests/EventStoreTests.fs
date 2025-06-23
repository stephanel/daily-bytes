module EventSourcing.UnitTests.EventStoreTests

open System

open Bogus
open Xunit

open EventSourcing.Domain.Enums
open EventSourcing.Domain.Entities.Booking
open EventSourcing.Infrastructure.EventStore

let NewAttendee () : Attendee =
    let faker = new Faker()

    { FirstName = faker.Person.FirstName
      LastName = faker.Person.LastName
      Email = faker.Internet.Email() }

let AssertState(expected: Booking) =
    match load expected.Id with
    | Some actual -> Assert.Equal(expected, actual)
    | None -> Assert.Fail("Booking was not loaded from the event store")
    
[<Fact>]
let ``Persists initial state`` () =
    // arrange
    let now = DateTime.UtcNow
    let booking =
        Booking.Create(Guid.CreateVersion7(), MeetingRoom.Helsinki, now, now.AddHours(1), [ NewAttendee() ])

    // act
    save booking

    // assert
    AssertState booking

[<Fact>]
let ``Persists updated states`` () =
    // arrange
    let now = DateTime.UtcNow
    let booking =
        Booking.Create(Guid.CreateVersion7(), MeetingRoom.Helsinki, now, now.AddHours(1), [ NewAttendee() ])
    save booking

    // act
    let booking = booking.UpdateTimeSlot(now.AddHours(1), now.AddHours(2))
    save booking

    // act
    let booking = booking.UpdateMeetingRoom(MeetingRoom.Paris)
    save booking

    // act
    let booking = booking.AddAttendee(NewAttendee())
    save booking

    // act
    let booking = booking.RemoveAttendee(booking.Attendees |> List.head)
    save booking

    // assert
    AssertState booking

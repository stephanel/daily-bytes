module EventSourcing.Domain.Entities.Booking

open System.Collections.Generic
open System

open EventSourcing.Domain.Enums.MeetingRoom

type Attendee = {
    FirstName: string
    LastName: string
    Email: string
}

// type BookingCreated = {
//     Id: Guid
//     MeetingRoom: MeetingRoom
//     StartTime: DateTime
//     EndTime: DateTime
//     Attendees: IReadOnlyList<Attendee>
// }

type Event =
    | BookingCreated of id : Guid * meetingRoom: MeetingRoom * startTime: DateTime * endTime: DateTime * attendees: IReadOnlyList<Attendee>

type Booking(id0: Guid, meetingRoom0: MeetingRoom, startTime0: DateTime, endTime0: DateTime, attendees0: IReadOnlyList<Attendee>) =
    let mutable id = id0
    let mutable meetingRoom = meetingRoom0
    let mutable startTime = startTime0
    let mutable endTime = endTime0
    let mutable attendees = attendees0
    let mutable events : IReadOnlyList<Event> = []
    do
        events <- [BookingCreated( id = id, meetingRoom = meetingRoom, startTime = startTime, endTime = endTime, attendees = attendees)]
    member this.MeetingRoom with get() = meetingRoom
    member this.StartTime with get() = startTime
    member this.EndTime with get() = endTime
    member this.Attendees with get() = attendees
    member this.Events with get() = events


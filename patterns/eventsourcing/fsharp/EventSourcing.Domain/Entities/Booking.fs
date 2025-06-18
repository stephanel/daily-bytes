module EventSourcing.Domain.Entities.Booking

open System

open EventSourcing.Domain.Enums.MeetingRoom

type Attendee =
    { FirstName: string
      LastName: string
      Email: string }

// type BookingCreated = {
//     Id: Guid
//     MeetingRoom: MeetingRoom
//     StartTime: DateTime
//     EndTime: DateTime
//     Attendees: IReadOnlyList<Attendee>
// }

type Event =
    | BookingCreated of
        id: Guid *
        meetingRoom: MeetingRoom *
        startTime: DateTime *
        endTime: DateTime *
        attendees: ResizeArray<Attendee>
    | BookedTimeSlotChanged of startTime: DateTime * endTime: DateTime

type Booking
    (id0: Guid, meetingRoom0: MeetingRoom, startTime0: DateTime, endTime0: DateTime, attendees0: ResizeArray<Attendee>)
    =
    let mutable id = id0
    let mutable meetingRoom = meetingRoom0
    let mutable startTime = startTime0
    let mutable endTime = endTime0
    let mutable attendees = attendees0
    let mutable events = ResizeArray<Event>()

    do
        events <-
            ResizeArray<Event>(
                [ BookingCreated(
                      id = id,
                      meetingRoom = meetingRoom,
                      startTime = startTime,
                      endTime = endTime,
                      attendees = attendees
                  ) ]
            )

    member this.Id = id
    member this.MeetingRoom = meetingRoom
    member this.StartTime = startTime
    member this.EndTime = endTime
    member this.Attendees = attendees
    member this.Events = events

    member this.UpdateTimeSlot(newStartTime: DateTime, newEndTime: DateTime) =
        startTime <- newStartTime
        endTime <- newEndTime
        events.Add(BookedTimeSlotChanged(startTime = startTime, endTime = endTime))

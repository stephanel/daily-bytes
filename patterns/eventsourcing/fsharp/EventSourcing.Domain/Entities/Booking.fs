module EventSourcing.Domain.Entities.Booking

open System

open EventSourcing.Domain.Enums.MeetingRoom

type Attendee =
    { FirstName: string
      LastName: string
      Email: string }

type Event =
    | BookingCreated of
        id: Guid *
        meetingRoom: MeetingRoom *
        startTime: DateTime *
        endTime: DateTime *
        attendees: Attendee List
    | BookedTimeSlotChanged of startTime: DateTime * endTime: DateTime
    | MeetingRoomChanged of meetingRoom : MeetingRoom
    | AttendeeAdded of attendees : Attendee List
    | AttendeeRemoved of attendees : Attendee List

type Booking
    (id0: Guid, meetingRoom0: MeetingRoom, startTime0: DateTime, endTime0: DateTime, attendees0: Attendee List)
    =
    let id = id0
    let mutable meetingRoom = meetingRoom0
    let mutable startTime = startTime0
    let mutable endTime = endTime0
    let mutable attendees = attendees0
    let mutable events : Event List = []

    do
        events <- List.append [] [BookingCreated(id, meetingRoom, startTime, endTime, attendees)]

    member this.Id = id
    member this.MeetingRoom = meetingRoom
    member this.StartTime = startTime
    member this.EndTime = endTime
    member this.Attendees = attendees
    
    member this.Events=
        events |> Seq.toList

    member this.UpdateTimeSlot(newStartTime: DateTime, newEndTime: DateTime) =
        startTime <- newStartTime
        endTime <- newEndTime
        events <- List.append events [BookedTimeSlotChanged(startTime = startTime, endTime = endTime)]

    member this.UpdateMeetingRoom(newMeetingRoom: MeetingRoom) =
        meetingRoom <- newMeetingRoom
        events <- List.append events [MeetingRoomChanged(meetingRoom = meetingRoom)]
        
    member this.AddAttendee(newAttendee: Attendee) =
        attendees <- attendees @ [newAttendee]
        events <- List.append events [AttendeeAdded(attendees = attendees)]
        
    member this.RemoveAttendee(attendeeToRemove: Attendee) =
        attendees <- attendees |> List.filter (fun a -> a <> attendeeToRemove)
        events <- List.append events [AttendeeRemoved(attendees = attendees)]
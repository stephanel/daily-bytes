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
    | MeetingRoomChanged of meetingRoom: MeetingRoom
    | AttendeeAdded of attendees: Attendee list
    | AttendeeRemoved of attendees: Attendee list

type Booking =
    { Id: Guid
      MeetingRoom: MeetingRoom
      StartTime: DateTime
      EndTime: DateTime
      Attendees: Attendee list
      Events: Event list }

type Booking with
    static member Create
        (id: Guid, meetingRoom: MeetingRoom, startTime: DateTime, endTime: DateTime, attendees: Attendee list)
        =
        let initialEvent = BookingCreated(id, meetingRoom, startTime, endTime, attendees)

        { Id = id
          MeetingRoom = meetingRoom
          StartTime = startTime
          EndTime = endTime
          Attendees = attendees
          Events = [ initialEvent ] }

    static member Create(id: Guid, events: Event list) =
        match events with
        | [] -> failwithf $"Cannot create Booking with ID '%A{id}' from an empty list of events"

        | BookingCreated(id0, room, startTime, endTime, attendees) :: otherEvents ->
            if id0 <> id then
                failwithf $"Mismatched state ID during rehydration. Expected %A{id}, but first event has %A{id0}."

            let initialEvent = BookingCreated(id0, room, startTime, endTime, attendees)

            let initialState =
                { Id = id0
                  MeetingRoom = room
                  StartTime = startTime
                  EndTime = endTime
                  Attendees = attendees
                  Events = [ initialEvent ] }

            otherEvents
            |> List.fold (fun (currentState: Booking) e -> currentState.Apply(e)) initialState

        | _ ->
            failwithf
                $"Cannot create Booking with ID '%A{id}' from a list of events that does not start with BookingCreated event."


    member this.UpdateTimeSlot(newStartTime: DateTime, newEndTime: DateTime) =
        { this with
            StartTime = newStartTime
            EndTime = newEndTime
            Events =
                this.Events
                @ [ BookedTimeSlotChanged(startTime = newStartTime, endTime = newEndTime) ] }

    member this.UpdateMeetingRoom(newMeetingRoom: MeetingRoom) =
        { this with
            MeetingRoom = newMeetingRoom
            Events = this.Events @ [ MeetingRoomChanged(meetingRoom = newMeetingRoom) ] }

    member this.AddAttendee(newAttendee: Attendee) =
        let attendees = this.Attendees @ [ newAttendee ]

        { this with
            Attendees = attendees
            Events = this.Events @ [ AttendeeAdded(attendees = attendees) ] }

    member this.RemoveAttendee(attendeeToRemove: Attendee) =
        let attendees = this.Attendees |> List.filter (fun a -> a <> attendeeToRemove)

        { this with
            Attendees = attendees
            Events = this.Events @ [ AttendeeRemoved(attendees = attendees) ] }

    member private this.Apply(event: Event) =
        match event with
        | BookingCreated(_, meetingRoom0, startTime0, endTime0, attendees0) ->
            { this with
                MeetingRoom = meetingRoom0
                StartTime = startTime0
                EndTime = endTime0
                Attendees = attendees0
                Events = [ event ] }
        | BookedTimeSlotChanged(startTime0, endTime0) ->
            { this with
                StartTime = startTime0
                EndTime = endTime0
                Events = this.Events @ [ event ] }
        | MeetingRoomChanged(meetingRoom0) ->
            { this with
                MeetingRoom = meetingRoom0
                Events = this.Events @ [ event ] }
        | AttendeeAdded(attendees0) ->
            { this with
                Attendees = attendees0
                Events = this.Events @ [ event ] }
        | AttendeeRemoved(attendees0) ->
            { this with
                Attendees = attendees0
                Events = this.Events @ [ event ] }

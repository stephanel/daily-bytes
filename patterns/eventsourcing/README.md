# Event Sourcing

## Aggregates
- `Booking`: entity that reprensent a booking of a meeting room 

## Events:
- `BookingCreated`: initial event appended when a new booking is created
- `BookedTimeSlotChanged`: appended when the booked start and/or end times changed
- `MeetingRoomChanged`: appended when the booked meeting room is changed

## Persistence
The implementation of the events persistence reads from/writes to an in-memory event store.

The model `EventEntry` represents a record in database and holds the .NET type of the event, and the event serialized to JSON.

# Event Sourcing

## Aggregates
- `Booking`: entity that reprensent a booking of a meeting room 

## Events:
- `BookingCreated`: initial event appended when a new booking is created
- `BookedTimeSlotChanged`: appended when the booked start and/or end times changed
- `MeetingSpaceChanged`: appended when the booked meeting space is changed

## Persistence
The implementation of the events persistence fakes the serilization/deserialization of the events when it respectively reads from/writes to a database.

The model `EventEntry` represents a record in daatabase and holds the .NET type of the event, and the event serialized to JSON.

:warning: for this experiment, a dictionary is used to persist.
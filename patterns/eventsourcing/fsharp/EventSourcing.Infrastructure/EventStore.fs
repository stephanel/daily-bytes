module EventSourcing.Infrastructure.EventStore

open System
open System.Collections.Generic
open System.Text.Json
open System.Text.Json.Serialization

open EventSourcing.Domain.Entities.Booking


let jsonOptions () : JsonSerializerOptions =
    let options = JsonSerializerOptions()

    options.Converters.Add(
        JsonFSharpConverter(
            unionEncoding =
                (JsonUnionEncoding.InternalTag
                 ||| JsonUnionEncoding.NamedFields
                 ||| JsonUnionEncoding.UnwrapFieldlessTags
                 ||| JsonUnionEncoding.UnwrapOption)
        )
    )

    options

type EventEntry =
    { dotNetType: string
      payload: string
      version: int }

let private store: Dictionary<Guid, EventEntry list> =
    Dictionary<Guid, EventEntry list>()

let save =
    fun (booking: Booking) ->

        let serialize (e: Event) : string =
            JsonSerializer.Serialize(e, jsonOptions ())

        let map (e: Event) : EventEntry =
            { dotNetType = e.GetType().FullName
              payload = serialize (e)
              version = 1 } // Or determine actual version

        let entries = booking.Events |> List.map map // Transform and convert to array

        match store.TryGetValue(booking.Id) with
        | true, existingEvents -> store[booking.Id] <- entries
        | false, _ -> store.Add(booking.Id, entries)

let load (id: Guid) : Option<Booking> =
    let map (e: EventEntry) : Event = JsonSerializer.Deserialize(e.payload, jsonOptions ())
    let events = store[id] |> List.map map
    let booking = Booking.Create(id, events)
    Some(booking)

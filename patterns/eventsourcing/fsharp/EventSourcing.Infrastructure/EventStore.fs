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

type EventEntry with
    static member Empty =
        { dotNetType = String.Empty
          payload = String.Empty
          version = 0 }

let private store: Dictionary<Guid, EventEntry list> =
    Dictionary<Guid, EventEntry list>()

let save =
    fun (booking: Booking) ->

        let serialize (e: Event) : string =
            JsonSerializer.Serialize(e, jsonOptions ())

        let map (version: int, e: Event) : EventEntry =
            { dotNetType = e.GetType().FullName
              payload = serialize (e)
              version = version + 1 }

        let entries = booking.Events |> List.indexed |> List.map map

        if not (store.ContainsKey(booking.Id)) then
            store.Add(booking.Id, [])

        let maxVersion =
            match store[booking.Id] |> List.map (fun e -> e.version) with
            | [] -> 0
            | entries -> entries |> List.max

        let toAppend = entries |> List.filter (fun e -> e.version > maxVersion)
        
        store[booking.Id] <- toAppend |> List.append store[booking.Id]
        
let load (id: Guid) : Option<Booking> =
    let map (e: EventEntry) : Event =
        JsonSerializer.Deserialize(e.payload, jsonOptions ())

    let events = store[id] |> List.map map
    let booking = Booking.Create(id, events)
    Some(booking)

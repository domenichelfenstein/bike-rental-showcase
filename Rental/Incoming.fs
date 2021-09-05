namespace BikeRental.Rental

open BikeRental

type RentalStorages =
    { Bikes: BikeStorage
      BookingEvents: BookingEventStorage }

type RentalStorageContext =
    | InMemory
    | Mixed of BikeJsonStorage.JsonContext

module RentalStorageCreator =
    let create (ctx: RentalStorageContext) =
        match ctx with
        | Mixed json ->
            { Bikes = BikeJsonStorage.create json
              BookingEvents = BookingEventInMemoryStorage.create () }
        | _ -> failwith "not implemented"

type RentalServices =
    {
        GetInstant: unit -> Instant
        WithdrawAmount: Amount -> UserId -> Async<bool>
    }

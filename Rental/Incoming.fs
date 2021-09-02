namespace BikeRental.Rental

type RentalStorages =
    { Bikes: BikeStorage }

type RentalStorageContext =
    | InMemory
    | Json of BikeJsonStorage.JsonContext

module RentalStorageCreator =
    let create (ctx: RentalStorageContext) =
        match ctx with
        | Json json ->
            { Bikes = BikeJsonStorage.create json }
        | _ -> failwith "not implemented"

type RentalServices = {
    GetNodaInstant : unit -> NodaTime.Instant
}

namespace BikeRental.Rental

type RentalStorages =
    { Placeholder : unit }

type SqlContext = { ConnectionString: string }

type RentalStorageContext =
    | InMemory
    | Sql of SqlContext

module RentalStorageCreator =
    let create (ctx: RentalStorageContext) =
        match ctx with
        | InMemory ->
            { Placeholder = () }
        | _ -> failwith "not implemented"

type RentalServices = {
    GetNodaInstant : unit -> NodaTime.Instant
}

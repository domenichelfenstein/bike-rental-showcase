namespace BikeRental.Starter

open System
open BikeRental

module Services =
    let getInstant () =
        DateTime.UtcNow |> NodaTime.Instant.FromDateTimeUtc |> Instant

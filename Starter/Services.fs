namespace BikeRental.Starter

open System

module Services =
    let getNodaInstant () =
        DateTime.UtcNow
        |> NodaTime.Instant.FromDateTimeUtc

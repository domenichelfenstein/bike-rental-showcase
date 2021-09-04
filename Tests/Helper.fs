namespace BikeRental

[<RequireQualifiedAccess>]
module NodaInstant =
    let parse s =
        s
        |> System.DateTime.Parse
        |> (fun x -> x.ToUniversalTime())
        |> NodaTime.Instant.FromDateTimeUtc

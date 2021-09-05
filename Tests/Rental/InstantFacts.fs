module BikeRental.Rental.InstantFacts

open BikeRental
open Xunit
open Swensen.Unquote

[<Fact>]
let ``is later`` () =
    let a = "2021-09-05 09:00:01" |> Instant.parse
    let b = "2021-09-05 09:00:00" |> Instant.parse

    a > b =! true
    a < b =! false
    a >= b =! true

[<Fact>]
let ``is earlier`` () =
    let a = "2021-09-05 09:00:00" |> Instant.parse
    let b = "2021-09-05 09:00:01" |> Instant.parse

    a < b =! true
    a > b =! false
    a <= b =! true

[<Fact>]
let ``is same`` () =
    let a = "2021-09-05 09:00:00" |> Instant.parse
    let b = "2021-09-05 09:00:00" |> Instant.parse

    a = b =! true

﻿module BikeRental.Rental.Booking.StatusComputationFacts

open BikeRental
open BikeRental.Rental
open NUnit.Framework
open Testing
open Swensen.Unquote

[<Test>]
let ``Empty list`` () =
    let result = Booking.getStatusOfBike (Example.create ()) (Example.create ()) []

    result =! Bookable

[<Test>]
let ``Unreleased booking before`` () = 
    let queryInstant = "2021-09-04 09:00" |> Instant.parse

    let booking =
        { Example.create<Booking> () with
            Start = "2021-09-04 08:00" |> Instant.parse
            End = None }

    let result = Booking.getStatusOfBike queryInstant (Example.create ()) [ booking ]

    result =! NotAvailable

[<Test>]
let ``Unreleased booking before from same user`` () =
    let queryInstant = "2021-09-04 09:00" |> Instant.parse

    let booking =
        { Example.create<Booking> () with
            Start = "2021-09-04 08:00" |> Instant.parse
            End = None }

    let result = Booking.getStatusOfBike queryInstant booking.UserId [ booking ]

    result =! Releasable booking.BookingId

[<Test>]
let ``Unreleased booking after`` () =
    let queryInstant = "2021-09-04 09:00" |> Instant.parse

    let booking =
        { Example.create<Booking> () with
            Start = "2021-09-04 10:00" |> Instant.parse
            End = None }

    let result = Booking.getStatusOfBike queryInstant (Example.create ()) [ booking ]

    result =! Bookable

[<Test>]
let ``Released booking before`` () =
    let queryInstant = "2021-09-04 09:00" |> Instant.parse

    let booking =
        { Example.create<Booking> () with
            Start = "2021-09-04 08:00" |> Instant.parse
            End = "2021-09-04 08:59" |> Instant.parse |> Some }

    let result = Booking.getStatusOfBike queryInstant (Example.create ()) [ booking ]

    result =! Bookable

[<Test>]
let ``Released booking in future`` () =
    let queryInstant = "2021-09-04 09:00" |> Instant.parse

    let booking =
        { Example.create<Booking> () with
            Start = "2021-09-04 08:00" |> Instant.parse
            End = "2021-09-04 09:01" |> Instant.parse |> Some }

    let result = Booking.getStatusOfBike queryInstant (Example.create ()) [ booking ]

    result =! NotAvailable

[<Test>]
let ``Released booking with unreleased booking. Both before`` () =
    let queryInstant = "2021-09-04 09:00" |> Instant.parse

    let booking1 =
        { Example.create<Booking> () with
            Start = "2021-09-04 07:00" |> Instant.parse
            End = "2021-09-04 07:30" |> Instant.parse |> Some }

    let booking2 =
        { booking1 with
            Start = "2021-09-04 08:00" |> Instant.parse
            End = None }

    let result =
        Booking.getStatusOfBike queryInstant (Example.create ()) [ booking1; booking2 ]

    result =! NotAvailable

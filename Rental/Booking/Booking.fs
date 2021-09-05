namespace BikeRental.Rental

open BikeRental
open FSharpx.Collections

type Booking =
    { BookingId: BookingId
      BikeId: BikeId
      UserId: UserId
      Start: Instant
      End: Instant option }

type AvailabilityStatus =
    | Bookable
    | NotAvailable
    | Releasable of BookingId

[<RequireQualifiedAccess>]
module Booking =
    let projectSingle (events: BookingEvent list) =
        events
        |> List.sortBy
            (fun x ->
                let (Instant instant) = x.Instant
                instant.ToUnixTimeTicks())
        |> List.fold
            (fun bookingOption event ->
                match bookingOption, event.Data with
                | _, Booked userId ->
                    Some
                        { BookingId = event.BookingId
                          UserId = userId
                          Start = event.Instant
                          BikeId = event.BikeId
                          End = None }
                | Some booking, Released ->
                    Some
                        { booking with
                              End = Some event.Instant }
                | _, _ -> None)
            None

    let projectMultiple (events: BookingEvent list) =
        events
        |> List.groupBy (fun x -> x.BookingId)
        |> List.map (fun (_, g) -> g |> projectSingle)
        |> List.choose id

    let getStatusOfBike (Instant instant) userId (bookingsOfBike: Booking list) =
        let ticks = instant.ToUnixTimeTicks()

        let unreleasedBookings =
            bookingsOfBike
            |> List.filter (fun { Start = (Instant start) } -> start.ToUnixTimeTicks() <= ticks)
            |> List.filter
                (fun b ->
                    match b.End with
                    | None -> true
                    | Some (Instant x) -> x.ToUnixTimeTicks() >= ticks)

        let hasUnreleasedBookings = unreleasedBookings |> List.isEmpty |> not

        match hasUnreleasedBookings with
        | true ->
            let lastBooking = unreleasedBookings |> List.last
            match lastBooking.UserId = userId with
            | true -> Releasable lastBooking.BookingId
            | false -> NotAvailable
        | false -> Bookable

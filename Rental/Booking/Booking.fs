namespace BikeRental.Rental

type Booking =
    { BookingId: BookingId
      BikeId: BikeId
      UserId: UserId
      Start: Instant
      End: Instant option }

type AvailabilityStatus =
    | Bookable
    | NotAvailable

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

    let getStatusOfBike (Instant instant) (bookingsOfBike: Booking list) =
        let ticks = instant.ToUnixTimeTicks()

        let bookingsUntilNow =
            bookingsOfBike
            |> List.filter (fun { Start = (Instant start) } -> start.ToUnixTimeTicks() <= ticks)

        let isAvailable =
            bookingsUntilNow
            |> List.filter (fun b -> b.End.IsSome)
            |> List.isEmpty

        match isAvailable with
        | true -> Bookable
        | false -> NotAvailable

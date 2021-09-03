namespace BikeRental.Rental.Features

open System
open BikeRental.Rental
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module RentBike =
    type Data = { BikeId: BikeId; UserId: UserId }

    let execute
        (persistBookingEvent: BookingEvent -> Async<unit>)
        (queryBookingEventsOfBike: BikeId -> Async<BookingEvent list>)
        (queryBike: BikeId -> Async<Bike option>)
        (getBalance: UserId -> Async<Balance option>)
        (getInstant: unit -> Instant)
        bookingId
        (data: Data)
        =
        asyncResult {
            let! bookingEventsOfBike = queryBookingEventsOfBike data.BikeId
            let bookings = Booking.projectMultiple bookingEventsOfBike

            let instant = getInstant ()

            do!
                Booking.getStatusOfBike instant bookings
                |> (fun x -> x = Bookable)
                |> Result.requireTrue RentalError.BikeAlreadyBooked

            let! bike =
                queryBike data.BikeId
                |> Async.map (Result.requireSome RentalError.BikeNotFound)

            let! userBalance =
                getBalance data.UserId
                |> Async.map (Result.requireSome RentalError.UserWalletNotFound)

            do!
                userBalance - bike.Price > Balance 0m
                |> Result.requireTrue RentalError.UserBalanceNotSufficient

            do!
                persistBookingEvent
                    { BookingEvent.BookingId = bookingId
                      EventId = Guid.NewGuid()
                      BikeId = data.BikeId
                      Data = Booked data.UserId
                      Instant = instant }
        }

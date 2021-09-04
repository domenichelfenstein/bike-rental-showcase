namespace BikeRental.Rental.Features

open System
open BikeRental.Rental
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module ReleaseBike =
    type Data = { BookingId: BookingId }

    let execute
        (persistBookingEvent: BookingEvent -> Async<unit>)
        (queryBookingEvents: BookingId -> Async<BookingEvent list>)
        (triggerUiChange: unit -> unit)
        (getInstant: unit -> Instant)
        (data: Data)
        =
        asyncResult {
            let! bookingEvents = queryBookingEvents data.BookingId

            let! booking =
                Booking.projectSingle bookingEvents
                |> Result.requireSome RentalError.BookingNotFound

            do!
                booking.End
                |> Result.requireNone RentalError.BikeAlreadyReleased

            do!
                persistBookingEvent
                    { BookingEvent.BookingId = data.BookingId
                      EventId = Guid.NewGuid()
                      BikeId = booking.BikeId
                      Data = Released
                      Instant = getInstant () }

            do triggerUiChange ()
        }

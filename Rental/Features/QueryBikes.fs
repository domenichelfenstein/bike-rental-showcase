namespace BikeRental.Rental.Features

open BikeRental
open BikeRental.Rental
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module QueryBikes =

    type BookableBike =
        { BikeId: BikeId
          Name: string
          Manufacturer: string
          Price: Price
          Status: AvailabilityStatus
          Base64Image: string }

    let query
        (queryAllBikes: unit -> Async<Bike list>)
        (queryAllBookingEvents: unit -> Async<BookingEvent list>)
        (getInstant: unit -> Instant)
        userId
        =
        async {
            let! bikes = queryAllBikes ()
            let! bookingEvents = queryAllBookingEvents ()
            let bookings = Booking.projectMultiple bookingEvents

            let getBookingsOfBike bikeId =
                bookings
                |> List.filter (fun b -> b.BikeId = bikeId)

            return bikes
            |> List.map
                (fun b ->
                    { BookableBike.BikeId = b.BikeId
                      Name = b.Name
                      Manufacturer = b.Manufacturer
                      Price = b.Price
                      Status =
                          b.BikeId
                          |> getBookingsOfBike
                          |> (Booking.getStatusOfBike (getInstant ()) userId)
                      Base64Image = b.Base64Image })
        }

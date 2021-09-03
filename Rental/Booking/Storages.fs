namespace BikeRental.Rental

open FSharpx.Collections

type BookingEventStorage =
    abstract PersistEvent : BookingEvent -> Async<unit>
    abstract GetAllEvents : unit -> Async<BookingEvent list>
    abstract GetEventsOfBike : BikeId -> Async<BookingEvent list>
    abstract GetEventsOfBooking : BookingId -> Async<BookingEvent list>

module BookingEventInMemoryStorage =
    let create () =
        let mutable events: BookingEvent list = []

        { new BookingEventStorage with
            member self.PersistEvent event =
                async { events <- events |> List.append [ event ] }

            member self.GetAllEvents() = async { return events }

            member self.GetEventsOfBike bikeId =
                async { return events |> List.filter (fun e -> e.BikeId = bikeId) }

            member self.GetEventsOfBooking bookingId =
                async {
                    return
                        events
                        |> List.filter (fun e -> e.BookingId = bookingId)
                } }

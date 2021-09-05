namespace BikeRental.Rental

open BikeRental.Rental.Features

type RentalFacade(services: RentalServices, storages: RentalStorages, bikesUiChanged: obj -> unit) =
    let bikesUiChangedFromRental () = bikesUiChanged {| Sender = "rental" |}

    member self.GetAllBookableBikes =
        QueryBikes.query storages.Bikes.GetAll storages.BookingEvents.GetAllEvents services.GetInstant

    member self.RentBike =
        RentBike.execute
            storages.BookingEvents.PersistEvent
            storages.BookingEvents.GetEventsOfBike
            storages.Bikes.GetBike
            services.WithdrawAmount
            bikesUiChangedFromRental
            services.GetInstant

    member self.ReleaseBike =
        ReleaseBike.execute
            storages.BookingEvents.PersistEvent
            storages.BookingEvents.GetEventsOfBooking
            bikesUiChangedFromRental
            services.GetInstant

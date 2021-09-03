namespace BikeRental.Rental

open BikeRental.Rental.Features

type RentalFacade(services: RentalServices, storages: RentalStorages) =
    let getInstant = services.GetNodaInstant >> Instant

    member self.GetAllBookableBikes =
        QueryBikes.query storages.Bikes.GetAll storages.BookingEvents.GetAllEvents getInstant

    member self.RentBike =
        RentBike.execute
            storages.BookingEvents.PersistEvent
            storages.BookingEvents.GetEventsOfBike
            storages.Bikes.GetBike
            services.GetUserBalance
            getInstant

    member self.ReleaseBike =
        ReleaseBike.execute storages.BookingEvents.PersistEvent storages.BookingEvents.GetEventsOfBooking getInstant

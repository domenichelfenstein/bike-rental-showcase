namespace BikeRental.Rental

type RentalFacade(services: RentalServices, storages: RentalStorages) =
    let getInstant = services.GetNodaInstant >> Instant

    member self.Test () = ()

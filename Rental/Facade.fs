namespace BikeRental.Rental

type RentalFacade(_services: RentalServices, storages: RentalStorages) =
    member self.GetAllBikes = storages.Bikes.GetAll

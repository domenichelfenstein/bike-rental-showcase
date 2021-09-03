namespace BikeRental.Rental

type RentalError =
    | BikeNotFound
    | BookingNotFound
    | BikeAlreadyReleased
    | BikeAlreadyBooked
    | UserWalletNotFound
    | UserBalanceNotSufficient

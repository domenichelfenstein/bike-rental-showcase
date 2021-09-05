namespace BikeRental.Rental

type Price = Price of decimal
type Amount = Amount of decimal

type Balance =
    | Balance of decimal
    static member (-) (Balance balance, Price price) = Balance (balance - price)

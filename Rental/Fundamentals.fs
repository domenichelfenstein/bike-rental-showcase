namespace BikeRental.Rental

[<NoComparison>]
type Instant = Instant of NodaTime.Instant

type Price = Price of decimal

type Balance =
    | Balance of decimal
    static member (-) (Balance balance, Price price) = Balance (balance - price)

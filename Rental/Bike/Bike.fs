namespace BikeRental.Rental

open System
open BikeRental

type Price = Price of decimal
type BikeId = BikeId of Guid

module Price =
    let toAmount (Price price) = Amount price

type Bike =
    { BikeId: BikeId
      Name: string
      Manufacturer: string
      Price: Price
      Base64Image: string }


namespace BikeRental.Rental

open System

type BikeId = BikeId of Guid
type Price = Price of decimal

type Bike =
    { BikeId: BikeId
      Name: string
      Manufacturer: string
      Price: Price
      Base64Image: string }


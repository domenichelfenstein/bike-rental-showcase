namespace BikeRental.Rental

open FSharpx.Collections
open FSharp.Data

type BikeStorage =
    abstract GetAll : unit -> Async<Bike list>

module BikeJsonStorage =
    let samplePath = "https://api.github.com/repos/fsharp/FSharp.Data/issues"
    type JsonBike = JsonProvider<Sample="./sample.json">

    type JsonContext = { FilePath: string }

    let create (ctx: JsonContext) =
        { new BikeStorage with
            member self.GetAll() = async {
                let! jsonBikes = JsonBike.AsyncLoad ctx.FilePath
                return jsonBikes
                    |> Array.map (fun bike -> { Bike.Name = bike.Name
                                                BikeId = BikeId bike.Id
                                                Manufacturer = bike.Manufacturer
                                                Price = Price bike.Price
                                                Base64Image = bike.Image })
                    |> Array.toList
            } }

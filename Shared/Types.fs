namespace BikeRental

[<CustomEquality; CustomComparison>]
type Instant =
    | Instant of NodaTime.Instant
    member a.innerCompareTo (b: obj) =
        let nodaA = a |> (fun (Instant x) -> x)
        let instantB =
            match b with
            | :? Instant as instant -> instant
            | _ -> Instant NodaTime.Instant.MinValue

        let nodaB = instantB |> (fun (Instant x) -> x)

        match nodaA.ToUnixTimeTicks() - nodaB.ToUnixTimeTicks() with
        | x when x < 0L -> -1
        | x when x = 0L -> 0
        | x when x > 0L -> 1
        | _ -> failwith "don't know how this could ever happen"

    interface System.IComparable with
        override a.CompareTo(b) = a.innerCompareTo b
    override a.Equals(b) = a.innerCompareTo b = 0
    override x.GetHashCode() =
        let nodaX = x |> (fun (Instant x) -> x)
        nodaX.GetHashCode()

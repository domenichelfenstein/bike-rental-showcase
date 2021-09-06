namespace BikeRental

open System

[<CustomEquality; CustomComparison>]
type Instant =
    | Instant of NodaTime.Instant
    member a.innerCompareTo(b: obj) =
        let (Instant nodaA) = a

        let nodaB =
            match b with
            | :? Instant as instant ->
                let (Instant noda) = instant
                noda
            | _ -> NodaTime.Instant.MinValue

        match nodaA.ToUnixTimeTicks() - nodaB.ToUnixTimeTicks() with
        | x when x < 0L -> -1
        | x when x = 0L -> 0
        | x when x > 0L -> 1
        | _ -> failwith "don't know how this could ever happen"

    interface System.IComparable with
        override a.CompareTo(b) = a.innerCompareTo b

    override a.Equals(b) = a.innerCompareTo b = 0

    override x.GetHashCode() =
        let (Instant nodaX) = x
        nodaX.GetHashCode()

type Amount = Amount of decimal

type Balance =
    | Balance of decimal
    static member (-)(Balance balance, Amount amount) = Balance(balance - amount)
    static member (+)(Balance balance, Amount amount) = Balance(balance + amount)

type UserId = UserId of Guid

namespace Testing

open FsCheck

[<RequireQualifiedAccess>]
module Example =
    type NodaGenerator =
        static member Instant() =
            Arb.generate<System.DateTime>
            |> Gen.map (fun dt -> dt.ToUniversalTime())
            |> Gen.map NodaTime.Instant.FromDateTimeUtc
            |> Arb.fromGen

    let createWithSeed<'a> seedNr =
        do Arb.register<NodaGenerator> () |> ignore
        let gen = Arb.generate<'a>
        let seed = Random.StdGen(seedNr, seedNr)
        Gen.eval 1 seed gen

    let create<'a> () =
        let randomNr = System.Random().Next()
        createWithSeed<'a> randomNr

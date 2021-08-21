namespace BikeRental.Registration

open FSharpx.Collections

type OpenVerificationStorage =
    abstract Add : OpenVerification -> Async<unit>
    abstract Remove : Username -> Async<unit>
    abstract Query : Username -> Async<OpenVerification option>

module OpenVerificationInMemoryStorage =
    let create () =
        let mutable verifications: OpenVerification list = []

        { new OpenVerificationStorage with
            member self.Add v =
                async { verifications <- verifications |> List.append [ v ] }

            member self.Remove username =
                async {
                    verifications <-
                        verifications
                        |> List.filter (fun v -> v.Username <> username)
                }

            member self.Query username =
                async {
                    return
                        verifications
                        |> List.tryFind (fun x -> x.Username = username)
                } }

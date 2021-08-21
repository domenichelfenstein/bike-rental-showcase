namespace BikeRental.Registration

open FSharpx.Collections

type UserEventStorage =
    abstract PersistEvent : UserEvent -> Async<unit>
    abstract QueryByUsername : Username -> Async<UserEvent list>

module UserEventInMemoryStorage =
    let create () =
        let mutable events: UserEvent list = []

        { new UserEventStorage with
            member self.PersistEvent event =
                async { events <- events |> List.append [ event ] }

            member self.QueryByUsername username =
                async {
                    return
                        events
                        |> List.filter (fun e -> e.Username = username)
                } }

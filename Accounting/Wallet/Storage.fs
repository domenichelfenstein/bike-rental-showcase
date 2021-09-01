namespace BikeRental.Accounting

type WalletEventStorage =
    abstract PersistEvent : WalletEvent -> Async<unit>
    abstract QueryByUserId : UserId -> Async<WalletEvent list>

module WalletEventInMemoryStorage =
    let create () =
        let mutable events: WalletEvent list = []

        { new WalletEventStorage with
            member self.PersistEvent event =
                async { events <- events |> List.append [ event ] }

            member self.QueryByUserId userId =
                async { return events |> List.filter (fun e -> e.UserId = userId) } }

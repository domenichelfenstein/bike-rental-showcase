namespace BikeRental.Accounting

open BikeRental

type WalletEventStorage =
    abstract PersistEvent : WalletEvent -> Async<unit>
    abstract GetWalletEventsByUserId : UserId -> Async<WalletEvent list>
    abstract GetWalletEvents : WalletId -> Async<WalletEvent list>

module WalletEventInMemoryStorage =
    let create () =
        let mutable events: WalletEvent list = []

        { new WalletEventStorage with
            member self.PersistEvent event =
                async { events <- events |> List.append [ event ] }

            member self.GetWalletEventsByUserId userId =
                async { return events |> List.filter (fun e -> e.UserId = userId) }

            member self.GetWalletEvents walletId =
                async { return events |> List.filter (fun e -> e.WalletId = walletId) } }

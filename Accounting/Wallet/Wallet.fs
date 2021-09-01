namespace BikeRental.Accounting

type Balance = Balance of decimal

type Wallet =
    { WalletId: WalletId
      UserId: UserId
      Balance: Balance }

module Wallet =
    let project (events: WalletEvent list) =
        events
        |> List.sortBy
            (fun x ->
                let (Instant instant) = x.Instant
                instant.ToUnixTimeTicks())
        |> List.fold
            (fun oldState event ->
                match oldState, event.Data with
                | _, Created _ ->
                    Some
                        { Wallet.WalletId = event.WalletId
                          UserId = event.UserId
                          Balance = Balance 0m }
                | Some old, Withdrawn (Amount amount) ->
                    let oldBalance = old.Balance |> (fun (Balance b) -> b)

                    Some
                        { old with
                              Balance = Balance(oldBalance - amount) }
                | Some old, Deposited (Amount amount) ->
                    let oldBalance = old.Balance |> (fun (Balance b) -> b)

                    Some
                        { old with
                              Balance = Balance(oldBalance + amount) }
                | old, _ -> old)
            None

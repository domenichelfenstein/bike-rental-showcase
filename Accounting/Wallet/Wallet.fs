namespace BikeRental.Accounting

open BikeRental

type Wallet =
    { WalletId: WalletId
      UserId: UserId
      Balance: Balance }

module Wallet =
    let project (events: WalletEvent list) =
        events
        |> List.sortBy (fun x -> x.Instant)
        |> List.fold
            (fun oldState event ->
                match oldState, event.Data with
                | _, Created _ ->
                    Some
                        { Wallet.WalletId = event.WalletId
                          UserId = event.UserId
                          Balance = Balance 0m }
                | Some old, Withdrawn amount ->
                    Some
                        { old with
                              Balance = old.Balance - amount }
                | Some old, Deposited amount ->
                    Some
                        { old with
                              Balance = old.Balance + amount }
                | old, _ -> old)
            None

namespace BikeRental.Accounting.Features

open BikeRental.Accounting
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module QueryWallet =
    let query (getEventsByUserId: UserId -> Async<WalletEvent list>) userId =
        asyncResult {
            let! events = getEventsByUserId userId

            return!
                Wallet.project events
                |> Result.requireSome AccountingError.WalletNotFound
        }

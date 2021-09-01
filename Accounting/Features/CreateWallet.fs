namespace BikeRental.Accounting.Features

open System
open BikeRental.Accounting
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module CreateWallet =
    type Data = { UserId : UserId }

    let execute
        (persistWalletEvent: WalletEvent -> Async<unit>)
        (getInstant : unit -> Instant)
        walletId
        (data: Data)
        =
        asyncResult {
            do!
                persistWalletEvent { WalletEvent.WalletId = walletId
                                     UserId = data.UserId
                                     EventId = Guid.NewGuid()
                                     Data = WalletEventData.Created
                                     Instant = getInstant() }
        }

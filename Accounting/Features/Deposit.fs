namespace BikeRental.Accounting.Features

open System
open BikeRental
open BikeRental.Accounting
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module Deposit =
    type Data = { UserId: UserId; Amount: Amount }

    let execute
        (getEventsByUserId: UserId -> Async<WalletEvent list>)
        (persistWalletEvent: WalletEvent -> Async<unit>)
        (getInstant: unit -> Instant)
        (triggerUIChange: WalletId -> unit)
        (data: Data)
        =
        asyncResult {
            let! events = getEventsByUserId data.UserId

            let! wallet =
                Wallet.project events
                |> Result.requireSome AccountingError.WalletNotFound

            do!
                persistWalletEvent
                    { WalletEvent.WalletId = wallet.WalletId
                      UserId = data.UserId
                      EventId = Guid.NewGuid()
                      Data = WalletEventData.Deposited data.Amount
                      Instant = getInstant () }

            triggerUIChange wallet.WalletId
        }

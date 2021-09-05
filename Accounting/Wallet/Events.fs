namespace BikeRental.Accounting

open System
open BikeRental

type WalletId = WalletId of Guid

type WalletEventData =
    | Created
    | Withdrawn of Amount
    | Deposited of Amount

type WalletEvent =
    { WalletId: WalletId
      UserId: UserId
      EventId: Guid
      Data: WalletEventData
      Instant: Instant }

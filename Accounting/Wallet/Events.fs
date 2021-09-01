namespace BikeRental.Accounting

open System

type WalletId = WalletId of Guid
type UserId = UserId of Guid
type Amount = Amount of decimal

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

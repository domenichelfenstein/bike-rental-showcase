namespace BikeRental.Accounting

open System

type AccountingId = AccountingId of Guid

type AccountingError =
    | WalletNotFound

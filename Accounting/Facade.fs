namespace BikeRental.Accounting

open BikeRental.Accounting.Features

type AccountingFacade(services: AccountingServices, storages: AccountingStorages, uiChanged: obj -> WalletId -> unit) =
    let getInstant = services.GetNodaInstant >> Instant

    let uiWalletChanged = uiChanged {| Sender = "accounting" |}

    member self.CreateWallet =
        CreateWallet.execute storages.WalletEvents.PersistEvent getInstant

    member self.Deposit =
        Deposit.execute
            storages.WalletEvents.GetWalletEventsByUserId
            storages.WalletEvents.PersistEvent
            getInstant
            uiWalletChanged

    member self.Withdraw =
        Withdraw.execute
            storages.WalletEvents.GetWalletEventsByUserId
            storages.WalletEvents.PersistEvent
            getInstant
            uiWalletChanged

    member self.GetWalletOfUser = QueryWallet.queryByUser storages.WalletEvents.GetWalletEventsByUserId

    member self.GetWallet = QueryWallet.queryByWalletId storages.WalletEvents.GetWalletEvents

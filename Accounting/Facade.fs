namespace BikeRental.Accounting

open BikeRental.Accounting.Features

type AccountingFacade(services: AccountingServices, storages: AccountingStorages, uiChanged: obj -> WalletId -> unit) =
    let uiWalletChanged = uiChanged {| Sender = "accounting" |}

    member self.CreateWallet =
        CreateWallet.execute storages.WalletEvents.PersistEvent services.GetInstant

    member self.Deposit =
        Deposit.execute
            storages.WalletEvents.GetWalletEventsByUserId
            storages.WalletEvents.PersistEvent
            services.GetInstant
            uiWalletChanged

    member self.Withdraw =
        Withdraw.execute
            storages.WalletEvents.GetWalletEventsByUserId
            storages.WalletEvents.PersistEvent
            services.GetInstant
            uiWalletChanged

    member self.GetWalletOfUser = QueryWallet.queryByUser storages.WalletEvents.GetWalletEventsByUserId

    member self.GetWallet = QueryWallet.queryByWalletId storages.WalletEvents.GetWalletEvents

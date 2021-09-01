namespace BikeRental.Accounting

open BikeRental.Accounting.Features

type AccountingFacade(services: AccountingServices, storages: AccountingStorages, uiChanged: Event<string>) =
    let getInstant = services.GetNodaInstant >> Instant

    member self.CreateWallet =
        CreateWallet.execute storages.WalletEvents.PersistEvent getInstant

    member self.GetWallet = QueryWallet.query storages.WalletEvents.QueryByUserId

    member self.TriggerUIChange = uiChanged.Trigger

    member self.UIChanged = uiChanged.Publish

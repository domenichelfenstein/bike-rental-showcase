namespace BikeRental.Starter

open System
open BikeRental.Accounting
open BikeRental.Accounting.Features
open BikeRental.Registration
open BikeRental.Rental
open Microsoft.Extensions.Configuration

type Facades =
    { Registration: RegistrationFacade
      Accounting: AccountingFacade
      Rental: RentalFacade }

module Adapters =
    let createWallet (facade: AccountingFacade) (BikeRental.Registration.UserId userId) =
        facade.CreateWallet
            (Guid.NewGuid() |> WalletId)
            { CreateWallet.Data.UserId = BikeRental.Accounting.UserId userId }

module FacadesCreator =

    let create (_configuration: IConfiguration) =
        let uiChangedEvent = Event<Guid * string>()

        let accountingServices =
            { AccountingServices.GetNodaInstant = Services.getNodaInstant }

        let accountingChanged msg (BikeRental.Accounting.UserId userId) = uiChangedEvent.Trigger(userId, msg)
        let accountingFacade =
            AccountingFacade(accountingServices, (AccountingStorageCreator.create AccountingStorageContext.InMemory), accountingChanged)

        let registrationServices =
            { RegistrationServices.GenerateVerificationCode = Fakes.generateVerificationCode
              GetNodaInstant = Services.getNodaInstant
              SendVerificationCode = Fakes.sendVerificationCode
              GetPasswordHash = Fakes.hashPassword
              CreateAuthToken = Fakes.createAuthToken
              CreateWallet = Adapters.createWallet accountingFacade }

        let registrationFacade =
            RegistrationFacade(
                registrationServices,
                (RegistrationStorageCreator.create RegistrationStorageContext.InMemory)
            )

        let rentalServices = { RentalServices.GetNodaInstant = Services.getNodaInstant }

        let rentalJsonContext = {  BikeJsonStorage.JsonContext.FilePath = "./bikes.json" }
        let rentalFacade =
            RentalFacade(rentalServices, (RentalStorageCreator.create (RentalStorageContext.Json rentalJsonContext)))

        uiChangedEvent, { Registration = registrationFacade ; Accounting = accountingFacade ; Rental = rentalFacade }

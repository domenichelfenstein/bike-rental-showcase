namespace BikeRental.Starter

open System
open BikeRental.Accounting
open BikeRental.Accounting.Features
open BikeRental.Registration
open Microsoft.Extensions.Configuration

type Facades =
    { Registration: RegistrationFacade
      Accounting: AccountingFacade }

module Adapters =
    let createWallet (facade: AccountingFacade) (BikeRental.Registration.UserId userId) =
        facade.CreateWallet
            (Guid.NewGuid() |> WalletId)
            { CreateWallet.Data.UserId = BikeRental.Accounting.UserId userId }

module FacadesCreator =
    let create (_configuration: IConfiguration) =
        let accountingServices =
            { AccountingServices.GetNodaInstant = Services.getNodaInstant }

        let accountingFacade =
            AccountingFacade(accountingServices, (AccountingStorageCreator.create AccountingStorageContext.InMemory))

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

        { Registration = registrationFacade
          Accounting = accountingFacade }

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

    let withdrawFromUserBalance
        (facade: AccountingFacade)
        (BikeRental.Rental.Amount amount)
        (BikeRental.Rental.UserId userId)
        =
        async {
            let accountingUserId = BikeRental.Accounting.UserId userId
            let accountingAmount = BikeRental.Accounting.Amount amount

            let! result =
                facade.Withdraw
                    { Withdraw.Data.UserId = accountingUserId
                      Amount = accountingAmount }

            return
                match result with
                | Ok _ -> true
                | _ -> false
        }

module FacadesCreator =

    let create (_configuration: IConfiguration) =
        let uiChangedEvent = Event<string * string>()

        let accountingServices =
            { AccountingServices.GetNodaInstant = Services.getNodaInstant }

        let accountingChanged msg (BikeRental.Accounting.UserId userId) =
            uiChangedEvent.Trigger(userId.ToString(), msg)

        let accountingFacade =
            AccountingFacade(
                accountingServices,
                (AccountingStorageCreator.create AccountingStorageContext.InMemory),
                accountingChanged
            )

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

        let rentalServices =
            { RentalServices.GetNodaInstant = Services.getNodaInstant
              WithdrawAmount = Adapters.withdrawFromUserBalance accountingFacade }

        let rentalJsonContext = { BikeJsonStorage.JsonContext.FilePath = "./bikes.json" }

        let bikesUiChanged sender = uiChangedEvent.Trigger("bikes", sender)

        let rentalFacade =
            RentalFacade(
                rentalServices,
                (RentalStorageCreator.create (RentalStorageContext.Mixed rentalJsonContext)),
                bikesUiChanged
            )

        uiChangedEvent,
        { Registration = registrationFacade
          Accounting = accountingFacade
          Rental = rentalFacade }

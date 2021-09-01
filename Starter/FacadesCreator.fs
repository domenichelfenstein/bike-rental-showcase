namespace BikeRental.Starter

open BikeRental.Accounting
open BikeRental.Registration
open Microsoft.Extensions.Configuration

type Facades = { Registration: RegistrationFacade ; Accounting: AccountingFacade }

module FacadesCreator =
    let create (_configuration: IConfiguration) =
        let registrationServices =
            { RegistrationServices.GenerateVerificationCode = Fakes.generateVerificationCode
              GetNodaInstant = Services.getNodaInstant
              SendVerificationCode = Fakes.sendVerificationCode
              GetPasswordHash = Fakes.hashPassword
              CreateAuthToken = Fakes.createAuthToken }

        let registrationFacade =
            RegistrationFacade(
                registrationServices,
                (RegistrationStorageCreator.create RegistrationStorageContext.InMemory)
            )

        let accountingServices =
            { AccountingServices.GetNodaInstant = Services.getNodaInstant }

        let accountingFacade =
            AccountingFacade(
                accountingServices,
                (AccountingStorageCreator.create AccountingStorageContext.InMemory))

        { Registration = registrationFacade ; Accounting = accountingFacade }

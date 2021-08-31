namespace BikeRental.Starter

open BikeRental.Registration
open Microsoft.Extensions.Configuration

type Facades = { Registration: RegistrationFacade }

module FacadesCreator =
    let create (_configuration: IConfiguration) =
        let registrationServices =
            { RegistrationServices.GenerateVerificationCode = Fakes.generateVerificationCode
              GetNodaInstant = Services.getNodaInstant
              SendVerificationCode = Fakes.sendVerificationCode
              GetPasswordHash = Fakes.hashPassword }

        let registrationFacade =
            RegistrationFacade(
                registrationServices,
                (RegistrationStorageCreator.create RegistrationStorageContext.InMemory)
            )

        { Registration = registrationFacade }

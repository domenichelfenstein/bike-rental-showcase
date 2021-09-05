namespace BikeRental.Registration

open BikeRental.Registration.Features

type RegistrationFacade(services: RegistrationServices, storages: RegistrationStorages) =
    member self.StartRegistration =
        StartRegistration.execute
            (User.getUser storages.UserEvents)
            storages.UserEvents.PersistEvent
            storages.OpenVerifications.Add
            services.GenerateVerificationCode
            services.SendVerificationCode
            services.GetInstant

    member self.VerifyPhone =
        VerifyPhone.execute
            (User.getUser storages.UserEvents)
            storages.OpenVerifications.Query
            storages.OpenVerifications.Remove

    member self.CompleteRegistration =
        CompleteRegistration.execute
            (User.getUser storages.UserEvents)
            storages.UserEvents.PersistEvent
            services.CreateWallet
            services.GetPasswordHash
            services.GetInstant

    member self.CreateToken =
        CreateToken.execute
            (User.getUser storages.UserEvents)
            services.GetPasswordHash
            services.CreateAuthToken

    member self.GetInitials =
        QueryInitials.query
            storages.UserEvents.QueryByUsername

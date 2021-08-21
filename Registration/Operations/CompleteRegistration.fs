namespace BikeRental.Registration.Operations

open BikeRental.Registration

[<RequireQualifiedAccess>]
module CompleteRegistration =
    type Data =
        { CompletionId: RegistrationCompletionId
          FirstName: string
          LastName: string }

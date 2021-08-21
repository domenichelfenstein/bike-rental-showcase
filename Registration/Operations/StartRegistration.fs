namespace BikeRental.Registration.Operations

open BikeRental.Registration
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module StartRegistration =
    type Data =
        { Username: Username
          PhoneNumber: PhoneNumber }

    let execute (queryUser: Username -> UserState) data = asyncResult { () }

namespace BikeRental.Registration.Operations

open BikeRental.Registration

[<RequireQualifiedAccess>]
module VerifyPhone =
    type Data =
        { Username: Username
          VerificationCode: VerificationCode }

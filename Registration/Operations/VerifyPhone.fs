namespace BikeRental.Registration.Operations

open BikeRental.Registration
open FSharpx
open FsToolkit.ErrorHandling
open FsToolkit.ErrorHandling.Operator.Result

[<RequireQualifiedAccess>]
module VerifyPhone =
    type Data =
        { Username: Username
          VerificationCode: VerificationCode }

    let execute
        (queryUser: Username -> Async<UserState>)
        (queryOpenVerification: Username -> Async<OpenVerification option>)
        (removeOpenVerification: Username -> Async<unit>)
        (data: Data)
        =
        asyncResult {
            let! userState = queryUser data.Username

            let! user =
                match userState with
                | InVerification user -> Ok(user)
                | _ -> Error RegistrationError.UserNotInVerificationProcess

            let! openVerification =
                user.Username
                |> queryOpenVerification
                |> Async.map (Result.requireSome RegistrationError.UserNotInVerificationProcess)

            do!
                (openVerification.VerificationCode = data.VerificationCode)
                |> Result.requireTrue RegistrationError.WrongVerificationCode

            do! removeOpenVerification user.Username

            return user.CompletionId
        }

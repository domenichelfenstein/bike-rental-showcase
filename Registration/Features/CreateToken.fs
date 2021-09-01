namespace BikeRental.Registration.Features

open BikeRental.Registration
open FSharpx
open FsToolkit.ErrorHandling

type AuthToken = AuthToken of string

[<RequireQualifiedAccess>]
module CreateToken =
    type Data =
        {
            Username : Username
            Password : string
        }

    let execute
        (queryUser: Username -> Async<UserState>)
        (hash: string -> PasswordHash)
        (createToken: User -> AuthToken)
        (data: Data)
        =
        asyncResult {
            let! userState = queryUser data.Username

            let! user =
                match userState with
                | Active user -> Ok(user)
                | _ -> Error RegistrationError.UserNotFound

            let passwordHash = hash data.Password

            do!
                (user.PasswordHash = passwordHash)
                |> Result.requireTrue RegistrationError.WrongPassword

            return createToken user
        }

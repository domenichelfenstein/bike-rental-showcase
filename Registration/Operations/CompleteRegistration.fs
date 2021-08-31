namespace BikeRental.Registration.Operations

open BikeRental.Registration
open FSharpx
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module CompleteRegistration =
    type Data =
        { Username: Username
          CompletionId: RegistrationCompletionId
          FirstName: string
          LastName: string
          Password: string }

    let execute
        (queryUser: Username -> Async<UserState>)
        (persistUserEvent: UserEvent -> Async<unit>)
        (hash: string -> PasswordHash)
        (getInstant: unit -> Instant)
        (data: Data)
        =
        asyncResult {
            let! userState = queryUser data.Username

            let! user =
                match userState with
                | InVerification user -> Ok(user)
                | _ -> Error RegistrationError.UserNotFound

            do!
                (user.CompletionId = data.CompletionId)
                |> Result.requireTrue RegistrationError.WrongCompletionId

            let passwordHash = hash data.Password

            do!
                persistUserEvent
                    { UserEvent.UserId = user.UserId
                      Username = user.Username
                      Data =
                          UserEventData.RegistrationCompleted
                              { RegistrationCompletedData.FirstName = data.FirstName
                                LastName = data.LastName
                                PasswordHash = passwordHash }
                      Instant = getInstant () }
        }

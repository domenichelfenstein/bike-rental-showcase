namespace BikeRental.Registration.Features

open System
open BikeRental
open BikeRental.Registration
open FsToolkit.ErrorHandling

[<RequireQualifiedAccess>]
module StartRegistration =
    type Data =
        { Username: Username
          PhoneNumber: PhoneNumber }

    let execute
        (queryUser: Username -> Async<UserState>)
        (persistUserEvent: UserEvent -> Async<unit>)
        (addOpenVerification: OpenVerification -> Async<unit>)
        (generateVerificationCode: GenerateVerificationCode)
        (sendVerificationCode: SendVerificationCode)
        (getInstant : unit -> Instant)
        userId
        completionId
        (data: Data)
        =
        asyncResult {
            let! userState = queryUser data.Username

            do!
                match userState with
                | NotExisting -> Ok()
                | _ -> Error RegistrationError.UsernameAlreadyTaken

            do!
                persistUserEvent
                    { UserEvent.UserId = userId
                      EventId = Guid.NewGuid()
                      Username = data.Username
                      Data =
                          UserEventData.RegistrationStarted
                              { RegistrationStartedData.CompletionId = completionId
                                PhoneNumber = data.PhoneNumber }
                      Instant = getInstant () }

            let verificationCode = generateVerificationCode ()

            do!
                addOpenVerification
                    { OpenVerification.Username = data.Username
                      VerificationCode = verificationCode }

            do! sendVerificationCode verificationCode data.PhoneNumber
        }

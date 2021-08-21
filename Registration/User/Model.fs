namespace BikeRental.Registration

open System

type UserId = UserId of Guid
type Username = Username of string
type PhoneNumber = PhoneNumber of string
type PasswordHash = PasswordHash of string
type RegistrationCompletionId = RegistrationCompletionId of Guid

type User =
    { UserId: UserId
      Username: Username
      PasswordHash: PasswordHash
      PhoneNumber: PhoneNumber
      FirstName: string
      LastName: string }

type VerifyingUser =
    { UserId: UserId
      Username: Username
      PhoneNumber: PhoneNumber
      CompletionId: RegistrationCompletionId }

type UserState =
    | NotExisting
    | InVerification of VerifyingUser
    | Active of User
    | Deactivated of User

type RegistrationStartedData =
    { PhoneNumber: PhoneNumber
      CompletionId: RegistrationCompletionId }

type RegistrationCompletedData =
    { PasswordHash: PasswordHash
      FirstName: string
      LastName: string }

type UserEventData =
    | RegistrationStarted of RegistrationStartedData
    | RegistrationCompleted of RegistrationCompletedData
    | UserDeactivated

type UserEvent =
    { UserId: UserId
      Username: Username
      Data: UserEventData
      Instant: Instant }

module User =
    let getState (events: UserEvent list) =
        events
        |> List.sortBy (fun x -> x.Instant)
        |> List.fold
            (fun oldState event ->
                match oldState, event.Data with
                | _, RegistrationStarted eventData ->
                    UserState.InVerification
                        { VerifyingUser.UserId = event.UserId
                          Username = event.Username
                          PhoneNumber = eventData.PhoneNumber
                          CompletionId = eventData.CompletionId }
                | InVerification old, RegistrationCompleted eventData ->
                    UserState.Active
                        { User.UserId = event.UserId
                          Username = event.Username
                          PasswordHash = eventData.PasswordHash
                          PhoneNumber = old.PhoneNumber
                          FirstName = eventData.FirstName
                          LastName = eventData.LastName }
                | Active user, UserDeactivated -> UserState.Deactivated user
                | old, _ -> old)
            UserState.NotExisting

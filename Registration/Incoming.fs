namespace BikeRental.Registration

open BikeRental
open BikeRental.Registration.Features

type RegistrationStorages =
    { UserEvents: UserEventStorage
      OpenVerifications: OpenVerificationStorage }

type SqlContext = { ConnectionString: string }

type RegistrationStorageContext =
    | InMemory
    | Sql of SqlContext

module RegistrationStorageCreator =
    let create (ctx: RegistrationStorageContext) =
        match ctx with
        | InMemory ->
            { UserEvents = UserEventInMemoryStorage.create ()
              OpenVerifications = OpenVerificationInMemoryStorage.create () }
        | _ -> failwith "not implemented"

type RegistrationServices = {
    GetInstant : unit -> Instant
    SendVerificationCode : SendVerificationCode
    GenerateVerificationCode : GenerateVerificationCode
    GetPasswordHash : string -> PasswordHash
    CreateAuthToken : User -> AuthToken
    CreateWallet : UserId -> Async<Result<unit, obj>>
}

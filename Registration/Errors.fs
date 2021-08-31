namespace BikeRental.Registration

type RegistrationError =
    | UsernameAlreadyTaken
    | UserNotInVerificationProcess
    | WrongVerificationCode
    | WrongCompletionId
    | UserNotFound

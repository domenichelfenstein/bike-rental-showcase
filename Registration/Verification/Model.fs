namespace BikeRental.Registration

type VerificationCode = VerificationCode of string

type OpenVerification = {
    Username : Username
    VerificationCode : VerificationCode
}

type GenerateVerificationCode = Unit -> VerificationCode
type SendVerificationCode = VerificationCode -> PhoneNumber -> Async<unit>
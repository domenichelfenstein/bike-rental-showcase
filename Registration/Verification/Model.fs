namespace BikeRental.Registration

type VerificationCode = VerificationCode of string

type OpenVerification = {
    Username : Username
    VerificationCode : VerificationCode
}
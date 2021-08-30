namespace BikeRental.Starter

open BikeRental.Registration

module Fakes =
    let generateVerificationCode () = VerificationCode "Xyz"

    let sendVerificationCode (VerificationCode code) (PhoneNumber phone) =
        async { printfn $"Verification Code for {phone}: {code}" }

namespace BikeRental.Starter

open System.Security.Cryptography
open System.Text
open BikeRental
open BikeRental.Registration
open BikeRental.Registration.Features

// It goes without saying that nothing in this file must ever be used in production!!!
module Fakes =
    let generateVerificationCode () = VerificationCode "Xyz"

    let sendVerificationCode (VerificationCode code) (PhoneNumber phone) =
        async { printfn $"Verification Code for {phone}: {code}" }

    let hashPassword (input: string) =
        let fromBytes (data: byte array) =
            use md5 = MD5.Create()

            (StringBuilder(), md5.ComputeHash(data))
            ||> Array.fold (fun sb b -> sb.Append(b.ToString("x2")))
            |> string

        let result = fromBytes (Encoding.UTF8.GetBytes(input))
        PasswordHash result

    let createAuthToken (user: User) =
        let (Username username) = user.Username
        let (UserId userId) = user.UserId
        let result = $"thisIsAFakeToken+{username}+{userId.ToString()}"

        AuthToken result

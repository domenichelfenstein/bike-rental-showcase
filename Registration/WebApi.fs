﻿namespace BikeRental.Registration

open System
open BikeRental
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Mvc
open FsToolkit.ErrorHandling
open BikeRental.Registration

[<ApiController>]
[<AllowAnonymous>]
[<Route("api/registration")>]
type RegistrationApiController(facade: RegistrationFacade) =
    inherit ControllerBase()

    [<HttpPost>]
    [<Route("start")>]
    member self.Start([<FromBody>] data) =
        asyncResult {
            do! facade.StartRegistration(Guid.NewGuid() |> UserId) (Guid.NewGuid() |> RegistrationCompletionId) data }

    [<HttpPost>]
    [<Route("verify")>]
    member self.Verify([<FromBody>] data) =
        asyncResult { return! facade.VerifyPhone data }

    [<HttpPost>]
    [<Route("complete")>]
    member self.Start([<FromBody>] data) =
        asyncResult { do! facade.CompleteRegistration data }

    [<HttpPost>]
    [<Route("login")>]
    member self.Start([<FromBody>] data) =
        asyncResult { return! facade.CreateToken data }

    [<HttpGet>]
    [<Route("initials/{username}")>]
    member self.GetUser([<FromRoute>] username) = facade.GetInitials(Username username)

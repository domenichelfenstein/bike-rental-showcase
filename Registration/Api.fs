namespace BikeRental.Registration

open System
open Microsoft.AspNetCore.Mvc
open FsToolkit.ErrorHandling
open BikeRental.Registration

[<ApiController>]
[<Route("registration")>]
type RegistrationApiController(facade : RegistrationFacade) =
    inherit ControllerBase()
    [<HttpPost>]
    [<Route("start")>]
    member self.Start([<FromBody>] data) =
        asyncResult {
            do! facade.StartRegistration
                    (Guid.NewGuid () |> UserId)
                    (Guid.NewGuid () |> RegistrationCompletionId)
                    data
        }

    [<HttpGet>]
    [<Route("test")>]
    member self.Test() =
        async {
            return! facade.Hack.QueryByUsername (Username "")
        }

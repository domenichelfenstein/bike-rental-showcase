namespace BikeRental.Accounting

open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Authorize(AuthenticationSchemes = "FakeAuthenticationScheme")>]
[<Route("accounting")>]
type AccountingController() =
    inherit ControllerBase()

    [<HttpGet>]
    [<Route("test")>]
    member self.Get() =
        [| 1 ; 2 ; 3 |]

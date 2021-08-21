namespace BikeRental.Accounting

open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("accounting")>]
type AccountingController() =
    inherit ControllerBase()

    [<HttpGet>]
    [<Route("test")>]
    member self.Get() =
        [| 1 ; 2 ; 3 |]

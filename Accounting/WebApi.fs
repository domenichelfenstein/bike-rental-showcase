namespace BikeRental.Accounting

open System
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Authorize(AuthenticationSchemes = "FakeAuthenticationScheme")>]
[<Route("accounting")>]
type AccountingController(facade: AccountingFacade) =
    inherit ControllerBase()

    [<HttpGet>]
    [<Route("wallet/{userId}")>]
    member self.GetWallet([<FromRoute>] userId: Guid) = facade.GetWallet(UserId userId)

    [<HttpPost>]
    [<Route("wallet/deposit")>]
    member self.Deposit([<FromBody>] data) = facade.Deposit data

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
    [<Route("user/{userId}/wallet")>]
    member self.GetWalletOfUser([<FromRoute>] userId: Guid) = facade.GetWalletOfUser(UserId userId)

    [<HttpGet>]
    [<Route("wallet/{walletId}")>]
    member self.GetWallet([<FromRoute>] walletId: Guid) = facade.GetWallet(WalletId walletId)

    [<HttpPost>]
    [<Route("wallet/deposit")>]
    member self.Deposit([<FromBody>] data) = facade.Deposit data

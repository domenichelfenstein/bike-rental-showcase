namespace BikeRental.Accounting

open System
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Mvc
open FSharp.Control.Tasks

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

    [<HttpGet>]
    [<AllowAnonymous>]
    [<Route("ws/user/{userId}")>]
    member self.GetWebSocket([<FromRoute>] userId: Guid) =
        task {
            use! webSocket = self.HttpContext.WebSockets.AcceptWebSocketAsync()
            do! userId |> WebSocket.sendWebSocketMessageOnEvent webSocket facade.UIChanged
        }

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
    member self.Get([<FromRoute>] userId: Guid) = facade.GetWallet(UserId userId)

    [<HttpGet>]
    [<AllowAnonymous>]
    [<Route("ws")>]
    member self.GetWebSocket() =
        task {
            use! webSocket = self.HttpContext.WebSockets.AcceptWebSocketAsync()
            do! WebSocket.sendWebSocketMessageOnEvent webSocket facade.UIChanged
        }

    [<HttpGet>]
    [<AllowAnonymous>]
    [<Route("test")>]
    member self.Test() = task {
        facade.TriggerUIChange "test"
        return [ 1; 2 ]
    }

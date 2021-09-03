namespace BikeRental.Rental

open System
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Mvc
open FsToolkit.ErrorHandling

[<ApiController>]
[<Authorize(AuthenticationSchemes = "FakeAuthenticationScheme")>]
[<Route("rental")>]
type RegistrationApiController(facade: RentalFacade) =
    inherit ControllerBase()

    [<HttpGet>]
    [<Route("bikes")>]
    member self.GetAllBikes() = facade.GetAllBookableBikes

    [<HttpPost>]
    [<Route("rent")>]
    member self.Rent([<FromBody>] data) =
        asyncResult { do! facade.RentBike(Guid.NewGuid() |> BookingId) data }

    [<HttpPost>]
    [<Route("release")>]
    member self.Release([<FromBody>] data) =
        asyncResult { do! facade.ReleaseBike data }

namespace BikeRental.WebApi.Controllers

open System
open Microsoft.AspNetCore.Mvc
open BikeRental.Registration

[<ApiController>]
[<Route("weather")>]
type WeatherForecastController() =
    inherit ControllerBase()

    let summaries =
        [| "Freezing"
           "Bracing"
           "Chilly"
           "Cool"
           "Mild"
           "Warm"
           "Balmy"
           "Hot"
           "Sweltering"
           "Scorching" |]

    [<HttpGet>]
    [<Route("forecast")>]
    member self.Get() =
        let rng = System.Random()

        [| for index in 0 .. 4 ->
               { Date = DateTime.Now.AddDays(float index)
                 TemperatureC = rng.Next(-20, 55)
                 Summary = summaries.[rng.Next(summaries.Length)] } |]

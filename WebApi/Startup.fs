namespace BikeRental.WebApi

open BikeRental.Registration
open BikeRental.WebApi.Controllers
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc.ApplicationParts
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

type Startup(configuration: IConfiguration) =
    member _.Configuration = configuration

    member _.ConfigureServices(services: IServiceCollection) =
        let registrationAssemblyPart = typeof<WeatherForecast>.Assembly |> AssemblyPart

        let accountingAssemblyPart =
            typeof<AccountingController>.Assembly
            |> AssemblyPart

        let parts =
            services
                .AddControllers()
                .PartManager
                .ApplicationParts

        parts.Add(registrationAssemblyPart)
        parts.Add(accountingAssemblyPart)

    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app
            .UseHttpsRedirection()
            .UseRouting()
            .UseAuthorization()
            .UseEndpoints(fun endpoints -> endpoints.MapControllers() |> ignore)
        |> ignore

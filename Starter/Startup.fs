namespace BikeRental.Starter

open BikeRental.Registration
open BikeRental.Accounting
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc.ApplicationParts
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.SpaServices.AngularCli

type Startup(configuration: IConfiguration) =
    member _.Configuration = configuration

    member self.ConfigureServices(services: IServiceCollection) =
        let registrationAssemblyPart =
            typeof<RegistrationFacade>.Assembly
            |> AssemblyPart

        let accountingAssemblyPart = typeof<AccountingId>.Assembly |> AssemblyPart

        let parts =
            services
                .AddControllers()
                .PartManager
                .ApplicationParts

        parts.Add(registrationAssemblyPart)
        parts.Add(accountingAssemblyPart)

        let facades = FacadesCreator.create self.Configuration

        services.AddSingleton<RegistrationFacade>(fun _ -> facades.Registration)
        |> ignore

        ()

    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app
            .UseRouting()
            .UseEndpoints(fun endpoints -> endpoints.MapControllers() |> ignore)
            .UseSpa(fun spa ->
                spa.Options.SourcePath <- "../"
                spa.UseAngularCliServer("start")
                ())
        |> ignore

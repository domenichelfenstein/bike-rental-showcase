namespace BikeRental.Starter

open System.Reflection
open BikeRental.Registration
open BikeRental.Accounting
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc.ApplicationParts
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.FileProviders
open Microsoft.Extensions.Hosting

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

        let getFileProvider assemblyName =
            let assembly = Assembly.Load(AssemblyName(assemblyName))
            EmbeddedFileProvider(assembly, $"{assemblyName}.wwwroot")

        let staticFileOptions =
            StaticFileOptions(
                FileProvider =
                    CompositeFileProvider(
                        getFileProvider "BikeRental.Registration",
                        getFileProvider "BikeRental.Accounting"
                    )
            )

        app
            .UseRouting()
            .UseStaticFiles(staticFileOptions)
            .UseEndpoints(fun endpoints -> endpoints.MapControllers() |> ignore)
        |> ignore

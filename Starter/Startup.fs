namespace BikeRental.Starter

open System
open System.IO
open System.Text.Json
open System.Text.Json.Serialization
open BikeRental.Registration
open BikeRental.Accounting
open BikeRental.Rental
open BikeRental.Starter.FakeAuthentication
open BikeRental.Startup
open Microsoft.AspNetCore.Authentication
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc.ApplicationParts
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.FileProviders
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.SpaServices

type Startup(configuration: IConfiguration) =
    let jsonFSharpConverter =
        JsonFSharpConverter(
            JsonFSharpOptions()
                .WithUnionExternalTag()
                .WithUnionNamedFields()
                .WithUnwrapOption()
                .WithUnionUnwrapFieldlessTags()
                .WithUnionUnwrapSingleCaseUnions()
                .WithUnionUnwrapSingleFieldCases()
                .WithUnionUnwrapRecordCases()
                .WithUnionTagNamingPolicy(JsonNamingPolicy.CamelCase)
                .WithUnionFieldNamingPolicy(JsonNamingPolicy.CamelCase)
                .WithUnionTagCaseInsensitive()
                .WithIncludeRecordProperties()
                .WithUnionAllowUnorderedTag()
        )

    let serializerOptions = JsonSerializerOptions JsonSerializerDefaults.Web

    let applyCustomSerializerOptions (serializerOptions: JsonSerializerOptions) =
        serializerOptions.PropertyNameCaseInsensitive <- false
        serializerOptions.PropertyNamingPolicy <- JsonNamingPolicy.CamelCase
        serializerOptions.Converters.Add(jsonFSharpConverter)

    do applyCustomSerializerOptions (serializerOptions)

    member _.Configuration = configuration

    member self.ConfigureServices(services: IServiceCollection) =
        services
            .AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>(FakeSchemeName, (fun _ -> ()))
        |> ignore

        services.AddAuthorization() |> ignore

        services
            .AddControllers(fun options ->
                options.OutputFormatters.Insert(0, FSharpResultOutputFormatter(serializerOptions)))
            .AddJsonOptions(fun options -> applyCustomSerializerOptions (options.JsonSerializerOptions))
        |> ignore

        let uiChangedEvent, facades = FacadesCreator.create self.Configuration

        services
            .AddSingleton<Event<string * obj>>(fun _ -> uiChangedEvent)
            .AddSingleton<RegistrationFacade>(fun _ -> facades.Registration)
            .AddSingleton<AccountingFacade>(fun _ -> facades.Accounting)
            .AddSingleton<RentalFacade>(fun _ -> facades.Rental)
        |> ignore

        ()

    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        let eventStream = app.ApplicationServices.GetService<Event<string * obj>>()

        app.UseRouting().UseAuthentication().UseAuthorization().UseWebSockets()
        |> ignore

        app
            .UseEndpoints(fun endpoints -> endpoints.MapControllers() |> ignore)
            .Use(WebSocket.wsMiddleware serializerOptions eventStream.Publish)
        |> ignore

#if DEBUG
        app
            .UseDeveloperExceptionPage()
            .UseSpa(fun spa -> spa.UseProxyToSpaDevelopmentServer "http://localhost:8080")
#else
        let fileProvider =
            new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "wwwroot"))

        app
            .UseStaticFiles()
            .UseSpa(fun spa ->
                spa.Options.SourcePath <- "wwwroot"
                spa.Options.DefaultPageStaticFileOptions <- StaticFileOptions(FileProvider = fileProvider)
                spa.Options.DefaultPage <- "/index.html")
#endif

module Program =
    [<EntryPoint>]
    let main args =
        Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder -> webBuilder.UseStartup<Startup>() |> ignore)
            .Build()
            .Run()

        0

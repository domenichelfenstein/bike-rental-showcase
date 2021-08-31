namespace BikeRental.Starter

open System.Security.Claims
open Microsoft.AspNetCore.Authentication
open FSharpx
open FsToolkit.ErrorHandling
open Microsoft.AspNetCore.Http

module FakeAuthentication =
    let FakeSchemeName = "FakeAuthenticationScheme"

    type FakeAuthenticationHandler(options, logger, encoder, clock) =
        inherit AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)

        override this.HandleAuthenticateAsync() =
            let inner (request: HttpRequest) =
                result {
                    let encodedTokenOption =
                        request.Headers
                        |> Seq.tryFind (fun x -> x.Key = "Authorization")
                        |> Option.map (fun x -> x.Value.Item 0)

                    return
                        match encodedTokenOption with
                        | Some encodedToken ->
                            if encodedToken.StartsWith("thisIsAFakeToken+") then
                                let parts = encodedToken.Split("+")

                                let identity =
                                    ClaimsIdentity([ Claim("Username", Array.get parts 1) ], this.Scheme.Name)

                                let principal = System.Security.Principal.GenericPrincipal(identity, null)
                                let ticket = AuthenticationTicket(principal, this.Scheme.Name)
                                AuthenticateResult.Success(ticket)
                            else
                                AuthenticateResult.Fail("token not valid")
                        | None -> AuthenticateResult.NoResult()
                }

            let result = inner this.Request

            let authResult =
                match result with
                | Ok authResult -> authResult
                | Error _ -> AuthenticateResult.NoResult()

            authResult |> Task.singleton

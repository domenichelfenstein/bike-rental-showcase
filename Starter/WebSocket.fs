﻿namespace BikeRental.Startup

open System
open System.Net.WebSockets
open System.Text
open System.Text.Json
open System.Text.Json.Serialization
open System.Threading
open System.Threading.Tasks
open FSharp.Control.Tasks
open Microsoft.AspNetCore.Http

module WebSocket =
    let serialize (input: 'a) =
        let options = JsonSerializerOptions()
        options.Converters.Add(JsonFSharpConverter())
        JsonSerializer.Serialize(input, options)

    let sendWebSocketMessageOnEvent (webSocket: WebSocket) (eventStream: IEvent<string * obj>) filterGuid =
        task {
            let rec waitForEvent () =
                task {
                    printfn "waiting"

                    let! id, obj = Async.AwaitEvent eventStream
                    printfn "change happened"

                    if id = filterGuid then
                        printfn "correct id"
                        let json = serialize obj
                        let serverMsg = Encoding.UTF8.GetBytes json

                        do!
                            webSocket.SendAsync(
                                ArraySegment<byte>(serverMsg, 0, serverMsg.Length),
                                WebSocketMessageType.Text,
                                true,
                                CancellationToken.None
                            )

                        printfn "msg sent"

                        do! waitForEvent ()
                    else
                        printfn "wrong id"
                        do! waitForEvent ()
                }

            do! waitForEvent ()
        }

    let wsMiddleware (eventStream: IEvent<string * obj>) (context: HttpContext) (next: Func<Task>) =
        task {
            if
                context.WebSockets.IsWebSocketRequest
                && context.Request.Path.Value.StartsWith("/ws/id")
            then
                let id = context.Request.Path.Value.Replace("/ws/id/", String.Empty)
                use! webSocket = context.WebSockets.AcceptWebSocketAsync()

                do!
                    id
                    |> sendWebSocketMessageOnEvent webSocket eventStream
            else
                do! next.Invoke()
        }
        :> Task

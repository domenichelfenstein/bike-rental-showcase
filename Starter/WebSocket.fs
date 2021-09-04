namespace BikeRental.Startup

open System
open System.Net.WebSockets
open System.Text
open System.Threading
open System.Threading.Tasks
open FSharp.Control.Tasks
open Microsoft.AspNetCore.Http

module WebSocket =
    let sendWebSocketMessageOnEvent (webSocket: WebSocket) (eventStream: IEvent<string * string>) filterGuid =
        task {
            let rec waitForEvent () =
                task {
                    printfn "waiting"

                    let! id, msg = Async.AwaitEvent eventStream
                    printfn "change happened"

                    if id = filterGuid then
                        printfn "correct id"
                        let serverMsg = Encoding.UTF8.GetBytes msg

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

    let wsMiddleware (eventStream: IEvent<string * string>) (context: HttpContext) (next: Func<Task>) =
        task {
            if context.WebSockets.IsWebSocketRequest && context.Request.Path.Value.StartsWith("/ws/id") then
                let id = context.Request.Path.Value.Replace("/ws/id/", String.Empty)
                use! webSocket = context.WebSockets.AcceptWebSocketAsync()
                do! id |> sendWebSocketMessageOnEvent webSocket eventStream
            else
                do! next.Invoke()
        } :> Task

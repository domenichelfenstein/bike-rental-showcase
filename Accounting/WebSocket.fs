namespace BikeRental.Accounting

open System
open System.Net.WebSockets
open System.Text
open System.Threading
open System.Threading.Tasks
open FSharp.Control.Tasks
open Microsoft.AspNetCore.Http

module WebSocket =
    let sendWebSocketMessageOnEvent (webSocket: WebSocket) eventStream =
        task {
            let rec waitForEvent () =
                task {
                    printfn "waiting"
                    let! _ = Async.AwaitEvent eventStream
                    printfn "change happened"
                    let serverMsg = Encoding.UTF8.GetBytes "change"

                    do!
                        webSocket.SendAsync(
                            ArraySegment<byte>(serverMsg, 0, serverMsg.Length),
                            WebSocketMessageType.Text,
                            true,
                            CancellationToken.None
                        )

                    printfn "msg sent"

                    do! waitForEvent ()
                }

            do! waitForEvent ()
        }

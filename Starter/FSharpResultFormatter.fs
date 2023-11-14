namespace BikeRental.Starter

open System
open System.Reflection
open System.Text
open System.Text.Json
open System.Threading.Tasks
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc.Formatters
open Microsoft.Extensions.Logging
open Microsoft.FSharp.Core
open Microsoft.Net.Http.Headers

type FSharpResultOutputFormatter(serializerOptions: JsonSerializerOptions) =
    inherit TextOutputFormatter()

    do
        base.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"))
        base.SupportedEncodings.Add(Encoding.UTF8)
        base.SupportedEncodings.Add(Encoding.Unicode)

    override this.CanWriteType(typ: Type) =
        typ.IsGenericType && typ.GetGenericTypeDefinition() = typedefof<Result<_, _>>

    override this.WriteResponseBodyAsync(context: OutputFormatterWriteContext, _encoding: Encoding) =
        task {
            if context = null then
                ArgumentNullException("context") |> raise

            let response = context.HttpContext.Response

            let result = context.Object

            let objectType = result.GetType()

            let valueResult = objectType.GetProperty("ResultValue").GetValue(result)
            let errorResult = objectType.GetProperty("ErrorValue").GetValue(result)
            let isOk = objectType.GetProperty("IsOk").GetValue(result) :?> bool

            let statusCode, bodyContent =
                if isOk then
                    StatusCodes.Status200OK, valueResult
                else
                    StatusCodes.Status400BadRequest, errorResult

            response.StatusCode <- statusCode

            // Serialize the F# Result as JSON and write it to the response body
            let json = JsonSerializer.Serialize(bodyContent, serializerOptions)
            do! response.WriteAsync(json)
        }
        :> Task

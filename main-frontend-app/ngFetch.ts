import { FSharpResult, ResultOk, ResultError, Result } from "../Starter/CommonTypes";

export const ngPost = async <TOut = null>(url: string, body: any) : Promise<Result<TOut>> => {
    const response = await fetch(
        url,
        {
            method: "POST",
            body: JSON.stringify(body),
            headers: {
                "Content-Type": "application/json"
            }
        });
    const fsharpResult = <FSharpResult<TOut>>await response.json();

    if(fsharpResult.Case == "Ok") {
        return new ResultOk<TOut>(<any>fsharpResult.Fields[0]);
    } else {
        return new ResultError(<any>fsharpResult.Fields[0]);
    }
}

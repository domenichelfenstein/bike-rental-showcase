import { FSharpResult, ResultOk, ResultError, Result } from "../Starter/CommonTypes";

export const ngPost = async <TOut = null>(url: string, body: any, headers: HeadersInit | undefined = undefined) : Promise<Result<TOut>> => {
    const defaultHeaders: HeadersInit = {
        "Content-Type": "application/json"
    }
    const mergedHeaders = headers == undefined ? defaultHeaders : Object.assign(defaultHeaders, headers);
    const response = await fetch(
        url,
        {
            method: "POST",
            body: JSON.stringify(body),
            headers: mergedHeaders
        });
    const fsharpResult = <FSharpResult<TOut>>await response.json();

    if(fsharpResult.Case == "Ok") {
        return new ResultOk<TOut>(<any>fsharpResult.Fields[0]);
    } else {
        return new ResultError(<any>fsharpResult.Fields[0]);
    }
}

export const ngGet = async <TOut>(url: string, headers: HeadersInit | undefined = undefined) : Promise<TOut> => {
    const defaultHeaders: HeadersInit = {
        "Content-Type": "application/json"
    }
    const mergedHeaders = headers == undefined ? defaultHeaders : Object.assign(defaultHeaders, headers);
    const response = await fetch(
        url,
        {
            method: "GET",
            headers: mergedHeaders
        });
    return await response.json();
}

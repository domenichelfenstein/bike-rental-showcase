import { FSharpResult, ResultOk, ResultError, Result } from "../Starter/CommonTypes";

const mergeHeaders = (headers: HeadersInit | undefined = undefined) => {
    const defaultHeaders: HeadersInit = {
        "Content-Type": "application/json"
    }
    return headers == undefined ? defaultHeaders : Object.assign(defaultHeaders, headers);
}

const parseFSharpResult = <TOut>(fsharpResult: FSharpResult<TOut>) : Result<TOut> => {
    if(fsharpResult.Case == "Ok") {
        return new ResultOk<TOut>(<any>fsharpResult.Fields[0]);
    } else {
        return new ResultError(<any>fsharpResult.Fields[0]);
    }
}

export const fetchPost = async <TOut = null>(url: string, body: any, headers: HeadersInit | undefined = undefined) : Promise<Result<TOut>> => {
    const response = await fetch(
        url,
        {
            method: "POST",
            body: JSON.stringify(body),
            headers: mergeHeaders(headers)
        });

    return parseFSharpResult(await response.json());
}

export const fetchGet = async <TOut>(url: string, headers: HeadersInit | undefined = undefined) : Promise<TOut> => {
    const response = await fetch(
        url,
        {
            method: "GET",
            headers: mergeHeaders(headers)
        });
    return await response.json();
}

export const fetchGetResult = async <TOut>(url: string, headers: HeadersInit | undefined = undefined) : Promise<Result<TOut>> => {
    const response = await fetch(
        url,
        {
            method: "GET",
            headers: mergeHeaders(headers)
        });
    return parseFSharpResult(await response.json());
}

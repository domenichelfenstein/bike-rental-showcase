import {getToken} from "./auth";

export function mergeHeaders(headers: HeadersInit | undefined = undefined) {
    const defaultHeaders: HeadersInit = {
        "Content-Type": "application/json"
    }
    return headers == undefined ? defaultHeaders : Object.assign(defaultHeaders, headers);
}

export class AppResponse<TOut = null, TError = null> {
    constructor(public ok: boolean, public data: TOut | TError) {}

    static Ok<TOut, TError = null>(data: TOut) : AppResponse<TOut, TError> {
        return new AppResponse<TOut, TError>(true, data);
    }

    static Error<TOut, TError>(data: TError) : AppResponse<TOut, TError> {
        return new AppResponse<TOut, TError>(false, data);
    }

    getValue() : TOut {
        if(this.ok) {
            return <TOut>this.data;
        } else {
            throw this.data;
        }
    }

    getError() : TError {
        if(!this.ok) {
            return <TError>this.data;
        } else {
            throw this.data;
        }
    }
}

async function request<TOut = null, TIn = null, TError = null>(method: "POST" | "GET", path: string, body: TIn | undefined, headers: HeadersInit | undefined) : Promise<AppResponse<TOut, TError>> {
    try {
        const currentDomainWithPort = window.location.host;
        const url = `http://${currentDomainWithPort}/api/${path}`;
        const response = await fetch(
            url,
            {
                method: method,
                body: body != undefined ? JSON.stringify(body) : undefined,
                headers: mergeHeaders(headers)
            });

        const json = await response.json();
        if(json.Case == "Ok") {
            return AppResponse.Ok(json.Fields[0]);
        } else if (json.Case == "Error") {
            return AppResponse.Error(json.Fields[0].Case);
        } else {
            return AppResponse.Ok(json);
        }
    } catch (error) {
        console.warn("http response", error);
        return AppResponse.Error(<TError>error);
    }
}

export async function fetchPost<TOut = null, TError = null>(path: string, body: any) : Promise<AppResponse<TOut, TError>> {
    return request("POST", path, body, undefined);
}

export async function authPost<TOut = null, TError = null>(path: string, body: any) : Promise<AppResponse<TOut, TError>> {
    const token = getToken();
    return request("POST", path, body, { "Authorization": `Bearer ${token}` });
}

export async function fetchGet<TOut = null, TError = null>(path: string) : Promise<AppResponse<TOut, TError>> {
    return request("GET", path, undefined, undefined);
}

export async function authGet<TOut = null, TError = null>(path: string) : Promise<AppResponse<TOut, TError>> {
    const token = getToken();
    return request("GET", path, undefined, { "Authorization": `Bearer ${token}` });
}

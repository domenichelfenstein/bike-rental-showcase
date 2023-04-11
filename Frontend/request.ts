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
    const currentDomainWithPort = window.location.host;
    const currentProtocol = window.location.protocol.split(":")[0];
    const url = `${currentProtocol}://${currentDomainWithPort}/api/${path}`;
    const response = await fetch(
        url,
        {
            method: method,
            body: body != undefined ? JSON.stringify(body) : undefined,
            headers: mergeHeaders(headers)
        })
        .catch(error => {
            console.log(error);
            return error;
        });


    if(response.status == 200) {
        const json = await response.json();
        return AppResponse.Ok(json);
    } else if(response.status == 400) {
        const json = await response.json();
        return AppResponse.Error(json);
    } else {
        throw new Error("Unknown error");
    }
}

export async function fetchPost<TOut = null, TError = null>(path: string, body: any) : Promise<AppResponse<TOut, TError>> {
    return request("POST", path, body, undefined);
}

export async function authPost<TOut = null, TError = null>(path: string, body: any) : Promise<AppResponse<TOut, TError>> {
    const token = getToken()!;
    return request("POST", path, body, { "Authorization": token });
}

export async function fetchGet<TOut = null, TError = null>(path: string) : Promise<AppResponse<TOut, TError>> {
    return request("GET", path, undefined, undefined);
}

export async function authGet<TOut = null, TError = null>(path: string) : Promise<AppResponse<TOut, TError>> {
    const token = getToken()!;
    return request("GET", path, undefined, { "Authorization": token });
}

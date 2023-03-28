export function mergeHeaders(headers: HeadersInit | undefined = undefined) {
    const defaultHeaders: HeadersInit = {
        "Content-Type": "application/json"
    }
    return headers == undefined ? defaultHeaders : Object.assign(defaultHeaders, headers);
}

export async function fetchPost<TOut = null, TError = null>(url: string, body: any, headers: HeadersInit | undefined = undefined) : Promise<TOut | TError> {
    try {
        const response = await fetch(
            url,
            {
                method: "POST",
                body: JSON.stringify(body),
                headers: mergeHeaders(headers)
            });

        const json = await response.json();
        return <TOut>json;
    } catch (error) {
        console.warn("http response", error);
        return <TError>error;
    }
}

export async function fetchGet<TOut = null, TError = null>(url: string, headers: HeadersInit | undefined = undefined) : Promise<TOut | TError> {
    try {
        const response = await fetch(
            url,
            {
                method: "GET",
                headers: mergeHeaders(headers)
            });

        const json = await response.json();
        return <TOut>json;
    } catch (error) {
        console.warn("http response", error);
        return <TError>error;
    }
}

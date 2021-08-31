export type FSharpResultCase = {
    Case : string;
}
export type FSharpResult<T = null> = {
    Case : "Ok" | "Error";
    Fields : (FSharpResultCase | T)[]
}

export class ResultOk<T> {
    constructor(
        public value : T
    ) {
    }
}
export class ResultError {
    constructor(
        public errorCode : string
    ) {
    }
}
export type Result<T = null> = ResultOk<T> | ResultError;

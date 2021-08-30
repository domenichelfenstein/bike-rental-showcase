export type ResultCase = {
    Case : string;
}
export type Result = {
    Case : "Ok" | "Error";
    Fields : (ResultCase | null)[]
}

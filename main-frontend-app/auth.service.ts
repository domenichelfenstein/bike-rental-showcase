import { Injectable } from "@angular/core";
import { ngGet, ngPost } from "./ngFetch";
import { ResultOk } from "../Starter/CommonTypes";
import { getCookie, setCookie } from "./cookies";

@Injectable()
export class AuthService {
    public get = <TOut>(url: string) => ngGet<TOut>(url, this.getHeaders());
    public post = <TOut = null>(url: string, body: any) => ngPost<TOut>(url, body, this.getHeaders());

    public login = async (username: string, password: string) => {
        const result = await this.post<string>("/registration/login", { "Username": username, "Password": password });

        if (result instanceof ResultOk) {
            setCookie("token", result.value);
        }

        return result;
    }

    private getHeaders = () : HeadersInit | undefined => {
        const tokenFromCookie = getCookie("token");
        return tokenFromCookie == undefined ? undefined : {
            "Authorization": tokenFromCookie
        };
    }

    public isLoggedIn = () => {
        const tokenFromCookie = getCookie("token");
        return tokenFromCookie != undefined;
    }
}

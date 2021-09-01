import { Injectable } from "@angular/core";
import { fetchGet, fetchGetResult, fetchPost } from "./ngFetch";
import { Result, ResultError, ResultOk } from "../Starter/CommonTypes";
import { getCookie, setCookie } from "./cookies";

@Injectable()
export class AuthService {
    private static TokenKey = "bikeRental_token";

    public get = <TOut>(url: string) => fetchGet<TOut>(url, this.getHeaders());
    public getResult = <TOut>(url: string) => fetchGetResult<TOut>(url, this.getHeaders());
    public post = <TOut = null>(url: string, body: any) => fetchPost<TOut>(url, body, this.getHeaders());

    public login = async (username: string, password: string) => {
        const result = await this.post<string>("/registration/login", { "Username": username, "Password": password });

        if (result instanceof ResultOk) {
            setCookie(AuthService.TokenKey, result.value);
        }

        return result;
    }

    private getHeaders = (): HeadersInit | undefined => {
        const tokenFromCookie = getCookie(AuthService.TokenKey);
        return tokenFromCookie == undefined ? undefined : {
            "Authorization": tokenFromCookie
        };
    }

    public isLoggedIn = () => {
        const tokenFromCookie = getCookie(AuthService.TokenKey);
        return tokenFromCookie != undefined;
    }

    public getUserInfo = (): Result<UserInfo> => {
        const tokenFromCookie = getCookie(AuthService.TokenKey);
        if (tokenFromCookie == undefined) {
            return new ResultError("no_token");
        }

        const [_, username, userid] = tokenFromCookie.split("+");

        return new ResultOk({ UserName: username, UserId: userid });
    }
}

export type UserInfo = {
    UserName: string;
    UserId: string;
}

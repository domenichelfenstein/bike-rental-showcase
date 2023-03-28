import {clearCookie, getCookie, setCookie} from "./cookies";
import {AppResponse} from "./request";

export const TokenKey = "bikeRental_token";

export function storeToken(token: string) {
    setCookie(TokenKey, token);
}

export function removeToken() {
    clearCookie(TokenKey);
}

export function getToken() {
    return getCookie(TokenKey);
}

export function isLoggedIn() {
    return getToken() != undefined;
}

export function getUserInfo(): AppResponse<{ username: string, userid: string }, string> {
    const token = getToken();
    if (token == undefined) {
        return AppResponse.Error("no_token");
    }
    const [_, username, userid] = token.split("+");
    return AppResponse.Ok({username, userid});
}

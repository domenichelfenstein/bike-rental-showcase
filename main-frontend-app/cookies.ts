export const setCookie = (key: string, value: string, hours : number | undefined = undefined) => {
    const getExpires = (h: number) => {
        const date = new Date();
        date.setTime(date.getTime() + (h * 60 * 60 * 1000));
        return "; expires=" + date.toUTCString();
    }
    const expiresPart = hours == undefined ? "" : getExpires(hours);
    document.cookie = key + "=" + (value || "") + expiresPart + "; path=/";
}

export const getCookie = (queryKey: string) : string | undefined => {
    const cookieElements = document.cookie.split(';');
    const element = cookieElements
        .map(element => element.trim().split("="))
        .find(([key, value]) => key == queryKey);
    if(element != undefined) {
        const [key, value] = element;
        return value;
    }
    return undefined;
}

export const setCookie = (name: string, value: string, hours : number | undefined = undefined) => {
    const getExpires = (h: number) => {
        const date = new Date();
        date.setTime(date.getTime() + (h * 60 * 60 * 1000));
        return "; expires=" + date.toUTCString();
    }
    const expiresPart = hours == undefined ? "" : getExpires(hours);
    document.cookie = name + "=" + (value || "") + expiresPart + "; path=/";
}

export const getCookie = (name: string) : string | undefined => {
    const nameEQ = name + "=";
    const ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return undefined;
}

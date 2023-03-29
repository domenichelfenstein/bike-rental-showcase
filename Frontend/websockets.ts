const changeRefs: { [userId: string]: WebSocket } = {};

function getWebSocket(userId: string) {
    if(changeRefs[userId] == undefined) {
        const currentDomainWithPort = window.location.host;
        changeRefs[userId] = new WebSocket(`ws://${currentDomainWithPort}/ws/id/${userId}`);
    }

    return changeRefs[userId];
}

export function listenOn<T>(userId: string, callback: (newValue: BackendChange<T>) => void) {
    if(userId == undefined) {
        throw new Error("userId is undefined");
    }

    const webSocket = getWebSocket(userId);
    webSocket.addEventListener("message", (event: MessageEvent<string>) => {
        console.debug("event", userId, event.data);
        const json = JSON.parse(event.data);
        callback(new BackendChange(json.userId, <T>json.data));
    })
}

export function listenOnAndTriggerImmediately<T>(userId: string, callback: (newValue: BackendChange<T | undefined>) => void) {
    listenOn(userId, callback);
    callback(new BackendChange(userId, undefined));
}

export class BackendChange<T> {
    constructor(public userId: string, public data: T) { }
}

const changeRefs: { [userId: string]: WebSocket } = {};

function getWebSocket(userId: string) {
    if(changeRefs[userId] == undefined) {
        const currentDomainWithPort = window.location.host;
        const currentProtocol = window.location.protocol.split(":")[0] == "https" ? "wss" : "ws";
        changeRefs[userId] = new WebSocket(`${currentProtocol}://${currentDomainWithPort}/ws/id/${userId}`);
    }

    return changeRefs[userId];
}

export function listenOn<T>(eventName: string, callback: (newValue: BackendChange<T>) => void) {
    if(eventName == undefined) {
        throw new Error("eventName is undefined");
    }

    const webSocket = getWebSocket(eventName);
    webSocket.addEventListener("message", (event: MessageEvent<string>) => {
        console.debug("event", eventName, event.data);
        const json = JSON.parse(event.data);
        callback(new BackendChange(json.userId, <T>json));
    })
}

export function listenOnAndTriggerImmediately<T>(userId: string, callback: (newValue: BackendChange<T | undefined>) => void) {
    listenOn(userId, callback);
    callback(new BackendChange(userId, undefined));
}

export class BackendChange<T> {
    constructor(public userId: string, public data: T) { }
}

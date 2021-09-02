import { Injectable, OnDestroy } from "@angular/core";
import { ReplaySubject } from "rxjs";

@Injectable()
export class ChangeService implements OnDestroy {
    private webSockets: [string, WebSocket][] = [];
    public onChange = new ReplaySubject<BackendChange>();

    public listeningForUserChanges = (userId: string) => {
        if(this.webSockets.find(([uid, _]) => uid == userId)) {
            console.log(`listeningForUserChanges: already listening for userId '${userId}'`)
            return;
        }
        const webSocket = new WebSocket(`ws://${window.location.hostname}:${window.location.port}/accounting/ws/user/${userId}`);
        webSocket.addEventListener("message", (event: MessageEvent<string>) => {
            console.log("event", [userId, event.data]);
            this.onChange.next(new BackendChange(userId, event.data));
        });

        this.webSockets.push([userId, webSocket]);
    }

    public closeConnections = () => {
        this.webSockets.forEach(([_, webSocket]) => webSocket.close());
    }

    ngOnDestroy(): void {
        this.closeConnections();
    }
}

export class BackendChange {
    constructor(
        public userId: string,
        public message: string
    ) {
    }
}

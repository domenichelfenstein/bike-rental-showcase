import { Injectable, OnDestroy } from "@angular/core";
import { merge, of, ReplaySubject } from "rxjs";
import { filter } from "rxjs/operators";

@Injectable()
export class ChangeService implements OnDestroy {
    private webSockets: [string, WebSocket][] = [];
    public onChange = new ReplaySubject<BackendChange>();

    public listeningForChanges = (id: string) => {
        if(this.webSockets.find(([uid, _]) => uid == id)) {
            console.log(`listeningForUserChanges: already listening for id '${id}'`)
            return this.onChange.pipe(filter(change => change.id == id));
        }
        const webSocket = new WebSocket(`ws://${window.location.hostname}:${window.location.port}/ws/id/${id}`);
        webSocket.addEventListener("message", (event: MessageEvent<string>) => {
            console.log("event", [id, event.data]);
            this.onChange.next(new BackendChange(id, event.data));
        });

        this.webSockets.push([id, webSocket]);

        return this.onChange.pipe(filter(change => change.id == id));
    }

    public listeningForChangesWithInstantLoad = (id: string) => merge(
        of(new BackendChange(id, "instant")),
        this.listeningForChanges(id)
    );

    public closeConnections = () => {
        this.webSockets.forEach(([_, webSocket]) => webSocket.close());
    }

    ngOnDestroy(): void {
        this.closeConnections();
    }
}

export class BackendChange {
    constructor(
        public id: string,
        public message: string
    ) {
    }
}

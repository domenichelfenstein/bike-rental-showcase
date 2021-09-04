import { Injectable, OnDestroy } from "@angular/core";
import { merge, of, ReplaySubject } from "rxjs";
import { filter, map, shareReplay } from "rxjs/operators";

@Injectable()
export class ChangeService implements OnDestroy {
    private webSockets: [string, WebSocket][] = [];
    private onChange = new ReplaySubject<BackendChange<any>>();

    public listeningForChanges = <T>(id: string | undefined) => {
        if(id == undefined) {
            return of(undefined);
        }

        if(this.webSockets.find(([uid, _]) => uid == id)) {
            console.log(`listeningForUserChanges: already listening for id '${id}'`)
            return this.getChanges<T>(id);
        }
        const webSocket = new WebSocket(`ws://${window.location.hostname}:${window.location.port}/ws/id/${id}`);
        webSocket.addEventListener("message", (event: MessageEvent<string>) => {
            console.log("event", [id, event.data]);
            const json = JSON.parse(event.data);
            this.onChange.next(new BackendChange(id, json));
        });

        this.webSockets.push([id, webSocket]);

        return this.getChanges<T>(id);
    }

    public listeningForChangesWithInstantLoad = <T>(id: string | undefined) => merge(
        of(new BackendChange<T>(<any>id, <any>undefined)),
        this.listeningForChanges<T>(id)
    );

    private getChanges = <T>(id: string) => this.onChange.pipe(
        filter(change => change.id == id),
        map(change => <T>change.message),
        shareReplay());

    public closeConnections = () => {
        this.webSockets.forEach(([_, webSocket]) => webSocket.close());
    }

    ngOnDestroy(): void {
        this.closeConnections();
    }
}

export class BackendChange<T> {
    constructor(
        public id: string,
        public message: T
    ) {
    }
}

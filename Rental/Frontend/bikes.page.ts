import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";
import { ResultError, ResultOk } from "../../Starter/CommonTypes";
import { BehaviorSubject, Observable } from "rxjs";
import { ChangeService } from "../../main-frontend-app/change.service";
import { mergeMap, shareReplay } from "rxjs/operators";

@Component({
    template: `
        <div class="toast toast-error" *ngIf="displayError | async">
            <button class="btn btn-clear float-right" (click)="displayError.next(false)"></button>
            Could not rent bike.
        </div>
        <div class="columns">
            <div class="column col-3 col-xl-6 col-sm-12" *ngFor="let bike of bikes | async">
                <div class="card">
                    <div class="card-header">
                        <h5>{{ bike.name }}</h5>
                        <div class="text-gray">{{ bike.manufacturer }}</div>
                    </div>
                    <img class="img-responsive" [src]="bike.base64Image" [alt]="bike.name">
                    <div class="card-body">
                        <p>{{ bike.price | price }}</p>
                    </div>
                    <div class="card-footer">
                        <button class="btn btn-primary" [disabled]="!(bike | available)" (click)="rent(bike)">Rent</button>
                        <button class="btn" *ngIf="bike.status.Case == 'Releasable'" (click)="release(bike)">Release</button>
                    </div>
                </div>
            </div>
        </div>
    `,
    styles: [`.column {
        padding: .4rem
    }`],
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class BikesPageComponent {
    public bikes: Observable<Bike[]> | undefined;
    public displayError = new BehaviorSubject(false);

    constructor(
        private authService: AuthService,
        changeService: ChangeService
    ) {
        const info = authService.getUserInfo();
        if (info instanceof ResultOk) {
            this.bikes = changeService.listeningForChangesWithInstantLoad("bikes").pipe(
                mergeMap(_ => authService.get<Bike[]>(`/rental/bikes/${info.value.UserId}`)),
                shareReplay()
            );
        }
    }

    rent = async (bike: Bike) => {
        const userInfo = this.authService.getUserInfo();
        if (userInfo instanceof ResultOk) {
            const result = await this.authService.post("/rental/rent", {
                "BikeId": bike.bikeId,
                "UserId": userInfo.value.UserId
            });

            if (result instanceof ResultError) {
                this.displayError.next(true);
            }
        }
    }

    release = async (bike: Bike) => {
        const userInfo = this.authService.getUserInfo();
        if (userInfo instanceof ResultOk && bike.status.Fields != undefined) {
            const result = await this.authService.post("/rental/release", {
                "BookingId": bike.status.Fields[0]
            });

            if (result instanceof ResultError) {
                this.displayError.next(true);
            }
        }
    }
}

export type AvailabilityStatus = "Bookable" | "NotAvailable" | "Releasable"
export type Bike = {
    bikeId: string
    name: string
    manufacturer: string
    price: number
    status: { "Case": AvailabilityStatus, "Fields"?: [string] }
    base64Image: string
}

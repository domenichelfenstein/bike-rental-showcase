import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";

@Component({
    template: `
        <user-wallet></user-wallet>
        <div class="columns">
            <div class="column col-3 col-xl-6 col-sm-12" *ngFor="let bike of bikes | async">
                <a class="card">
                    <div class="card-header">
                        <h5>{{ bike.name }}</h5>
                        <div class="text-gray">{{ bike.manufacturer }}</div>
                    </div>
                    <img class="img-responsive" [src]="bike.base64Image" [alt]="bike.name">
                    <p class="card-body">
                        {{ bike.price | price }}
                    </p>
                </a>
            </div>
        </div>
    `,
    styles: [ `.column { padding: .4rem }` ],
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class BikesPageComponent {
    public bikes: Promise<Bike[]>;

    constructor(
        authService: AuthService
    ) {
        this.bikes = authService.get<Bike[]>("/rental/bikes");
    }
}

type Bike = {
    bikeId: string
    name: string
    manufacturer: string
    price: number
    base64Image: string
}

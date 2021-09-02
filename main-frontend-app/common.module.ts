import { NgModule } from '@angular/core';
import { PricePipe } from "./price.pipe";

@NgModule({
    declarations: [
        PricePipe
    ],
    exports: [
        PricePipe
    ]
})
export class CommonBikeRentalModule {
}

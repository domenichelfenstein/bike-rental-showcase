import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import { CommonModule } from "@angular/common";
import { BikesPageComponent } from "./bikes.page";
import { AccountingModule } from "../../Accounting/Frontend/accounting.module";

const routes: Routes = [
    {path: "bikes", component: BikesPageComponent},
];

@NgModule({
    declarations: [
        BikesPageComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        CommonModule,
        AccountingModule
    ],
    providers: []
})
export class RentalModule {
}

import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AccountingPageComponent} from "./accountingPage.component";
import { CommonModule } from "@angular/common";

const routes: Routes = [
    {path: "accountingPage", component: AccountingPageComponent},
];

@NgModule({
    declarations: [
        AccountingPageComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        CommonModule
    ],
    providers: []
})
export class AccountingModule {
}

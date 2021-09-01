import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {DepositPageComponent} from "./deposit.page";
import { CommonModule } from "@angular/common";
import { WalletComponent } from "./wallet.component";
import { UserWalletComponent } from "./userWallet.component";

const routes: Routes = [
    {path: "deposit", component: DepositPageComponent},
];

@NgModule({
    declarations: [
        DepositPageComponent,
        WalletComponent,
        UserWalletComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        CommonModule
    ],
    exports: [
        UserWalletComponent
    ],
    providers: []
})
export class AccountingModule {
}

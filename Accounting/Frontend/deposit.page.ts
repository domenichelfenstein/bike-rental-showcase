import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ResultOk } from "../../Starter/CommonTypes";
import { AuthService } from "../../main-frontend-app/auth.service";

@Component({
    template: `
        <h1><user-wallet></user-wallet></h1>
        <button (click)="deposit()" class="btn">Deposit</button>`,
    styleUrls: [ "deposit.page.scss" ],
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class DepositPageComponent {
    constructor(
        private authService: AuthService
    ) {
    }

    public deposit = async () => {
        const userInfo = this.authService.getUserInfo();
        if (userInfo instanceof ResultOk) {
            await this.authService.post(
                "/accounting/wallet/deposit",
                { "UserId": userInfo.value.UserId, "Amount": 20.45 });
        }
    }
}

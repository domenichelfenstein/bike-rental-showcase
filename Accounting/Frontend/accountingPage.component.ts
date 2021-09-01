import { Component } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";
import { Result, ResultOk } from "../../Starter/CommonTypes";

@Component({
    template: `Accounting Page: {{ testResult | async | json }}`
})

export class AccountingPageComponent {
    public testResult: Promise<Result<Wallet>> | undefined;

    constructor(
        authService: AuthService
    ) {
        const info = authService.getUserInfo();
        if(info instanceof ResultOk) {
            this.testResult = authService.getResult<Wallet>(`/accounting/wallet/${info.value.UserId}`);
        }
    }
}

type Wallet = {
    walletId: string;
    userId: string;
    balance: number;
}

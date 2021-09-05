import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";
import { ResultOk } from "../../Starter/CommonTypes";

@Component({
    selector: "user-wallet",
    template: `
        <a routerLinkActive="active" [routerLink]="['/', 'accounting', 'deposit']" *ngIf="clickable; else noLink">
            <wallet [walletId]="(wallet | async)?.walletId"></wallet>
        </a>
        <ng-template #noLink>
            <wallet [walletId]="(wallet | async)?.walletId"></wallet>
        </ng-template>`,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserWalletComponent {
    public wallet: Promise<Wallet> | undefined;

    @Input() clickable = true;

    constructor(
        authService: AuthService
    ) {
        const info = authService.getUserInfo();
        if (info instanceof ResultOk) {
            this.wallet = authService.getOkResult<Wallet>(`/accounting/user/${info.value.UserId}/wallet`);
        }
    }
}

type Wallet = {
    walletId: string
}

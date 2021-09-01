import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";
import { ResultOk } from "../../Starter/CommonTypes";

@Component({
    selector: "user-wallet",
    template: `
        <a routerLinkActive="active" [routerLink]="['/', 'accounting', 'deposit']">
            <wallet [userId]="userId"></wallet>
        </a>`,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class UserWalletComponent {
    public userId: string | undefined;

    constructor(
        private authService: AuthService
    ) {
        const info = authService.getUserInfo();
        if(info instanceof ResultOk) {
            this.userId = info.value.UserId;
        }
    }
}

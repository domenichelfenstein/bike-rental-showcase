import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";
import { ResultOk } from "../../Starter/CommonTypes";

@Component({
    selector: "user-badge",
    template: `
        <figure class="avatar avatar-md" [attr.data-initial]="initials | async" style="background-color: #5755d9;"></figure>
    `,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserBadgeComponent {
    public initials: Promise<string> | undefined;

    constructor(
        authService: AuthService
    ) {
        const info = authService.getUserInfo();
        if (info instanceof ResultOk) {
            this.initials = authService.get<string>(`/registration/initials/${info.value.UserName}`);
        }
    }
}

type Wallet = {
    walletId: string
}

import { ChangeDetectionStrategy, Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";
import { ResultOk } from "../../Starter/CommonTypes";
import { BehaviorSubject } from "rxjs";

@Component({
    selector: "wallet",
    template: `Balance: {{ balanceResult | async }}`,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class WalletComponent implements OnChanges {
    @Input() userId: string | undefined;

    public balanceResult = new BehaviorSubject("???");

    constructor(
        private authService: AuthService
    ) {
    }

    async ngOnChanges(changes: SimpleChanges) {
        const userIdChanges = changes["userId"];
        if (userIdChanges.currentValue != undefined) {
            const walletResult = await this.authService.getResult<Wallet>(`/accounting/wallet/${this.userId}`);
            if (walletResult instanceof ResultOk) {
                this.balanceResult.next(`${walletResult.value.balance.toFixed(2)} $`);
            }
        }
    }
}

type Wallet = {
    walletId: string;
    userId: string;
    balance: number;
}

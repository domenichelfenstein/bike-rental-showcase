import { ChangeDetectionStrategy, Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";
import { ResultOk } from "../../Starter/CommonTypes";
import { merge, Observable, of } from "rxjs";
import { ChangeService } from "../../main-frontend-app/change.service";
import { filter, map, mergeMap, shareReplay } from "rxjs/operators";
import { PricePipe } from "../../main-frontend-app/price.pipe";

@Component({
    selector: "wallet",
    template: `Balance: {{ balanceResult | async }}`,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class WalletComponent implements OnChanges {
    @Input() walletId: string | undefined;

    public balanceResult: Observable<string> | undefined;

    constructor(
        private authService: AuthService,
        private changeService: ChangeService
    ) {
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (this.walletId != undefined) {
            const balance = this.changeService.listeningForChangesWithInstantLoad<any>(this.walletId).pipe(
                mergeMap(_ => this.authService.getResult<Wallet>(`/accounting/wallet/${this.walletId}`)),
                filter(result => result instanceof ResultOk),
                map(result => result instanceof ResultOk ? PricePipe.transform(result.value.balance) : PricePipe.transform(undefined)),
                shareReplay()
            );

            this.balanceResult = merge(of("???"), balance);
        }
    }
}

type Wallet = {
    walletId: string;
    userId: string;
    balance: number;
}

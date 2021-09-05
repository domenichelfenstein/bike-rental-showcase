import { ChangeDetectionStrategy, Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";
import { ResultOk } from "../../Starter/CommonTypes";
import { merge, Observable, of } from "rxjs";
import { ChangeService } from "../../main-frontend-app/change.service";
import { filter, map, mergeMap, shareReplay } from "rxjs/operators";
import { PricePipe } from "../../main-frontend-app/price.pipe";

@Component({
    selector: "wallet",
    template: `
        <span [class.text-error]="(balance | async) == 0">
            Balance: {{ balanceResult | async }}
        </span>`,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class WalletComponent implements OnChanges {
    @Input() walletId: string | undefined;

    public balanceResult: Observable<string> | undefined;
    public balance: Observable<number | undefined> | undefined;

    constructor(
        private authService: AuthService,
        private changeService: ChangeService
    ) {
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (this.walletId != undefined) {
            this.balance = this.changeService.listeningForChangesWithInstantLoad<any>(this.walletId).pipe(
                mergeMap(_ => this.authService.getResult<Wallet>(`/accounting/wallet/${this.walletId}`)),
                filter(result => result instanceof ResultOk),
                map(result => result instanceof ResultOk ? result.value.balance : undefined),
                shareReplay()
            );
            const balance = this.balance.pipe(
                map(balance => PricePipe.transform(balance)),
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

import { ChangeDetectionStrategy, Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";
import { ResultOk } from "../../Starter/CommonTypes";
import { merge, Observable, of, ReplaySubject } from "rxjs";
import { ChangeService } from "../../main-frontend-app/change.service";
import { filter, map, mergeMap, shareReplay } from "rxjs/operators";
import { PricePipe } from "../../main-frontend-app/price.pipe";

@Component({
    selector: "wallet",
    template: `Balance: {{ balanceResult | async }}`,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class WalletComponent implements OnChanges {
    @Input() userId: string | undefined;

    private userIdChanged = new ReplaySubject<string>();
    public balanceResult: Observable<string>;

    constructor(
        private authService: AuthService,
        private changeService: ChangeService
    ) {
        const loadTrigger = merge(
            this.userIdChanged,
            this.changeService.onChange.pipe(
                filter(x => x.userId == this.userId && x.message == "accounting"),
                map(x => x.userId)));

        const balance = loadTrigger.pipe(
            mergeMap(userId => this.authService.getResult<Wallet>(`/accounting/wallet/${userId}`)),
            filter(result => result instanceof ResultOk),
            map(result => result instanceof ResultOk ? PricePipe.transform(result.value.balance) : PricePipe.transform(undefined)),
            shareReplay()
        );

        this.balanceResult = merge(of("???"), balance);
    }

    async ngOnChanges(changes: SimpleChanges) {
        if (this.userId != undefined) {
            this.userIdChanged.next(this.userId);
            this.changeService.listeningForUserChanges(this.userId);
        }
    }
}

type Wallet = {
    walletId: string;
    userId: string;
    balance: number;
}

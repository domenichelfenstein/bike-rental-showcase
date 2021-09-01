import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    template: `Make a deposit!`,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class DepositPageComponent {
    public userId: string | undefined;

    constructor(
    ) {
    }
}

import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    template: `Bikes!<user-wallet></user-wallet>`,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class BikesPageComponent {
    public userId: string | undefined;

    constructor(
    ) {
    }
}

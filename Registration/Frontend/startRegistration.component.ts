import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    template: `sadf`,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class StartRegistrationPageComponent {
    constructor() {
        fetch("/accounting/test")
    }
}

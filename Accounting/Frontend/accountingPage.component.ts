import {Component} from '@angular/core';

@Component({
    template: `Accounting Page xyz {{ test }}`
})

export class AccountingPageComponent {
    test = 1 + 3;
}

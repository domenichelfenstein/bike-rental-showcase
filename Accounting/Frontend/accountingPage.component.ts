import { Component } from '@angular/core';

@Component({
    template: `Accounting Page xyz {{ test }} <img src="./Accounting/Frontend/assets/testbild2.jpg" height="100" />`
})

export class AccountingPageComponent {
    test = 1 + 3;
}

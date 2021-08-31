import { Component } from '@angular/core';
import { AuthService } from "../../main-frontend-app/auth.service";

@Component({
    template: `Accounting Page: {{ testResult | async | json }}`
})

export class AccountingPageComponent {
    public testResult: Promise<number[]>;

    constructor(
        authService: AuthService
    ) {
        this.testResult = authService.get<number[]>("/accounting/test");
    }
}

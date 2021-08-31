import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BehaviorSubject } from "rxjs";
import { ResultError, ResultOk } from "../../Starter/CommonTypes";
import { ngGet, ngPost } from "../../main-frontend-app/ngFetch";
import { Router } from "@angular/router";

@Component({
    template: `
        <form #form="ngForm" class="column col-4 col-mx-auto card">
            <div class="card-header">
                <div class="card-title h5">Registration</div>
            </div>
            <div class="card-body">
                <div class="toast toast-error" *ngIf="displayError | async">
                    <button class="btn btn-clear float-right" (click)="displayError.next(false)"></button>
                    Error registering user.
                </div>
                <div class="form-group">
                    <label class="form-label" for="username">Username</label>
                    <input [(ngModel)]="username" name="username" class="form-input" type="text" id="username" required>
                </div>
                <div class="form-group">
                    <label class="form-label" for="phoneNumber">Phone Number</label>
                    <input [(ngModel)]="phoneNumber" name="phoneNumber" class="form-input" type="tel" id="phoneNumber"
                           required>
                </div>
                <div class="card-footer">
                    <button class="btn btn-primary" (click)="register()" [disabled]="form.invalid || form.untouched">
                        Register
                    </button>
                </div>
            </div>
        </form>
    `,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class StartRegistrationPageComponent {
    public displayError = new BehaviorSubject(false);
    public username = "";
    public phoneNumber = "";

    constructor(
        private router: Router
    ) {
        ngGet<number[]>("/accounting/test", { "Authorization": "thisIsAFakeToken+Blubb+X+Y" }).then(console.log);
    }

    public register = async () => {
        const response = await ngPost(
            "/registration/start",
            { "PhoneNumber": this.phoneNumber, "Username": this.username });
        this.displayError.next(response instanceof ResultError);

        if (response instanceof ResultOk) {
            await this.router.navigate(["registration", "verify", this.username]);
        }
    }
}

import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BehaviorSubject, Observable } from "rxjs";
import { ResultError, ResultOk } from "../../Starter/CommonTypes";
import { ngPost } from "../../main-frontend-app/ngFetch";
import { ActivatedRoute, Router } from "@angular/router";
import { map } from "rxjs/operators";

@Component({
    template: `
        <form #form="ngForm" class="column col-4 col-mx-auto card">
            <div class="card-header">
                <div class="card-title h5">Verify phone number</div>
            </div>
            <div class="card-body">
                <div class="toast toast-error" *ngIf="displayError | async">
                    <button class="btn btn-clear float-right" (click)="displayError.next(false)"></button>
                    Wrong verification code.
                </div>
                <div class="form-group">
                    <label class="form-label" for="userName">Username</label>
                    <input [ngModel]="userName | async" (ngModelChange)="changeUserName($event)" name="userName"
                           class="form-input" type="text" id="userName"
                           required>
                </div>
                <div class="form-group">
                    <label class="form-label" for="verificationCode">Verification Code</label>
                    <input [(ngModel)]="verificationCode" name="verificationCode" class="form-input" type="text"
                           id="verificationCode"
                           required>
                </div>
                <div class="card-footer">
                    <button class="btn btn-primary" *ngIf="userName | async as $username"
                            (click)="verify($username)" [disabled]="form.invalid || form.untouched">
                        Verify
                    </button>
                </div>
            </div>
        </form>
    `,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class VerifiyPhonePageComponent {
    public displayError = new BehaviorSubject(false);
    public userName: Observable<string>;
    public verificationCode = "";

    constructor(
        activatedRoute: ActivatedRoute,
        private router: Router
    ) {
        this.userName = activatedRoute.params.pipe(map(x => x.username));
    }

    changeUserName = async (userName: string) => {
        await this.router.navigate(["registration", "verify", userName]);
    }

    public verify = async (userName: string) => {
        const response = await ngPost<string>(
            "/registration/verify",
            { "Username": userName, "VerificationCode": this.verificationCode });
        this.displayError.next(response instanceof ResultError);

        if (response instanceof ResultOk) {
            await this.router.navigate(["registration", "complete", userName, response.value]);
        }
    }
}

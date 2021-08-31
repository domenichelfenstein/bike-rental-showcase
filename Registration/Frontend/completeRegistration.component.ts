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
                <h5>Complete Registration</h5>
            </div>
            <div class="card-body">
                <div class="toast toast-error" *ngIf="displayError | async">
                    <button class="btn btn-clear float-right" (click)="displayError.next(false)"></button>
                    Something went wrong.
                </div>
                <div class="form-group">
                    <label class="form-label" for="userName">Username</label>
                    <input [ngModel]="userName | async" name="userName" class="form-input" type="text" id="userName"
                           disabled>
                </div>
                <div class="form-group">
                    <label class="form-label" for="firstName">First Name</label>
                    <input [(ngModel)]="firstName" name="firstName" class="form-input" type="text"
                           id="firstName"
                           required>
                </div>
                <div class="form-group">
                    <label class="form-label" for="lastName">Last Name</label>
                    <input [(ngModel)]="lastName" name="lastName" class="form-input" type="text"
                           id="lastName"
                           required>
                </div>
                <div class="form-group">
                    <label class="form-label" for="password1">Password</label>
                    <input [(ngModel)]="password1" name="password1" class="form-input" type="password"
                           id="password1"
                           required>
                </div>
                <div class="form-group">
                    <label class="form-label" for="password2">Password Verification</label>
                    <input [(ngModel)]="password2" name="password2" class="form-input" type="password"
                           id="password2"
                           required>
                </div>
            </div>
            <div class="card-footer">
                <button class="btn btn-primary" *ngIf="fromRoute | async as $fromRoute"
                        (click)="complete($fromRoute)" [disabled]="form.invalid || form.untouched || (password1 != password2)">
                    Verify
                </button>
            </div>
        </form>
    `,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CompleteRegistrationPageComponent {
    public displayError = new BehaviorSubject(false);
    public fromRoute: Observable<[string, string]>;
    public userName: Observable<string>;
    public firstName = "";
    public lastName = "";
    public password1 = "";
    public password2 = "";

    constructor(
        activatedRoute : ActivatedRoute,
        private router: Router
    ) {
        this.fromRoute = activatedRoute.params.pipe(map(x => [ x.username, x.completionId ]));
        this.userName = activatedRoute.params.pipe(map(x => x.username));
    }

    public complete = async ([username, completionId]: [string, string]) => {
        const response = await ngPost(
            "/registration/complete",
            { "Username": username, "CompletionId": completionId, "FirstName": this.firstName, "LastName": this.lastName, "Password": this.password1 });
        this.displayError.next(response instanceof ResultError);

        if(response instanceof ResultOk) {
            console.log("juhu!");
        }
    }
}

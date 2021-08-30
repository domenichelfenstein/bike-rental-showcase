import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BehaviorSubject } from "rxjs";
import { Result } from "../../Starter/CommonTypes";
import { ngPost } from "../../main-frontend-app/ngFetch";

@Component({
    template: `
    <form #form="ngForm" class="col-4 col-mx-auto">
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
            <input [(ngModel)]="phoneNumber" name="phoneNumber" class="form-input" type="tel" id="phoneNumber" required>
        </div>
        <button class="btn btn-primary" (click)="register()" [disabled]="form.invalid || form.untouched">Register</button>
    </form>
    `,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class StartRegistrationPageComponent {
    public displayError = new BehaviorSubject(false);
    public username = "";
    public phoneNumber = "";

    public register = async () => {
        const response = await ngPost<Result>(
            "/registration/start",
            { "PhoneNumber": this.phoneNumber, "Username": this.username });
        this.displayError.next(response.Case == "Error");
    }
}

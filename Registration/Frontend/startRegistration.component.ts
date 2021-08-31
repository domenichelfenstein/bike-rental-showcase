import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BehaviorSubject } from "rxjs";
import { ResultError, ResultOk } from "../../Starter/CommonTypes";
import { ngPost } from "../../main-frontend-app/ngFetch";
import { Router } from "@angular/router";
import { AuthService } from "../../main-frontend-app/auth.service";

@Component({
    template: `
        <div class="column col-8 col-mx-auto card">
            <div class="columns card-body">
                <form #loginForm="ngForm" class="column col-6">
                    <h5>Login</h5>
                    <div class="toast toast-error" *ngIf="displayLoginError | async">
                        <button class="btn btn-clear float-right"
                                (click)="displayLoginError.next(false)"></button>
                        Login error.
                    </div>
                    <div class="form-group">
                        <label class="form-label" for="username">Username</label>
                        <input [(ngModel)]="loginUsername" name="username" class="form-input" type="text" id="login_username"
                               required>
                    </div>
                    <div class="form-group">
                        <label class="form-label" for="password">Password</label>
                        <input [(ngModel)]="password" name="password" class="form-input" type="password"
                               id="password"
                               required>
                    </div>
                    <div class="card-footer">
                        <button class="btn btn-primary" (click)="login()"
                                [disabled]="loginForm.invalid || loginForm.untouched">
                            Login
                        </button>
                    </div>
                </form>
                <div class="divider-vert" data-content="OR"></div>
                <form #regForm="ngForm" class="column">
                    <h5>Registration</h5>
                    <div class="toast toast-error" *ngIf="displayRegistrationError | async">
                        <button class="btn btn-clear float-right"
                                (click)="displayRegistrationError.next(false)"></button>
                        Error registering user.
                    </div>
                    <div class="form-group">
                        <label class="form-label" for="username">Username</label>
                        <input [(ngModel)]="regUsername" name="username" class="form-input" type="text"
                               id="registration_username"
                               required>
                    </div>
                    <div class="form-group">
                        <label class="form-label" for="phoneNumber">Phone Number</label>
                        <input [(ngModel)]="phoneNumber" name="phoneNumber" class="form-input" type="tel"
                               id="phoneNumber"
                               required>
                    </div>
                    <div class="card-footer">
                        <button class="btn btn-primary" (click)="register()"
                                [disabled]="regForm.invalid || regForm.untouched">
                            Register
                        </button>
                    </div>
                </form>
            </div>
        </div>
    `,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class StartRegistrationPageComponent {
    public displayRegistrationError = new BehaviorSubject(false);
    public displayLoginError = new BehaviorSubject(false);
    public loginUsername = "";
    public regUsername = "";
    public phoneNumber = "";
    public password = "";

    constructor(
        private router: Router,
        private authService: AuthService
    ) {
    }

    public register = async () => {
        const response = await ngPost(
            "/registration/start",
            { "PhoneNumber": this.phoneNumber, "Username": this.regUsername });
        this.displayRegistrationError.next(response instanceof ResultError);

        if (response instanceof ResultOk) {
            await this.router.navigate(["registration", "verify", this.regUsername]);
        }
    }

    public login = async () => {
        const response = await this.authService.login(this.loginUsername, this.password);
        this.displayLoginError.next(response instanceof ResultError);

        if (response instanceof ResultOk) {
            await this.router.navigate([""]);
        }
    }
}

import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { AuthService } from "./auth.service";
import { Router } from "@angular/router";

@Component({
    selector: 'app-root',
    template: `
        <header>
            <a [routerLink]="['rental', 'bikes']"><h1>Bike rental</h1></a>
            <div class="placeholder"></div>
            <div class="dropdown dropdown-right" *ngIf="authService.isLoggedInChange | async">
                <a class="dropdown-toggle" tabindex="0">
                    <user-badge></user-badge>
                    <user-wallet [clickable]="false"></user-wallet>
                </a>
                <ul class="menu">
                    <li class="menu-item"><a [routerLink]="['accounting', 'deposit']">Deposit</a></li>
                    <li class="menu-item"><a (click)="logout()">Logout</a></li>
                </ul>
            </div>
        </header>
        <main class="container">
            <router-outlet></router-outlet>
        </main>
        <footer>
            This is a F# DDD show case by Domenic Helfenstein
        </footer>`,
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation: ViewEncapsulation.None,
    styleUrls: [ "global.style.scss" ]
})
export class AppComponent {
    constructor(
        public authService: AuthService,
        private router: Router
    ) {
    }

    public logout = async () => {
        this.authService.logout();
            await this.router.navigate(["/", "registration", "start"]);
    }
}

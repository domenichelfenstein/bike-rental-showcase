import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    template: `
        <a routerLinkActive="active" [routerLink]="['page1']">Open Page 1</a>
        <a routerLinkActive="active" [routerLink]="['accounting', 'accountingPage']">Open Accounting-Page</a>
        <router-outlet></router-outlet>

        <img src="main-frontend-app/assets/testbild.jpg" height="100"/>`
})
export class AppComponent {
}

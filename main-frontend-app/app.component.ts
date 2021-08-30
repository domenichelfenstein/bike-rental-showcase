import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'app-root',
    template: `
        <header>
            <nav>
                <a routerLinkActive="active" [routerLink]="['registration', 'start']">Start Registration</a>
                <a routerLinkActive="active" [routerLink]="['accounting', 'accountingPage']">Open Accounting-Page</a>
            </nav>
        </header>
        <main>
            <router-outlet></router-outlet>
        </main>`,
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation: ViewEncapsulation.None,
    styleUrls: [ "app.component.scss" ]
})
export class AppComponent {
}

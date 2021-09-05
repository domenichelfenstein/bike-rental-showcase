import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'app-root',
    template: `
        <header>
            <a [routerLink]="['rental', 'bikes']"><h1>Bike rental</h1></a>
            <nav>
                <user-wallet></user-wallet>
            </nav>
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
}

import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'app-root',
    template: `
        <div class="off-canvas off-canvas-sidebar-show">
            <a class="off-canvas-toggle btn btn-primary btn-action" (click)="sidebarActive = true">
                <i class="icon icon-menu"></i>
            </a>
            <header class="off-canvas-sidebar" [class.active]="sidebarActive">
                <ul class="menu menu-nav">
                    <li class="menu-item"><a routerLinkActive="active" [routerLink]="['registration', 'start']">Start Registration</a></li>
                    <li class="menu-item"><a routerLinkActive="active" [routerLink]="['accounting', 'accountingPage']">Open Accounting-Page</a></li>
                </ul>
            </header>
            <a class="off-canvas-overlay" (click)="sidebarActive = false"></a>
            <main class="off-canvas-content">
                <router-outlet></router-outlet>
            </main>
        </div>`,
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation: ViewEncapsulation.None,
    styleUrls: [ "global.style.scss" ]
})
export class AppComponent {
    public sidebarActive = false;
}

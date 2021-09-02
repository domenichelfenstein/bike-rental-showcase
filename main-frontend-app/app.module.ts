import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { PreloadAllModulesStrategy } from "./preloadStrategy";
import { AuthService } from "./auth.service";
import { MustBeLoggedInGuard } from "./mustBeLoggedIn.guard";
import { MustNotBeLoggedInGuard } from "./mustNotBeLoggedIn.guard";
import { ChangeService } from "./change.service";

const routes: Routes = [
    {
        path: "accounting",
        loadChildren: () => import("../Accounting/Frontend/accounting.module").then(m => m.AccountingModule),
        canActivate: [MustBeLoggedInGuard]
    },
    {
        path: "rental",
        loadChildren: () => import("../Rental/Frontend/rental.module").then(m => m.RentalModule),
        canActivate: [MustBeLoggedInGuard]
    },
    {
        path: "registration",
        loadChildren: () => import("../Registration/Frontend/registration.module").then(m => m.RegistrationModule),
        canActivate: [MustNotBeLoggedInGuard]
    },
    { path: '**', redirectTo: "rental/bikes" }
];

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModulesStrategy }),
        BrowserModule
    ],
    providers: [AuthService, MustBeLoggedInGuard, MustNotBeLoggedInGuard, ChangeService],
    bootstrap: [AppComponent]
})
export class AppModule {
}

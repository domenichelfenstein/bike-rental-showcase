import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';

const routes: Routes = [
    {
        path: "accounting",
        loadChildren: () => import("../Accounting/Frontend/accounting.module").then(m => m.AccountingModule)
    },
    {
        path: "registration",
        loadChildren: () => import("../Registration/Frontend/registration.module").then(m => m.RegistrationModule)
    },
    { path: '**', redirectTo: "registration/start" }
];

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        RouterModule.forRoot(routes),
        BrowserModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { Page1Component } from "./page1.component";

const routes: Routes = [
    { path: "page1", component: Page1Component },
    {
        path: "accounting",
        loadChildren: () => import("../Accounting/Frontend/accounting.module").then(m => m.AccountingModule)
    },
];

@NgModule({
    declarations: [
        AppComponent,
        Page1Component
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

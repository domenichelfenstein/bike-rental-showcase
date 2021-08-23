import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AccountingPageComponent } from "./accountingPage.component";

const routes: Routes = [
  { path: "accountingPage", component: AccountingPageComponent },
];

@NgModule({
  declarations: [
    AccountingPageComponent
  ],
  imports: [
    RouterModule.forChild(routes)
  ],
  providers: []
})
export class AccountingModule { }

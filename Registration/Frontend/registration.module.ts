import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartRegistrationPageComponent } from "./startRegistration.component";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";

const routes: Routes = [
    { path: "start", component: StartRegistrationPageComponent },
];

@NgModule({
    declarations: [
        StartRegistrationPageComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        FormsModule,
        CommonModule
    ],
    providers: []
})
export class RegistrationModule {
}

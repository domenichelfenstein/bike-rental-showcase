import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartRegistrationPageComponent } from "./startRegistration.component";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { VerifiyPhonePageComponent } from "./verifyPhone.component";

const routes: Routes = [
    { path: "start", component: StartRegistrationPageComponent },
    { path: 'verify', redirectTo: 'verify/', pathMatch: 'full' },
    { path: 'verify/:username', component: VerifiyPhonePageComponent },
];

@NgModule({
    declarations: [
        StartRegistrationPageComponent,
        VerifiyPhonePageComponent
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

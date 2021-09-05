import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartRegistrationPageComponent } from "./startRegistration.component";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { VerifiyPhonePageComponent } from "./verifyPhone.component";
import { CompleteRegistrationPageComponent } from "./completeRegistration.component";
import { UserBadgeComponent } from "./userBadge.component";

const routes: Routes = [
    { path: "start", component: StartRegistrationPageComponent },

    { path: 'verify', redirectTo: 'verify/', pathMatch: 'full' },
    { path: 'verify/:username', component: VerifiyPhonePageComponent },

    { path: 'complete', redirectTo: 'complete/', pathMatch: 'full' },
    { path: 'complete/:username/:completionId', component: CompleteRegistrationPageComponent },
];

@NgModule({
    declarations: [
        StartRegistrationPageComponent,
        VerifiyPhonePageComponent,
        CompleteRegistrationPageComponent,
        UserBadgeComponent
    ],
    exports: [
        UserBadgeComponent
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

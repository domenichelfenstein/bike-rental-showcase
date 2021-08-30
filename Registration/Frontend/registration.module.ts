import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartRegistrationPageComponent } from "./startRegistration.component";

const routes: Routes = [
    { path: "start", component: StartRegistrationPageComponent },
];

@NgModule({
    declarations: [
        StartRegistrationPageComponent
    ],
    imports: [
        RouterModule.forChild(routes)
    ],
    providers: []
})
export class RegistrationModule {
}

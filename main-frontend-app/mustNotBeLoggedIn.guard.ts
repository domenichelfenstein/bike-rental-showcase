import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthService } from "./auth.service";

@Injectable()
export class MustNotBeLoggedInGuard implements CanActivate {

    constructor(
        private authService: AuthService,
        private router: Router) { }

    canActivate() {
        if (!this.authService.isLoggedIn()) {
            return true;
        }

        this.router.navigate([""]);
        return false;
    }
}

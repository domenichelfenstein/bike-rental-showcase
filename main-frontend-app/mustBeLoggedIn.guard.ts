import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthService } from "./auth.service";

@Injectable()
export class MustBeLoggedInGuard implements CanActivate {

    constructor(
        private authService: AuthService,
        private router: Router) { }

    canActivate() {
        if (this.authService.isLoggedIn()) {
            return true;
        }

        this.router.navigate(["registration", "start"]);
        return false;
    }
}

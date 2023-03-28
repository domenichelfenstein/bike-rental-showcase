import {NavigationGuardWithThis} from "vue-router";
import {isLoggedIn} from "./auth";

export const mustBeLoggedIn: NavigationGuardWithThis<unknown> = (to, from, next) => {
    if(isLoggedIn()) {
        next();
    } else {
        next("/registration/start");
    }
}

export const mustBeLoggedOut: NavigationGuardWithThis<unknown> = (to, from, next) => {
    if(isLoggedIn()) {
        next("/");
    } else {
        next();
    }
}

import {NavigationGuardWithThis} from "vue-router";

export const mustBeLoggedIn: NavigationGuardWithThis<unknown> = (to, from, next) => {
    next("/registration/start");
}

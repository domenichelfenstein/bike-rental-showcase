import { RouteRecordRaw } from 'vue-router'
import {mustBeLoggedIn} from "../../Frontend/guards";
import Overview from "./Overview.vue";

const routes: RouteRecordRaw[] = [
    {
        path: 'overview',
        component: Overview,
        beforeEnter: mustBeLoggedIn
    }
]

export default routes;

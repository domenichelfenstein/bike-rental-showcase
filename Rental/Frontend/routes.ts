import { RouteRecordRaw } from 'vue-router'
import RentalTest from "./RentalTest.vue";
import {mustBeLoggedIn} from "../../Frontend/guards";

const routes: RouteRecordRaw[] = [
    {
        path: 'overview',
        component: RentalTest,
        beforeEnter: mustBeLoggedIn
    }
]

export default routes;

import { RouteRecordRaw } from 'vue-router'
import StartRegistrationView from "./StartRegistrationView.vue";

const routes: RouteRecordRaw[] = [
    {
        path: '/registration/start',
        component: StartRegistrationView
    }
]

export default routes;

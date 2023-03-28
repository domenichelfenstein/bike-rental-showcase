import { RouteRecordRaw } from 'vue-router'
import StartRegistrationView from "./StartRegistrationView.vue";
import VerifyPhoneView from "./VerifyPhoneView.vue";
import CompleteRegistrationView from "./CompleteRegistrationView.vue";

const routes: RouteRecordRaw[] = [
    {
        path: 'start',
        component: StartRegistrationView
    },
    {
        path: 'verify/:username',
        component: VerifyPhoneView
    },
    {
        path: 'complete/:username/:id',
        component: CompleteRegistrationView
    }
]

export default routes;

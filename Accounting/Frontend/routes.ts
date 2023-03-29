import { RouteRecordRaw } from 'vue-router'
import DepositView from "./DepositView.vue";

const routes: RouteRecordRaw[] = [
    {
        path: 'deposit',
        component: DepositView
    }
]

export default routes;

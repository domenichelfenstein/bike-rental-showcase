import { createRouter, createWebHistory } from 'vue-router'
import registrationRoutes from '../Registration/Frontend/routes'
import rentalRoutes from '../Rental/Frontend/routes'
import MainApp from "./MainApp.vue";
import {mustBeLoggedOut} from "./guards";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            redirect: '/rental/overview'
        },
        {
            path: '/rental',
            component: MainApp,
            children: rentalRoutes
        },
        {
            path: '/registration',
            beforeEnter: mustBeLoggedOut,
            children: registrationRoutes
        }
    ]
})

export default router

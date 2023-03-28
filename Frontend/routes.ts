import { createRouter, createWebHistory } from 'vue-router'
import registrationRoutes from '../Registration/Frontend/routes'
import rentalRoutes from '../Rental/Frontend/routes'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            redirect: '/rental/overview'
        },
        {
            path: '/rental',
            children: rentalRoutes
        },
        {
            path: '/registration',
            children: registrationRoutes
        }
    ]
})

export default router

import { createRouter, createWebHistory } from 'vue-router'
import registrationRoutes from '../Registration/Frontend/routes'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        ...registrationRoutes
    ]
})

export default router

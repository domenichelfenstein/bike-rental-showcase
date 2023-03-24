import { createApp } from 'vue'
import App from './App.vue'

type Test = {
    X: string
}

const test: Test = { X: "1234" }

createApp(App).mount('#app')

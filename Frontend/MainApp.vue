<script setup lang="ts">
import {RouterView, RouterLink, useRouter} from "vue-router";
import {removeToken} from "./auth";
import UserBadge from "../Registration/Frontend/UserBadge.vue";
import UserWallet from "../Accounting/Frontend/UserWallet.vue";

const router = useRouter();

function logout() {
    removeToken();
    router.push("/registration/start");
}
</script>

<template>
    <header>
        <RouterLink to="/rental/bikes"><h1>Bike rental</h1></RouterLink>
        <div class="placeholder"></div>
        <div class="dropdown dropdown-right">
            <a class="dropdown-toggle" tabindex="0">
                <UserBadge/>
                <UserWallet :clickable="false" />
            </a>
            <ul class="menu">
                <li class="menu-item">
                    <RouterLink to="/accounting/deposit">Deposit</RouterLink>
                </li>
                <li class="menu-item"><a @click="logout()">Logout</a></li>
            </ul>
        </div>
    </header>
    <main class="container">
        <RouterView></RouterView>
    </main>
    <footer>
        This is a F# DDD show case by Domenic Helfenstein
    </footer>
</template>
<style lang="scss" scoped>
.menu {
    a {
        cursor: pointer;
    }
}
</style>

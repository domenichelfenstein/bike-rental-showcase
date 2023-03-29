<template>
    <h1><UserWallet /></h1>
    <button @click="deposit()" class="btn">Deposit</button>
</template>

<script setup lang="ts">
import UserWallet from "./UserWallet.vue";
import {authPost} from "../../Frontend/request";
import {getUserInfo} from "../../Frontend/auth";

async function deposit() {
    const userInfo = getUserInfo();
    await authPost("accounting/wallet/deposit", { userId: userInfo.getValue().userid, amount: 20.45 });
}
</script>

<style lang="scss">
main.container { // main container is used by the router view and therefore gets set to this component -> equivalent to :host
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    a {
        text-decoration: none;

        span {
            color: #bcc3ce !important;
        }
    }
}
</style>

<template>
    <RouterLink to="/accounting/deposit" v-if="clickable">
        <span :class="{ 'text-error': balance == 0 }">
            Balance: {{ formattedBalance }}
        </span>
    </RouterLink>
    <span v-else :class="{ 'text-error': balance == 0 }">
        Balance: {{ formattedBalance }}
    </span>
</template>

<script setup lang="ts">
import {computed, onMounted, ref} from "vue";
import {getUserInfo} from "../../Frontend/auth";
import {authGet} from "../../Frontend/request";

defineProps({
    clickable: {
        type: Boolean,
        default: true
    }
});

const balance = ref(0);
const formattedBalance = computed(() => balance.value ? `${balance.value.toFixed(2)} $` : "0.00 $");

onMounted(async () => {
    const userInfo = getUserInfo();

    const wallet = await authGet<{ walletId: string }>(`accounting/user/${userInfo.getValue().username}/wallet`);
    if(wallet.ok) {
        const walletDetail = await authGet<WalletDetail>(`accounting/wallet/${wallet.getValue().walletId}`);
        if(walletDetail.ok) {
            balance.value = walletDetail.getValue().balance;
        }
    }
})

type WalletDetail = {
    walletId: string;
    userId: string;
    balance: number;
}
</script>
<style lang="scss" scoped>
span {
    cursor: default;
}
</style>

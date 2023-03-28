<template>
    <figure class="avatar avatar-md" :data-initial="initials" style="background-color: #5755d9;"></figure>
</template>

<script setup lang="ts">
import {onMounted, ref} from "vue";
import {fetchGet} from "../../Frontend/request";
import {getUserInfo} from "../../Frontend/auth";

const initials = ref("");

onMounted(async () => {
    const userInfo = getUserInfo();
    const result = await fetchGet<string>(`registration/initials/${userInfo.getValue().username}`);
    initials.value = result.getValue();
});
</script>

<style scoped>
figure {
    cursor: pointer;
}
</style>

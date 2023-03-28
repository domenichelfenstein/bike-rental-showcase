<script setup lang="ts">
import {useRoute, useRouter} from "vue-router";
import {ref} from "vue";
import {fetchPost} from "../../Frontend/request";

const route = useRoute();
const router = useRouter();
const username = ref(route.params.username as string);
const verificationCode = ref("");

const error = ref(undefined);

function changeUserName() {
    router.push(`/registration/verify/${username.value}`);
}

async function verify() {
    const result = await fetchPost<string, any>("registration/verify", { username: username.value, verificationCode: verificationCode.value });
    if(result.ok) {
        await router.push(`/registration/complete/${username.value}/${result.getValue()}`);
    } else {
        error.value = result.getError();
    }
}
</script>

<template>
    <form class="column col-4 col-mx-auto card" @submit.prevent="verify()">
        <div class="card-header">
            <div class="card-title h5">Verify phone number</div>
        </div>
        <div class="card-body">
            <div class="toast toast-error" v-if="error">
                <button class="btn btn-clear float-right" @click="error = undefined"></button>
                Wrong verification code.
            </div>
            <div class="form-group">
                <label class="form-label" for="userName">Username</label>
                <input v-model="username" @change="changeUserName()" class="form-input" type="text" id="userName" required>
            </div>
            <div class="form-group">
                <label class="form-label" for="verificationCode">Verification Code</label>
                <input v-model="verificationCode" class="form-input" type="text" id="verificationCode" placeholder="Just type in Xyz" required>
            </div>
            <div class="card-footer">
                <button class="btn btn-primary" v-if="username" :disabled="!verificationCode">
                    Verify
                </button>
            </div>
        </div>
    </form>
</template>

<script setup lang="ts">
import {ref} from "vue";
import {useRoute, useRouter} from "vue-router";
import {fetchPost} from "../../Frontend/request";

const router = useRouter();
const route = useRoute();
const username = route.params.username as string;
const id = route.params.id as string;

const firstName = ref("");
const lastName = ref("");
const password1 = ref("");
const password2 = ref("");

const error = ref(undefined);

async function complete() {
    const result = await fetchPost<null, any>(
        "registration/complete",
        { username, completionId: id, firstName: firstName.value, lastName: lastName.value, password: password1.value });
    if(result.ok) {
        await router.push("/registration/start");
    } else {
        error.value = result.getError();
    }
}
</script>

<template>
    <form class="column col-4 col-mx-auto card" @submit.prevent="complete()">
        <div class="card-header">
            <h5>Complete Registration</h5>
        </div>
        <div class="card-body">
            <div class="toast toast-error" v-if="error">
                <button class="btn btn-clear float-right" @click="error = undefined"></button>
                Something went wrong: {{error}}
            </div>
            <div class="form-group">
                <label class="form-label" for="userName">Username</label>
                <input v-model="username" class="form-input" type="text" id="userName" disabled>
            </div>
            <div class="form-group">
                <label class="form-label" for="firstName">First Name</label>
                <input v-model="firstName" class="form-input" type="text" id="firstName" required>
            </div>
            <div class="form-group">
                <label class="form-label" for="lastName">Last Name</label>
                <input v-model="lastName" class="form-input" type="text" id="lastName" required>
            </div>
            <div class="form-group">
                <label class="form-label" for="password1">Password</label>
                <input v-model="password1" class="form-input" type="password" id="password1" required>
            </div>
            <div class="form-group">
                <label class="form-label" for="password2">Password Verification</label>
                <input v-model="password2" class="form-input" type="password" id="password2" required>
            </div>
        </div>
        <div class="card-footer">
            <button class="btn btn-primary" :disabled="password1 !== password2 || !password1 || !firstName || !lastName">
                Verify
            </button>
        </div>
    </form>
</template>

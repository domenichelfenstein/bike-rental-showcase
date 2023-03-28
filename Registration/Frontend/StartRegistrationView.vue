<script lang="ts" setup>

import {Ref, ref} from "vue";
import {useRouter} from 'vue-router';
import {fetchPost} from "../../Frontend/request";
import {storeToken} from "../../Frontend/auth";

const router = useRouter();

const loginError = ref(undefined);
const loginUsername = ref("");
const password = ref("");

async function login() {
    const result = await fetchPost<string, any>("registration/login", { username: loginUsername.value, password: password.value });
    if(result.ok) {
        const token = result.getValue();
        storeToken(token);
        await router.push("/");
    } else {
        loginError.value = result.getError();
    }
}

const registrationError: Ref<string | undefined> = ref(undefined);
const registrationUsername = ref("");
const phoneNumber = ref("");

async function register() {
    const result = await fetchPost<null, string>("registration/start", { phoneNumber: phoneNumber.value, username: registrationUsername.value });
    if(result.ok) {
        await router.push(`/registration/verify/${registrationUsername.value}`);
    } else {
        registrationError.value = result.getError();
    }
}
</script>
<template>
    <div class="column col-8 col-mx-auto card">
        <div class="columns card-body">
            <form class="column col-6" @submit.prevent="login()">
                <h5>Login</h5>
                <div class="toast toast-error" v-if="loginError">
                    <button class="btn btn-clear float-right" @click="loginError = undefined"></button>
                    Login error.
                </div>
                <div class="form-group">
                    <label class="form-label" for="username">Username</label>
                    <input v-model="loginUsername" class="form-input" type="text" id="login_username" required>
                </div>
                <div class="form-group">
                    <label class="form-label" for="password">Password</label>
                    <input v-model="password" class="form-input" type="password" id="password" required>
                </div>
                <div class="card-footer">
                    <button class="btn btn-primary" type="submit" :disabled="!loginUsername || !password">
                        Login
                    </button>
                </div>
            </form>
            <div class="divider-vert" data-content="OR"></div>
            <form class="column" @submit.prevent="register()">
                <h5>Registration</h5>
                <div class="toast toast-error" v-if="registrationError">
                    <button class="btn btn-clear float-right" @click="registrationError = undefined"></button>
                    Error registering user: {{ registrationError }}
                </div>
                <div class="form-group">
                    <label class="form-label" for="registration_username">Username</label>
                    <input v-model="registrationUsername" class="form-input" type="text" id="registration_username" required>
                </div>
                <div class="form-group">
                    <label class="form-label" for="phoneNumber">Phone Number</label>
                    <input v-model="phoneNumber" class="form-input" type="tel" id="phoneNumber" required>
                </div>
                <div class="card-footer">
                    <button class="btn btn-primary" type="submit" :disabled="!registrationUsername || !phoneNumber">
                        Register
                    </button>
                </div>
            </form>
        </div>
    </div>
</template>

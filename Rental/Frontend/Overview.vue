<template>
    <div class="toast toast-error" v-if="error">
        <button class="btn btn-clear float-right" @click="error = undefined"></button>
        Error: {{ error}}
    </div>
    <div class="columns">
        <div class="column col-3 col-xl-6 col-sm-12" v-for="bike in bikes">
            <div class="card">
                <div class="card-header">
                    <h5>{{ bike.name }}</h5>
                    <div class="text-gray">{{ bike.manufacturer }}</div>
                </div>
                <img class="img-responsive" :src="bike.base64Image" :alt="bike.name">
                <div class="card-body">
                    <p>{{ renderPrice(bike.price) }}</p>
                </div>
                <div class="card-footer">
                    <button class="btn btn-primary" :disabled="isAvailable(bike)" @click.prevent="rent(bike)">Rent</button>
                    <button class="btn" v-if="bike.status.Case == 'Releasable'" @click.prevent="release(bike)">Release</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import {onMounted, Ref, ref} from "vue";
import {getUserInfo} from "../../Frontend/auth";
import {authGet} from "../../Frontend/request";

const error: Ref<string | undefined> = ref(undefined);
const bikes: Ref<Bike[]> = ref([]);

function renderPrice(value: number) {
    return value.toFixed(2) + " $";
}
function isAvailable(bike: Bike) {
    return bike.status.Case === "Available";
}

function rent(bike: Bike) {

}

function release(bike: Bike) {

}

onMounted(async () => {
    const userInfo = getUserInfo();
    const bikesResult = await authGet<Bike[], string>(`rental/bikes/${userInfo.getValue().userid}`);
    if(bikesResult.ok) {
        bikes.value = bikesResult.getValue();
    }
    else {
        error.value = bikesResult.getError();
    }
});

type AvailabilityStatus = "Bookable" | "NotAvailable" | "Releasable"
type Bike = {
    bikeId: string
    name: string
    manufacturer: string
    price: number
    status: { "Case": AvailabilityStatus, "Fields"?: [string] }
    base64Image: string
}
</script>

<style scoped></style>
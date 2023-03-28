<script setup lang="ts">
import { RouterView } from "vue-router";
</script>

<template>
  <RouterView />
</template>
<style lang="scss">
@import "../node_modules/spectre.css/dist/spectre.min.css";
@import "../node_modules/spectre.css/dist/spectre-exp.min.css";
@import "../node_modules/spectre.css/dist/spectre-icons.min.css";

body {
    height: 100vh;
}

#app {
    display: flex;
    flex-direction: column;
    height: 100vh;

    header {
        flex: 0 0 auto;
        padding: 1rem;
        display: flex;
        flex-direction: row;
        align-items: center;

        a {
            flex: 0 0 auto;
        }

        .placeholder {
            flex: 1 1 auto;
        }

        .dropdown {
            flex: 0 0 auto;
        }
    }

    main {
        flex: 1 1 auto;
        overflow: auto;
    }

    footer {
        flex: 0 0 auto;
        padding: 1rem;
        background: #f7f8f9;
    }
}

// spectre modifications
.card {
    box-shadow: 0 0.25rem 1rem rgb(48 55 66 / 15%);
}

a.card {
    color: #3b4351;
    cursor: pointer;

    &:hover {
        box-shadow: 0 0.25rem 1rem rgb(48 55 66 / 25%);
        color: #3b4351;
        text-decoration: none;
    }
}

</style>

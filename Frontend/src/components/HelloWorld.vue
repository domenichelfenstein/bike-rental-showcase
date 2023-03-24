<script setup lang="ts">
import { ref } from 'vue'

const content = ref('Hello world!');
</script>

<template>
  <h1>{{ content }}</h1>
</template>

<style scoped>
h1 {
  color: #888;
}
</style>

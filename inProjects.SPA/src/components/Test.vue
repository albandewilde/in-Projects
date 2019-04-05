<template>
  <div>
    <h3>TS Counter</h3>
    <div>{{count}}</div>
    <div>
    <button @click="increment()">INCREMENT</button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component 
export default class Counter extends Vue {
  count = 0
  increment() { this.count++ }
}
</script>
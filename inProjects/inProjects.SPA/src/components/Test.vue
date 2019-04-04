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
import{Test, testPost} from '../api/apiTest'

@Component 
export default class Counter extends Vue {
  count = 0
  increment() { 
    this.count++;
    var person = { 
       requestId: this.count, 
    }; 
    Test(this.count);
    testPost(person);
   }
}
</script>
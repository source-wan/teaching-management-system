<template>
  <div class="footerInfo">
    {{this.filing}}
  </div>
</template>

<script>
import record from '../config/record'
export default {
  data(){
    return{
      filing:record,
    }
  }
}
</script>

<style>

</style>
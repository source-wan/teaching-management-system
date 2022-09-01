<template>
  <div class="headerInfo">
    <div class="header_r">
      <div class="headr_d1">
        <side-collapse class="sidecoll" @child-change="countChange"></side-collapse>
        <bread-crumb class="bread"></bread-crumb>
      </div>
      <div class="headr_d2">
        <user-dropdown></user-dropdown>
      </div>
      </div>
  </div>
</template>

<script>
import SideCollapse from '@/components/headre/SideCollapse.vue'
import BreadCrumb from '@/components/headre/BreadCrumb.vue'
import UserDropdown from '@/components/headre/UserDropdown.vue'




export default {
    components: {
    SideCollapse,
    BreadCrumb,
    UserDropdown
  },
  data(){
    return{
      Cookie:false,
      id:'',
      nickName:'',
      imgUrl:'',
    }
  },
  methods:{
        countChange(isCollapse) {
      // console.log(isCollapse);
      this.$emit('child-change', isCollapse);
    },
        getBreadcrumb() {
        return this.$route.matched.filter(
          (item) => item.meta.title
        )
      },
      
  },
}
</script>
<style scoped>
.headr_d1 {
  display: flex;
  justify-content: flex-start;
  width: 600px !important;
  float: left;
  height: 100%;
  padding: 0px 12px;
}
.headerInfo { 
  height: 100%;
  width: 100%;
}
.header_r {
  display: flex;
  align-items: center;
  justify-content: space-between;
  height: 100%;
}
.headr_d2{
  display: flex;
  justify-content: flex-end;
  width: 200px !important;
  height: 60px;
  float: right;
}
</style>
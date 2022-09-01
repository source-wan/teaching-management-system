<template>
  <!-- <el-menu default-active="2" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose"
        background-color="rgb(53,63,81)" text-color="#fff" router collapse active-text-color="#ffd04b">
        <corporation-info></corporation-info>
        <nav-menu-item v-for="menu in menus" :menu="menu" :key="menu.path"></nav-menu-item>
    </el-menu> @open="handleOpen" @close="handleClose" -->
  <div>
    <el-menu
      class="el-menu-vertical-demo"
      :collapse="isCollapse"
      background-color=" rgb(33, 37, 43)"
      text-color="#fff"
      router
      active-text-color="#409EFF"
    >
      <el-menu-item class="home-title" index="/">
        <i class="el-icon-files"></i>
        <span slot="title">教学管理系统</span>
      </el-menu-item>
      <nav-menu-item
        v-for="menu in menus"
        :menu="menu"
        :key="menu.path"
        background-color=" rgb(33, 37, 43)"
      ></nav-menu-item>
    </el-menu>
  </div>
</template>

<script>
import { getToken } from "@/utils/auth";
import NavMenuItem from "./NavMenuItem.vue";
import jwtDecode from "jwt-decode";

export default {
  name: "NavMenu",
  props: ["menus", "isCollapse"],
  components: {
    NavMenuItem,
  },
  data() {
    return {
      role: [],
      check: this.menus,
      //   checkRole: ["teacher", "instructor","admin","student"],
    };
  },
  methods: {},
  created() {
    let token = getToken();
    let decodeToken = jwtDecode(token);
    console.log(decodeToken);

    // let arr = [];
    this.role = (() => {
      for (const key in decodeToken) {
        if (key.endsWith("role")) {
          return decodeToken[key];
        }
      }
    })();
    console.log(this.check);
    console.log(this.role);
    if (this.role.length != 4) {
      if (this.role.includes("teacher") && this.role.includes("instructor")) {
        this.check.shift();
        this.check.pop();
      } else if (this.role.includes("instructor")) {
        this.check.shift();
        this.check.pop();
        this.check.pop();
      }
      if (this.role.includes("student")) {
        this.check.shift();
        this.check.shift();
        this.check.shift();
      }
    }
    // if (this.role.length != 4) {

    // }

    // if (this.role.length != 4) {

    //   if (this.role.findIndex((item) => item != "admin")) {
    //     this.check.splice(0, 1);
    //   }
    //   if (this.role.findIndex((item) => item != "instructor")) {
    //     this.check.splice(1, 1);
    //   }
    //   if (this.role.findIndex((item) => item != "teacher")) {
    //     this.check.splice(2, 1);
    //   }
    //   if (this.role.findIndex((item) => item != "student")) {
    //     this.check.splice(3, 1);
    //   }
    // }
    //   this.role=jwt["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    //   console.log(this.role)
    //   this.id=jwtDecode(token).UserId
    //   this.username=jwtDecode(token).Username
  },
};
</script>

<style>
/* 收起的宽度 */
.el-menu--collapse {
  width: 65px;
}
.el-menu--collapse .el-submenu__title .el-submenu__icon-arrow {
  display: none;
}
.el-menu--collapse .el-submenu__title span {
  display: none;
}
.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 200px;
  /* min-height: calc(100vh - 60px); */
  overflow-y: auto;
  overflow-x: hidden;
  max-height: 100%;
  min-height: 100vh;
}

.el-menu {
  border-right: solid 1px rgb(33, 37, 43);
}
.el-aside {
  overflow: hidden !important;
}

.menuCollapse :not(:first-child) {
  display: none;
}
.home-title > i {
  font-size: 25px !important;
}
.home-title > span {
  font-size: 20px !important;
}
</style>
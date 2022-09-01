<template>
  <div>
    <el-dropdown>
      <!-- <div class="userDrop">
        <span class="userDrop_text"
          >{{ this.nickName }}<i class="el-icon-caret-bottom"></i
        ></span> -->
        <div class="change-icon">
          <i class="el-icon-switch-button"></i>
        </div>
      <!-- </div> -->
      <el-dropdown-menu slot="dropdown">
        <el-dropdown-item @click.native="OutLogin">退出登录</el-dropdown-item>
      </el-dropdown-menu>
    </el-dropdown>
  </div>
</template>

<script>
import {
  clearAllToken,
  // getToken
} from "@/utils/auth";
import appConfig from "@/config/appConfig";
// import jwtDecode from 'jwt-decode'
export default {
  props: ["dropdownInfo"],
  data() {
    return {
      Cookie: false,
      id: "",
      nickName: "",
      imgUrl: "",
      isAdmin: false,
      role: [],
    };
  },
  methods: {
    jumpPersonal() {
      this.$router.push("/article/personal");
    },
    OutLogin() {
      let exit = confirm("确认退出登录?");
      if (exit) {
        clearAllToken();
        location.href = "/login";
      }
    },
    jumpFront() {
      let exit = confirm("确认跳转前台?");
      if (exit) {
        window.location.href = `${appConfig.frontUrl}`;
      }
    },
  },
  created() {
    // let token=getToken()
    // let jwt=jwtDecode(token)
    // console.log(jwt)
    // this.role=jwt["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    // console.log(this.role)
    // this.id=jwtDecode(token).UserId
    // this.username=jwtDecode(token).Username
  },
};
</script>

<style scoped>
.userDrop_text {
  position: relative;
  bottom: 15px;
}
.el-avatar {
  margin: 20px 30px 0px 5px;
}
.el-dropdown {
  width: 200px;
  display: flex;
  align-items: center;
  justify-content: flex-end;
  line-height: 60px;
  height: 100%;
}
.change-icon{
  margin-top: 10%;
  margin-right: 20%;
    font-size:3vh;
}
</style>
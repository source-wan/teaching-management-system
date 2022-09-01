<template>
  <!-- <div class="review-options-details" :style="{ height: clientHeight-194 + 'px' }"> -->
  <div class="box">
    <div class="main-box">
      <el-card class="main-box-card">
        <div class="main-box-left">
          <el-card class="main-box-card-left"> </el-card>
          <div class="father-div">
            <div class="login-div">
              <div class="logo-div">
                <el-image
                  class="el-image"
                  :src="require('@/assets/logo.jpg')"
                  fit="cover"
                ></el-image>
              </div>
              <div class="title-div">
                <span class="sapn-title">欢迎使用教学管理系统</span>
              </div>
              <div class="login-form">
                <el-form
                  :model="ruleForm"
                  status-icon
                  :rules="rules"
                  ref="ruleForm"
                >
                  <el-form-item prop="username">
                    <el-input
                      class="username"
                      placeholder="学号 / 工号"
                      v-model="ruleForm.Account"
                      clearable
                    >
                      <i slot="prefix" class="el-input__icon el-icon-user"></i>
                    </el-input>
                  </el-form-item>
                  <el-form-item prop="password">
                    <el-input
                      class="password"
                      placeholder="密码"
                      v-model="ruleForm.Password"
                      show-password
                    >
                      <i slot="prefix" class="el-input__icon el-icon-lock"></i>
                    </el-input>
                  </el-form-item>
                  <el-form-item style="margin-top: 15px">
                    <el-checkbox v-model="checked">记住我</el-checkbox>
                    <el-link class="pwd-link" :underline="false"
                      >忘记密码</el-link
                    >
                  </el-form-item>
                  <el-form-item>
                    <el-button
                      type="primary"
                      round
                      @click="loginClick('ruleForm')"
                      >登 录</el-button
                    >
                  </el-form-item>
                </el-form>
              </div>
            </div>
          </div>
        </div>
      </el-card>
    </div>
  </div>
</template>

<script>
import { login } from "@/api/Login/Login";
import { setToken } from "@/utils/auth";

export default {
  data() {
    let validateUser = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请输入学号或工号"));
      } else {
        callback();
      }
      //  else if (!(/^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/).test(value)) {
      //     callback(new Error('学号或工号的格式不对'))
      // }
    };
    let validatePass = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请输入密码"));
      } else {
        callback();
      }
    };
    return {
      radio: 0,
      checked: false,
      ruleForm: {
        Account: "",
        Password: "",
      },
      rules: {
        Account: [{ validator: validateUser, trigger: "blur" }],
        Password: [{ validator: validatePass, trigger: "blur" }],
      },
    };
  },
  methods: {
    loginClick(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          console.log(this.ruleForm);
          login(this.ruleForm).then((data) => {
            console.log(data);
            if ((data.code == 1000)) {
              setToken(data.data.accessToken,data.data.refreshToken)
               this.$message.success("登录成功");
              this.$router.push('/')
            }else{
          this.$message.error('账号或者密码错误');
            }
          });
        } else {
          return false;
        }
      });
    },
  },
};
</script>

<style scoped>
.box {
  /* background: url(@/assets/1.jpg); */
  /* background: #60b4e1; */
  /* fallback for old browsers */
  /* background: -webkit-linear-gradient(to right,
            rgb(81, 174, 228),
            rgb(242, 252, 254)); */
  /* Chrome 10-25, Safari 5.1-6 */
  /* background: linear-gradient(to right,
            rgb(107, 185, 227),
            rgb(242, 252, 254)); */
  /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
  width: 100%;
  height: 100%;
}
.logo {
  background: url(@/assets/logo.jpg);
}
.el-card {
  padding: 0px;
  margin: 0px;
}

.el-card >>> .el-card__body {
  padding: 0;
}

.main-box {
  display: flex;
  justify-content: center;
  align-items: center;
  padding-top: 80px;
}

.main-box-card {
  padding: 0px;
  margin: 0px;
  width: 60%;
  height: 80vh;
  min-width: 380px;
  margin-right: 20px;
  transition: all 0.5s;

  background: rgba(255, 255, 255, 0.5);
  /*背景颜色为白色，透明度为0.8*/
  box-sizing: border-box;
  /*box-sizing设置盒子模型的解析模式为怪异盒模型，
    将border和padding划归到width范围内*/
  box-shadow: -16px -16px 32px #7badca, 16px 16px 32px #abefff;
  /*边框阴影  水平阴影0 垂直阴影15px 模糊25px 颜色黑色透明度0.5*/
  border-radius: 5px;
  /*边框圆角，四个角均为5px*/
}

.main-box-card-left {
  float: left;
  width: 50%;
  height: 80vh;
  background: url(@/assets/2.jpg);
  background-size: auto;
  background-position: center center;
  /* 背景图不平铺 */
  background-repeat: no-repeat;
  /* 当内容高度大于图片高度时，背景图像的位置相对于viewport固定 */
  /* background-attachment: fixed; */
  /* 让背景图基于容器大小伸缩 */
  background-size: cover;
  /* 设置背景颜色，背景图加载过程中会显示背景色 */
  background-color: #464646;
}

.main-box-right {
  padding-top: 15%;
}

.main-box-card-right {
  background: rgba(255, 255, 255, 0.5);
  /*背景颜色为白色，透明度为0.8*/
  box-sizing: border-box;
  /*box-sizing设置盒子模型的解析模式为怪异盒模型，
    将border和padding划归到width范围内*/
  box-shadow: -9px -9px 10px #96c0d9, 9px 9px 10px #bcf0ff;
  /*边框阴影  水平阴影0 垂直阴影15px 模糊25px 颜色黑色透明度0.5*/
  border-radius: 5px;
  /*边框圆角，四个角均为15px*/
}

.main-box-card:hover {
  margin-top: -5px;
}

h1 {
  text-align: center;
  font-family: STKaiti;
  padding-top: 10px;
  padding-right: 20%;
}

.button {
  margin-left: 20%;
}

.input {
  width: 70%;
}

.user-input,
.password-input {
  padding-left: 15%;
}

.input,
.el-input--medium,
.el-input__inner {
  border-top: none !important;
  border-left: none !important;
  border-right: none !important;
  border-radius: 0;
}
.father-div {
  margin: 0 auto;
  display: flex;
}

.login-div {
  width: 500px;
  height: 600px;
  /* box-shadow: 0 2px 12px 0; */
  border-radius: 10px;
  margin: 0 auto;
  margin-top: 15%;
  background: white;
}

.logo-div {
  width: 280px;
  margin: 0 auto;

  margin-bottom: 10px;
  /* border-bottom: 2px solid #C0C4CC; */
}

.el-image {
  margin-top: 10px;
}

.title-div {
  width: 450px;
  display: flex;
  justify-content: center;
  margin: 0 auto;
  font-weight: bold;
  font-size: 30px;
  color: #409eff;
}

.sapn-title {
  margin-top: 20px;
  margin-bottom: 5px;
}

/* .role-radio {
    width: 400px;
    height: 35px;
    text-align: center;
    margin: 0 auto;
    margin-top: 10px;
    margin-bottom: 10px;
} */

.login-form {
  display: flex;
  justify-content: center;
  margin-top: 20px;
}

.el-input {
  width: 350px;
  height: 45px;
  margin-top: 30px;
}

.username >>> .el-input__inner {
  border-radius: 1px;
  border: none;
  border-bottom: 2px solid #c0c4cc;
}

.password >>> .el-input__inner {
  border-radius: 1px;
  border: none;
  border-bottom: 2px solid #c0c4cc;
}

.username >>> .el-input__inner:focus {
  border-color: #409eff;
}

.password >>> .el-input__inner:focus {
  border-color: #409eff;
}

.el-button {
  width: 350px;
  height: 48px;
  border-radius: 30px;
  margin-top: 20px;
}

.el-input__icon {
  font-size: 17px;
}

.el-form-item {
  margin-bottom: 2px;
}

.el-checkbox {
  margin-left: 0.5px;
}

.pwd-link {
  margin-left: 228px;
  margin-bottom: 5px;
}

.username,
.password {
  display: flex;
  align-items: center;
}

.el-form-item.is-error .username >>> .el-input__inner {
  border-color: #f56c6c;
}

.el-form-item.is-error .password >>> .el-input__inner {
  border-color: #f56c6c;
}

.el-form-item.is-success .username >>> .el-input__inner {
  border-color: #67c23a;
}

.el-form-item.is-success .password >>> .el-input__inner {
  border-color: #67c23a;
}

.el-form-item.is-success .username >>> .el-input__validateIcon {
  color: #67c23a !important;
}

.el-form-item.is-success .password >>> .el-input__validateIcon {
  color: #67c23a !important;
}
</style>
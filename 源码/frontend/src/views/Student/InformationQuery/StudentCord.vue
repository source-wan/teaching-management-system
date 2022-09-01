<template>
  <div class="form">
    <div class="form-top">
      <div class="form">
        <div class="form-top">
          <el-form :model="form">
            <div class="form-left">
              <el-form-item label="姓名：">
                <el-input
                  v-model="form.name"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>
              <el-form-item label="学号：">
                <el-input
                  disabled
                  v-model="form.studentCode"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>

              <el-form-item label="身份证号：">
                <el-input
                  v-model="form.identityCode"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>
              <el-form-item label="父亲姓名：">
                <el-input
                  v-model="form.fatherName"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>
              <el-form-item label="母亲姓名：">
                <el-input
                  v-model="form.motherName"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>

              <el-form-item label="QQ号：">
                <el-input
                  v-model="form.qq"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>

              <el-form-item label="家庭住址：">
                <el-input
                  v-model="form.address"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>
            </div>

            <div class="form-right">
              <el-form-item label="性别：">
                <el-select v-model="form.gender" style="width: 150px" disabled>
                  <el-option label="男" value="false"></el-option>
                  <el-option label="女" value="true"></el-option>
                </el-select>
              </el-form-item>
              <el-form-item label="手机号：">
                <el-input
                  v-model="form.phone"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>
              <el-form-item label="邮箱:">
                <el-input
                  v-model="form.mail"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>
              <el-form-item label="父亲联系方式：">
                <el-input
                  v-model="form.fatherPhone"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>

              <el-form-item label="母亲联系方式：">
                <el-input
                  v-model="form.motherPhone"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>
              <el-form-item width="100px" label="微信号：">
                <el-input
                  v-model="form.weChat"
                  autocomplete="off"
                  class="label"
                ></el-input>
              </el-form-item>
            </div>
          </el-form>
          <div slot="footer" class="dialog-footer">
            <el-button type="primary" @click="handleEdit()">确 定</el-button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import jwtDecode from "jwt-decode";
import { getToken } from "@/utils/auth";
import { findStudent, putStudent } from "@/api/Student/student";

export default {
  data() {
    return {
      UserId: "",
      form: {
        id: "",
        name: "",
        identityCode: "",
        studentCode: "",
        phone: "",
        address: "",
        gender: null,
        mail: "",
        qq: "",
        weChat: "",
        fatherName: "",
        fatherPhone: "",
        motherName: "",
        motherPhone: "",
      },
    };
  },
  methods: {
    async init() {},
    async handleEdit() {
      let model = {
        name: this.form.name,
        identityCode: this.form.identityCode,
        phone: this.form.phone,
        address: this.form.address,
        mail: this.form.mail,
        qq: this.form.qq,
        weChat: this.form.weChat,
        fatherName: this.form.fatherName,
        fatherPhone: this.form.fatherPhone,
        motherName: this.form.motherName,
        montherPhone: this.form.motherPhone,
      };
      console.log(this.form.motherPhone);
      let put = (await putStudent(this.form.id, model)).data;
      console.log(put);
      this.$message.success("修改成功");
    },
  },
  async created() {
    let token = getToken();
    let decodeToken = jwtDecode(token);
    console.log(decodeToken);
    this.UserId = decodeToken.UserId;
    let student = (await findStudent(this.UserId)).data;
    this.form.id = student.data[0].id;
    this.form.name = student.data[0].name;
    this.form.identityCode = student.data[0].identityCode;
    this.form.studentCode = student.data[0].studentCode;
    this.form.phone = student.data[0].phone;
    this.form.address = student.data[0].address;
    this.form.gender = student.data[0].gender.toString();
    this.form.mail = student.data[0].mail;
    this.form.qq = student.data[0].qq;
    this.form.weChat = student.data[0].weChat;
    this.form.fatherName = student.data[0].fatherName;
    this.form.fatherPhone = student.data[0].fatherPhone;
    this.form.motherName = student.data[0].motherName;
    this.form.motherPhone = student.data[0].montherPhone;
    console.log(this.form.id);
  },
};
</script>

<style scoped>
.form {
  width: 100%;
  height: 100%;
}

.form-top {
  width: 100%;
  height: 100%;
}

.form-right {
  margin: 40px 0 0 0;
  width: 40%;
  float: right;
  height: 80%;
  /* margin-left:0; */
}
/* 基本信息按钮 */
.dialog-footer {
  margin-left: 60%;
  padding-top: 32%;
}

.form-left {
  float: left;
  width: 40%;
  height: 80%;
  margin-left: 5%;
}

/*基本信息显示框的长度  */
.a {
  width: 80%;
}
.input-with-select .el-input-group__prepend {
  background-color: #fff;
}
.input-with-select {
  width: 400px;
}
.el-form-item {
  margin-bottom: 0px;
}
</style>
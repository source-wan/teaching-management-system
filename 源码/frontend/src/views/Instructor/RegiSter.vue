<template>
  <div>
    <div style="margin-top: 15px">
      <el-input placeholder="请输入内容" v-model="input3" class="input-with-select">
        <el-select v-model="select" slot="prepend" placeholder="请选择">
          <el-option label="班级查找" value="1"></el-option>
          <el-option label="姓名查找" value="2"></el-option>
        </el-select>
        <el-button slot="append" icon="el-icon-search"></el-button>
      </el-input>
    </div>

    <el-table :data="tableData" style="width: 100%">
      <el-table-column prop="number" label="序号" width="120px">
      </el-table-column>
      <el-table-column prop="name" label="姓名" width="120px">
      </el-table-column>
      <el-table-column prop="sex" label="性别" width="120px"> </el-table-column>
      <el-table-column prop="id" label="学号"> </el-table-column>
      <el-table-column prop="car" label="身份证号"> </el-table-column>
      <el-table-column prop="phone" label="手机号"> </el-table-column>
      <el-table-column prop="wechart" label="微信号"> </el-table-column>
      <el-table-column prop="caozuo" label="操作">
        <el-button type="text" @click="dialogFormVisible = true">查看更多</el-button>
        <el-dialog width="64%" title="基本信息" :visible.sync="dialogFormVisible" :append-to-body="true">
          <div class="form">
            <div class="form-top">
              <el-form :model="form">
              
                <div class="aaa">
                  <el-form-item label="姓名：" :label-width="formLabelWidth">
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item label="性别：" :label-width="formLabelWidth">
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item label="学号：" :label-width="formLabelWidth">
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item
                    label="身份证号："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item
                    label="家庭住址："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item label="手机号：" :label-width="formLabelWidth">
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>
                </div>

                <div class="bbb">
                  <el-form-item
                    width="100px"
                    label="微信号："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item label="QQ号：" :label-width="formLabelWidth">
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item
                    label="父亲姓名："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item
                    label="父亲联系方式："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item
                    label="母亲姓名："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>

                  <el-form-item
                    label="母亲联系方式："
                    :label-width="formLabelWidth"
                  >
                    <el-input
                      v-model="form.name"
                      autocomplete="off"
                      class="a"
                    ></el-input>
                  </el-form-item>
                </div>
              </el-form>
            </div>
            <div slot="footer" class="dialog-footer">
              <el-button type="primary" @click="dialogFormVisible = false">确 定</el-button>
              <el-button @click="dialogFormVisible = false">取 消</el-button>
            </div>
          </div>
        </el-dialog>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
export default {
  data() {
    return {
      input3: "",
      select: "",
      tableData: [
        {
          number: "1",
          name: "王小虎",
          sex: "男",
          id: "20440233235",
          car: "15456215646541",
          phone: "15541234",
          wechart: "111",
        },
        {
          number: "1",
          name: "王小虎",
          sex: "男",
          id: "20440233235",
          car: "15456215646541",
          phone: "15541234",
          wechart: "111",
        },
        {
          number: "1",
          name: "王小虎",
          sex: "男",
          id: "20440233235",
          car: "15456215646541",
          phone: "15541234",
          wechart: "111",
        },
        {
          number: "1",
          name: "王小虎",
          sex: "男",
          id: "20440233235",
          car: "15456215646541",
          phone: "15541234",
          wechart: "111",
        },
      ],
      dialogTableVisible: false,
      dialogFormVisible: false,
      form: {
        name: "",
        region: "",
        date1: "",
        date2: "",
        delivery: false,
        type: [],
        resource: "",
        desc: "",
      },
      formLabelWidth: "120px",
    };
  },
};
</script>
<style scoped>
.form {
  width: 100%;
  height: 100%;
}

.form-top{
  width:100%;
  height: 100%;
}

.bbb {
  width: 50%;
  float: right;
  height: 80%;
}
/* 基本信息按钮 */
.dialog-footer{
  margin-left: 60%;
}

.aaa {
  float: left;
  width: 50%;
  height: 80%;
}

/*基本信息显示框的长度  */
.a {
  width: 80%;
}
/* 选择、搜索框 */
.el-select .el-input {
  width: 130px;
}
.input-with-select .el-input-group__prepend {
  background-color: #fff;
}
.input-with-select {
  width: 400px;
}
</style>



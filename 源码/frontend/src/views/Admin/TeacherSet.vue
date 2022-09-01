<template>
  <div class="table">
    <template>
      <div class="table-top">
        <div class="search">
          <el-input
            placeholder="请输入内容"
            v-model="state"
            class="input-with-select"
            style="width: 20%"
          >
          </el-input>
        </div>
        <div class="button-top">
          <el-button type="primary" icon="el-icon-plus" @click="handleAdd">
          </el-button>
          <el-button
            type="primary"
            icon="el-icon-refresh"
            @click="handleRefresh"
          >
          </el-button>
        </div>
        <el-dialog
          title="新增教师"
          :visible.sync="dialogFormVisible"
          :append-to-body="true"
          class="dialog"
        >
          <div class="dialog-main">
            <el-form ref="form" label-width="70px">
              <!-- <el-form-item label="工号" style="width: 60vh">
                <el-input v-model="form.workCode"></el-input>
              </el-form-item> -->
              <el-form-item label="教师名称" style="width: 60vh">
                <el-input v-model="form.name"></el-input>
              </el-form-item>
              <el-form-item label="性别" style="width: 60vh">
                <el-select
                  v-model="form.gender"
                  placeholder="选择性别"
                  filterable
                >
                  <el-option
                    v-for="item in options"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  >
                  </el-option>
                </el-select>
              </el-form-item>
              <el-form-item label="身份证" style="width: 60vh">
                <el-input v-model="form.IdentityCode"></el-input>
              </el-form-item>
              <el-form-item label="学院" style="width: 60vh">
                <el-select
                  v-model="form.AcademyId"
                  placeholder="选择学院"
                  filterable
                >
                  <el-option
                    v-for="item in firstOptions"
                    :key="item.id"
                    :label="item.academyName"
                    :value="item.id"
                  >
                  </el-option>
                </el-select>
              </el-form-item>
              <el-button
                @click="addTeacher"
                type="primary"
                class="notarizeButton"
                >确认</el-button
              >
            </el-form>
          </div>
        </el-dialog>
      </div>
      <el-table
        :data="
          tableData.filter(
            (data) =>
              !state ||
              data.name.toLowerCase().includes(state.toLowerCase()) ||
              data.workCode.toLowerCase().includes(state.toLowerCase())
          )
        "
        style="width: 100%"
      >
        <el-table-column label="序号" width="200px" type="index" align="center">
        </el-table-column>
        <el-table-column label="工号" prop="workCode" align="center">
        </el-table-column>
        <el-table-column label="教师名称" prop="name" align="center">
        </el-table-column>
        <!-- <el-table-column label="性别" prop="gender" align="center">
        </el-table-column>
        <el-table-column label="电话" prop="phone" align="center">
        </el-table-column>
        <el-table-column label="邮箱" prop="mail" align="center">
        </el-table-column> -->

        <el-table-column label="操作" align="center">
          <template slot-scope="scope">
            <!-- <el-button
              size="mini"
              type="danger"
              @click="handleEdit(scope.$index, scope.row)"
              >编辑</el-button
            > -->
            <el-button
              size="mini"
              type="danger"
              @click="handleDelete(scope.$index, scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
    </template>
  </div>
</template>

<script>
import { getTeacher, postTeacher } from "@/api/Admin/Teacher";
import { getAcademy } from "@/api/Admin/College";
export default {
  data() {
    return {
      state: "",
      dialogFormVisible: false,
      direction: "rtl",
      tableData: [],
      firstOptions: [],
      form: {
        name: "",
        gender: "",
        phone: "",
        mail: "",
        IdentityCode:'',
        AcademyId: "",
      },
      options: [
        {
          value: true,
          label: "男",
        },
        {
          value: false,
          label: "女",
        },
      ],
    };
  },
  methods: {
    async handleRefresh() {
       let getTeacherList = (await getTeacher()).data;
           this.tableData = getTeacherList.data;
           console.log(getTeacherList)
    },
    //添加
    async addTeacher() {
      let model = {
        Name: this.form.name,
        Gender: this.form.gender,
        AcademyId:this.form.AcademyId,
        IdentityCode:this.form.IdentityCode
      };
      console.log(model)
      let postTeacherList = (await postTeacher(model)).data;
      console.log(postTeacherList)
      // postAcademy(model).then((data) => {
      //   console.log(data);
      // });
      // this.drawer = false;
      //handleRefresh();
    },
    //序号
    indexMethod(index) {
      return index + 1;
    },
    handleClose(done) {
      this.$confirm("确认关闭？")
        // eslint-disable-next-line
        .then((_) => {
          done();
        })
        // eslint-disable-next-line
        .catch((_) => {});
    },
    //删除
    async handleDelete(index, row) {
      let delTeacher=(await delTeacher(row.id)).data
      console.log(delTeacher)
    },
    handleAdd() {
      this.dialogFormVisible = true;
    },
  },
  async created() {
    let getTeacherList = (await getTeacher()).data;
    let getCollegeList=(await getAcademy()).data
    this.firstOptions=getCollegeList.data
    this.tableData = getTeacherList.data;
  },
};
</script>

<style>
.button {
  margin-right: 50%;
}

.el-input__inner {
  width: 25vh;
}
.button-top {
  margin-left: 5px;
}
.notarizeButton {
  margin-left: 25vh;
}
.search {
  float: left;
}
.dialog {
  width: 100vh;
  margin-left: 26%;
  margin-top: 4%;
}
.dialog-main {
  margin-left: 5vh;
}
</style>
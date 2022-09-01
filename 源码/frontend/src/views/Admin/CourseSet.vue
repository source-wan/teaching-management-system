<template slot-scope="scope">
  <div class="table">
    <div class="table-top">
      <div class="top-input">
        <el-input
          placeholder="请输入内容"
          v-model="input3"
          class="input-with-select"
        >
          <el-button
            slot="append"
            icon="el-icon-search"
            @click="search()"
            
          ></el-button>
        </el-input>
      </div>
      <div class="top-button">
        <el-button
          type="primary"
          icon="el-icon-plus"
          circle
          @click="handleAdd = true"
        ></el-button>
        <el-drawer
          title="新增课程"
          :visible.sync="handleAdd"
          :with-header="false"
          :before-close="handleClose"
          direction="rtl"
          custom-class="demo-drawer"
          ref="drawer"
        >
          <div class="demo-drawer__content">
            <h1>新增课程</h1>
            <el-form :model="form" style="width: 400px">
              <el-form-item label="课程名称" :label-width="formLabelWidth">
                <el-input
                  v-model="form.courseName"
                  autocomplete="off"
                ></el-input>
              </el-form-item>
              <el-form-item label="学分" :label-width="formLabelWidth">
                <el-input v-model="form.credit" autocomplete="off"></el-input>
              </el-form-item>
              <el-form-item label="学时" :label-width="formLabelWidth">
                <el-input v-model="form.period" autocomplete="off"></el-input>
              </el-form-item>
              <!-- <el-form-item label="学期" :label-width="formLabelWidth">
                <el-input v-model="form.semester" autocomplete="off"></el-input>
              </el-form-item> -->
            </el-form>
            <div class="demo-drawer__footer">
              <el-button @click="cancelForm">取 消</el-button>
              <el-button
                type="primary"
                @click="$refs.drawer.closeDrawer()"
                :loading="loading"
                :plain="true"
                >{{ loading ? "提交中 ..." : "确 定" }}</el-button
              >
            </div>
          </div>
        </el-drawer>
        <el-button
          type="primary"
          circle
          icon="el-icon-refresh"
          @click="search()"
        ></el-button>
      </div>
    </div>
    <div class="table-main">
      <el-table :data="tableData" border style="width: 100%" align="center">
        <el-table-column
          type="index"
          :index="indexMethod"
          width="80px"
          align="center"
          fixed
          laber="序号"
        ></el-table-column>

        <el-table-column prop="courseName" label="课程" align="center">
          <template slot-scope="scope">
            <el-input
              v-show="scope.row.isEdit"
              v-model="scope.row.courseName"
            ></el-input>
            <span v-show="!scope.row.isEdit">
              {{ scope.row.courseName }}
            </span>
          </template>
        </el-table-column>

        <el-table-column prop="credit" label="学分" align="center">
          <template slot-scope="scope">
            <el-input
              v-show="scope.row.isEdit"
              v-model="scope.row.credit"
            ></el-input>
            <span v-show="!scope.row.isEdit">
              {{ scope.row.credit }}
            </span>
          </template>
        </el-table-column>
        <el-table-column prop="period" label="学时" align="center">
          <template slot-scope="scope">
            <el-input
              v-show="scope.row.isEdit"
              v-model="scope.row.period"
            ></el-input>
            <span v-show="!scope.row.isEdit">
              {{ scope.row.period }}
            </span>
          </template>
        </el-table-column>

        <el-table-column label="操作" align="center">
          <template slot-scope="scope">
            <el-button
              icon="el-icon-edit"
              size="large"
              v-show="!scope.row.editButton"
              type="primary"
              @click="handleEdit(scope.$index, scope.row)"
              >编辑</el-button
            >
            <el-button
              icon="el-icon-circle-check"
              size="large"
              v-show="scope.row.editButton"
              @click="confirm(scope.$index, scope.row)"
              >确认</el-button
            >
            <el-button
              size="large"
              type="danger"
              slot="reference"
              icon="el-icon-delete el-icon--left"
              v-show="!scope.row.cancelButton"
              @click="handleDelete(scope.$index, scope.row)"
              >删除</el-button
            >
            <el-button
              icon="el-icon-circle-close el-icon--left"
              size="large"
              v-show="scope.row.editButton"
              @click="cancel(scope.$index, scope.row)"
              >取消
            </el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<script>
import {
  getCourse,
  putCourse,
  postCourse,
  delCourse,
} from "@/api/Admin/Course";
export default {
  data() {
    return {
      cancelButton: false,
      editButton: false,
      loading: false,
      handleAdd: false,
      tableData: [],
      input1: "",
      input2: "",
      input3: "",
      select: "",
      form: {
        courseName: "",
        credit: "",
        period: "",
        semester: "",
      },
      formLabelWidth: "80px",
      timer: null,
    };
  },
  async created() {
    let data = (await getCourse(this.input3)).data;
    console.log(data.data)
    for (let course of data.data) {
      course.isEdit = false;
      this.tableData.push(course);
    }
    console.log(this.tableData);
  },
  methods: {
    async search() {

      let data = (await getCourse(this.input3)).data;
      let arr = [];
      for (let course of data.data) {
        course.isEdit = false;
        arr.push(course);
      }
      this.tableData = arr;
    },
    // 编辑按钮
    handleDelete(index, row) {
      delCourse(row.id).then((data) => {
        if (data.code == 1000) {
          this.$message.success("删除成功");
        } else {
          this.$message.error("删除失败");
        }
      });
    },
    handleEdit(index, row) {
      console.log(index);
      row.isEdit = true;
      row.editButton = true;
      row.cancelButton = true;
    },
    //确认按钮
    confirm(index, row) {
      let model = {
        courseName: row.courseName,
        credit: row.credit,
        period: row.period,
      };
      putCourse(row.id, model).then((data) => {
        console.log(data);
      });
      row.isEdit = false;
      row.editButton = false;
      row.cancelButton = false;
    },
    //取消按钮
    cancel(index, row) {
      row.isEdit = false;
      row.editButton = false;
      row.cancelButton = false;
    },
    indexMethod(index) {
      return index + 1;
    },
    handleClose(done) {
      if (this.loading) {
        return;
      }
      this.$confirm("确定要提交表单吗？")
        // eslint-disable-next-line
        .then((_) => {
          let model = {
            courseName: this.form.courseName,
            credit: this.form.credit,
            period: this.form.period,
          };
          postCourse(model).then((data) => {
            console.log(data);
          });
          this.loading = true;
          this.timer = setTimeout(() => {
            done();
            // 动画关闭需要一定的时间
            setTimeout(() => {
              this.loading = false;
            }, 400);
          }, 500);
          this.$message({
            showClose: true,
            message: "添加课程信息成功！",
            type: "success",
          });
        })
        // eslint-disable-next-line
        .catch((_) => {});
    },
    cancelForm() {
      this.loading = false;
      this.handleAdd = false;
      clearTimeout(this.timer);
    },
  },
};
</script>
<style scoped>
.top-input {
  width: 540px;
}
.el-select .el-input {
  width: 110px;
}
.input-with-select .el-input-group__prepend {
  background-color: #fff;
}
.top-button {
  float: right;
}
.top-input {
  float: left;
}
.el-button {
  margin-left: 10px;
}
.el-icon-search {
  margin: auto;
}
</style>>

  <template>
  <div>
    <el-card class="box-card">
      <el-card class="top-card">
        <div class="search-box">
          <el-select
            v-model="data.courseId"
            placeholder="请选择课程"
            filterable
            @change="coursechange"
          >
            <el-option
              v-for="(item, index) in courseOptions"
              :key="index"
              :label="item.courseName"
              :value="item.id"
            >
            </el-option>
          </el-select>
          <el-select
            v-model="select"
            slot="prepend"
            placeholder="学期"
            @change="handlechange"
          >
            <el-option label="大一上学期" value="1"></el-option>
            <el-option label="大一下学期" value="2"></el-option>
            <el-option label="大二上学期" value="3"></el-option>
            <el-option label="大二下学期" value="4"></el-option>
            <el-option label="大三上学期" value="5"></el-option>
            <el-option label="大三下学期" value="6"></el-option>
          </el-select>
        </div>
        <div class="top-button">
          <el-button
            type="primary"
            circle
            icon="el-icon-refresh"
            @click="handleRefresh"
          ></el-button>
        </div>
      </el-card>
      <el-table :data="tableData" style="width: 100%">
        <el-table-column
          fixed
          label="序号"
          align="center"
          type="index"
        ></el-table-column>
        <el-table-column
          fixed
          prop="term"
          label="学期"
          align="center"
        ></el-table-column>
        <el-table-column
          fixed
          prop="className"
          label="班级"
          align="center"
        ></el-table-column>
        <el-table-column fixed label="操作" align="center">
          <template slot-scope="scope">
            <el-button type="primary" @click="check(scope.row)">查看</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>
    <el-drawer
      size="60%"
      style="width: 100%"
      :title="className"
      :visible.sync="drawer"
      :direction="direction"
    >
      <div class="upload">
        <el-upload
          action
          accept=".xlsx, .xls"
          :auto-upload="false"
          :show-file-list="false"
          :on-change="importExcel"
        >
          <el-button
            type="primary"
            icon="el-icon-cloudy"
            slot="trigger"
            class="iexcel"
            >导入数据</el-button
          >
        </el-upload>
      </div>
      <div class="upload2">
        <el-button
          type="primary"
          icon="el-icon-cloudy"
          @click="exportExcel"
          class="eexcel"
          >导出数据</el-button
        >
      </div>
      <el-table :data="studentData" style="width: 100%">
        <el-table-column
          fixed
          label="序号"
          align="center"
          type="index"
        ></el-table-column>
        <el-table-column
          fixed
          prop="studentCode"
          label="学号"
          align="center"
        ></el-table-column>
        <el-table-column fixed prop="name" label="学生姓名" align="center">
        </el-table-column>
        <!-- <el-table-column fixed label="成绩" align="center">
          {{ this.studentData.score }}
        </el-table-column> -->
      </el-table>
    </el-drawer>
  </div>
</template>

<script>
import xlsx from "xlsx";
import jwtDecode from "jwt-decode";
import { getToken } from "@/utils/auth";
import {
  getTeacher,
  getClassCourse,
  getCourse,
  getClass,
  getClassstudent,
  postScore,
} from "@/api/Teacher/Score";
import { handleExcel } from "@/utils/excel";
export default {
  data() {
    return {
      classId: "",
      courseId: "",
      term: "",
      data: {
        teacherId: "",
        userId: "",
        CourseId: "",
      },
      courseOptions: [],
      select: "",
      studentData: [
        // { studentId: 1, studentName: "王小虎", score: 50 },
      ],
      tableData: [],
      className: "",
      drawer: false,
      pageIndex: 0,
      pageSize: 5,
      total: 0,
      upFile: "",
      dialogFormVisible: false,
      direction: "rtl",
    };
  },
  methods: {
    async handleRefresh() {
      let Course = (await getClassCourse(this.data.teacherId)).data;
      for (const item of Course.data) {
        let classList = (await getClass(item.classId)).data;
        item.className = classList.data[0].className;
      }
      this.tableData = Course.data;
    },
    async coursechange(val) {
      let Course = (await getClassCourse(this.data.teacherId)).data;
      for (const item of Course.data) {
        let classList = (await getClass(item.classId)).data;
        item.className = classList.data[0].className;
      }
      this.tableData = Course.data;
      for (let i = 0; i < this.tableData.length; i++) {
        if (this.tableData[i].courseId !== val) {
          this.tableData.splice(i, 1);
        }
      }
    },
    async handlechange(val) {
      let Course = (await getClassCourse(this.data.teacherId)).data;
      for (const item of Course.data) {
        let classList = (await getClass(item.classId)).data;
        item.className = classList.data[0].className;
      }
      this.tableData = Course.data;
      for (let i = 0; i < this.tableData.length; i++) {
        if (this.tableData[i].term / 1 !== val / 1) {
          this.tableData.splice(i, 1);
        }
      }
    },

    async importExcel(files) {
      let file = files.raw; // files.raw 为上传的文件数据
      if (file.length <= 0) {
        // 如果没有文件名
        return false;
      } else if (!/\.(xls|xlsx)$/.test(file.name.toLowerCase())) {
        this.$message.error("上传格式不正确，请上传xls或者xlsx格式");
        return false;
      }
      let data = await handleExcel(file);
      let arr = [];
      for (const item of data) {
        let a = {};
        a.CourseId = this.courseId;
        a.StudentId = item.StudentId;
        a.Score = item.Score;
        a.Term = this.term;
        arr.push(a);
      }
      let Score = await postScore(this.classId, arr);
      if (Score.code == 3003) {
        this.$message.error("成绩传入失败");
      } else {
        this.$message.success("成绩传入成功");
      }
      console.log(Score);
    },
    exportExcel() {
      let arr = this.studentData.map((item) => {
        return {
          StudentId: item.id,
          StudentName: item.name,
          Score: item.score,
        };
      });
      let sheet = xlsx.utils.json_to_sheet(arr);
      let book = xlsx.utils.book_new();
      xlsx.utils.book_append_sheet(book, sheet, `${this.className}`);
      xlsx.writeFile(book, `${this.className}- 成绩录入表.xlsx`);
    },
    async check(row) {
      this.drawer = true;
      this.className = row.className;
      this.classId = row.classId;
      this.term = row.term;
      this.courseId = row.courseId;
      let classStudent = (await getClassstudent(row.classId)).data;
      console.log(classStudent.data);
      this.studentData = classStudent.data;
      console.log(row);
    },
    changeScore(index, row) {
      console.log(index, row);
    },
    testData(tabData) {
      console.log(tabData);
      this.tableData = tabData;
    },
    handleSizeChange(val) {
      this.pageSize = val;
    },
    handleCurrentChange(val) {
      this.pageIndex = val;
    },
    // 上传成功事件
    uploadSuccess(response) {
      console.log(response);
      if (response.stat == 1) {
        this.$message({
          message: response.msg,
          type: "success",
        });
        this.$router.back(); //这里我是单独一个页面的上传组件，成功后返回上一个页面
      } else {
        this.$message({
          message: response.msg,
          type: "error",
        });
      }
    },
    // 上传失败时的钩子
    uploadFalse() {
      this.$message({
        message: "excel文件上传失败！",
        type: "error",
      });
    },
    // 上传前对文件的大小的判断
    beforeAvatarUpload(file) {
      const extension = file.name.split(".")[1] === "xls";
      const extension2 = file.name.split(".")[1] === "xlsx";
      const isLt10M = file.size / 1024 / 1024 < 10;
      if (!extension && !extension2) {
        this.$message({
          message: "上传模板只能是 xls、xlsx格式!",
          type: "error",
        });
      }
      if (!isLt10M) {
        console.log("上传模板大小不能超过 10MB!");
        this.$message({
          message: "上传模板大小不能超过 10MB!",
          type: "error",
        });
      }
      return extension || extension2 || isLt10M;
    },
  },
  async created() {
    let token = getToken();
    let decodeToken = jwtDecode(token);
    this.data.userId = decodeToken.UserId;
    let Teacher = (await getTeacher(this.data.userId)).data;
    this.data.teacherId = Teacher.data[0].id;
    let Course = (await getClassCourse(this.data.teacherId)).data;
    for (const item of Course.data) {
      let classList = (await getClass(item.classId)).data;
      item.className = classList.data[0].className;
    }
    this.tableData = Course.data;
    console.log(this.tableData);
    for (const item of Course.data) {
      let Course = (await getCourse(item.courseId)).data;
      this.courseOptions.push(Course.data[0]);
    }
  },
};
</script>

<style scoped>
.Edit-button {
  margin-right: 25px;
}
.search-box {
  display: flex;
  width: auto;
  height: 40px;
  float: left;
}
.top-button {
  float: right;
}
.top-card {
  margin-bottom: 4px;
  padding-bottom: 14px;
}
.handle-input {
  width: 300px;
  display: inline-block;
}
.fenye {
  text-align: center;
}

.el-dropdown-link {
  cursor: pointer;
  color: #409eff;
}
.el-icon-arrow-down {
  font-size: 12px;
}
.el-search {
  margin-left: 15px;
}
.upload {
  display: flex;
  float: left;
}
.upload2 {
  display: flex;
}
.iexcel .eexcel .el-search {
  display: flex;
  float: left;
  line-height: 40px;
}
</style>


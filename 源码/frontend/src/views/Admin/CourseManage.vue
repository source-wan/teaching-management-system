<template>
  <el-card class="all">
    <!-- 选择器 -->
    <div class="choice-box">
      <el-select
        v-model="data.collegeId"
        placeholder="请选择院系"
        @change="getMajor"
        filterable
      >
        <el-option
          v-for="(item, index) in firstOptions"
          :key="index"
          :label="item.academyName"
          :value="item.id"
        >
        </el-option>
      </el-select>
      <el-select
        v-model="data.majorId"
        placeholder="请选择专业"
        @change="getClass"
        filterable
      >
        <el-option
          v-for="(item, index) in secondOptions"
          :key="index"
          :label="item.majorName"
          :value="item.id"
        >
        </el-option>
      </el-select>
      <el-select v-model="data.classId" placeholder="请选择班级" filterable>
        <el-option
          v-for="(item, index) in thirdOptions"
          :key="index"
          :label="item.className"
          :value="item.id"
        >
        </el-option>
      </el-select>
      <el-select
        v-model="data.semesterId"
        placeholder="请选择学期"
        clearable
        filterable
      >
        <el-option
          v-for="(item, index) in year"
          :key="index"
          :label="item.label"
          :value="item.value"
        >
        </el-option>
      </el-select>
      <div class="top-button">
        <el-button type="primary" @click="getTableData">搜索</el-button>
      </div>
    </div>

    <div class="info-list">
      <!-- 折叠面版 -->
      <el-collapse v-if="collapseVisible" @change="handleChange">
        <el-collapse-item>
          <template slot="title">
            <i class="el-icon-plus"> 分配课程</i>
          </template>
          <div class="Curriculum_form">
            <el-form
              :inline="true"
              :model="data"
              ref="ruleForms"
              class="demo-form-inline"
            >
              <el-form-item label="课程名">
                <el-select
                  v-model="data.courseId"
                  placeholder="请选择课程名"
                  filterable
                >
                  <el-option
                    v-for="(item, index) in courseOptions"
                    :key="index"
                    :label="item.courseName"
                    :value="item.id"
                  >
                  </el-option>
                </el-select>
              </el-form-item>
              <!-- <el-button
                class="addBtn"
                icon="el-icon-plus"
                @click="dialogFormVisible2 = true"
                circle
              ></el-button> -->
              <el-form-item label="教材">
                <el-select
                  v-model="data.textBookId"
                  placeholder="请选择教材"
                  filterable
                >
                  <el-option
                    v-for="(item, index) in bookOptions"
                    :key="index"
                    :label="item.bookName"
                    :value="item.id"
                  >
                  </el-option>
                </el-select>
              </el-form-item>
              <!-- <el-button
                class="addBtn"
                icon="el-icon-plus"
                @click="dialogFormVisible = true"
                circle
              ></el-button> -->
              <el-form-item label="任课老师">
                <el-select
                  v-model="data.tchId"
                  placeholder="请选择任课老师"
                  filterable
                >
                  <el-option
                    v-for="(item, index) in teacherOptions"
                    :key="index"
                    :label="item.name"
                    :value="item.id"
                  >
                  </el-option>
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="coursesSettingSave"
                  >开设课程</el-button
                >
              </el-form-item>
            </el-form>
          </div>
        </el-collapse-item>
      </el-collapse>
      <!-- 表格数据 -->
      <el-table v-loading="loading" :data="tableData" style="width: 100%">
        <!-- .filter((data) => data.term === this.data.semesterId) -->
        <el-table-column label="序号" width="150">
          <template slot-scope="scope">
            {{ scope.$index + 1 }}
          </template>
        </el-table-column>
        <el-table-column label="学期" width="150">
          <template slot-scope="scope">
            {{ scope.row.term }}
          </template>
        </el-table-column>
        <el-table-column label="课程" width="150">
          <template slot-scope="scope">
            {{ scope.row.courseName }}
          </template>
        </el-table-column>
        <el-table-column label="任课老师" width="150">
          <template slot-scope="scope">
            {{ scope.row.teacherName }}
          </template>
        </el-table-column>
        <el-table-column label="教材" width="150">
          <template slot-scope="scope">
            {{ scope.row.bookName }}
          </template>
        </el-table-column>
        <el-table-column label="操作" fixed="right" width="150">
          <template slot-scope="scope">
            <!-- <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button> -->
            <el-button
              size="mini"
              type="danger"
              @click="handleDelete(scope.$index, scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
      <!-- 分页区域 -->
      <!-- <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="queryInfo.pageIndex"
        :page-sizes="[1, 5, 10, 15, 20]"
        :page-size="queryInfo.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="pageTotal"
      >
      </el-pagination> -->
    </div>
    <!-- 添加课程的模态框 -->
    <el-dialog
      title="课程信息"
      style="text-align: center"
      :visible.sync="dialogFormVisible2"
      :close-on-click-modal="closeOnClickModal2"
      width="500px"
    >
      <el-form
        :model="form"
        status-icon
        :rules="rules_form"
        ref="ruleForm"
        label-width="100px"
        class="demo-ruleForm"
      >
        <el-form-item label="课程名称" prop="courseName">
          <el-input
            v-model="form.courseName"
            placeholder="请输入课程名称"
            style="width: 300px"
          ></el-input>
        </el-form-item>
        <el-form-item label="周学时" prop="weekPeriod">
          <el-input
            v-model="form.weekPeriod"
            placeholder="请输入周学时"
            style="width: 300px"
          ></el-input>
        </el-form-item>
        <el-form-item label="总学时" prop="countPeriod">
          <el-input
            v-model="form.countPeriod"
            placeholder="请输入总学时"
            style="width: 300px"
          ></el-input>
        </el-form-item>
        <el-form-item label="学分" prop="creditHour">
          <el-input
            v-model="form.creditHour"
            placeholder="请输入学分"
            style="width: 300px"
            class="b"
          ></el-input>
        </el-form-item>
      </el-form>
      <div class="dialog-footer">
        <el-button type="primary" @click="handleSave">保 存</el-button>
        <el-button @click="dialogFormVisible2 = false">取 消</el-button>
      </div>
    </el-dialog>
    <!-- 添加教材的模态框 -->
    <el-dialog
      title="添加教材信息"
      style="text-align: center"
      :visible.sync="dialogFormVisible"
      :close-on-click-modal="closeOnClickModal"
      width="500px"
    >
      <el-form
        :model="textBookForm"
        status-icon
        :rules="rules_book"
        ref="ruleBookForm"
        label-width="100px"
        class="demo-ruleForm"
      >
        <el-form-item label="教材名" prop="bookName">
          <el-input
            v-model="textBookForm.bookName"
            placeholder="请输入课程名称"
            style="width: 300px"
          ></el-input>
        </el-form-item>
        <el-form-item label="作者" prop="author">
          <el-input
            v-model="textBookForm.author"
            placeholder="请输入周学时"
            style="width: 300px"
          ></el-input>
        </el-form-item>
        <el-form-item label="出版社" prop="press">
          <el-input
            v-model="textBookForm.press"
            placeholder="请输入总学时"
            style="width: 300px"
          ></el-input>
        </el-form-item>
        <el-form-item label="价格" prop="price">
          <el-input
            v-model="textBookForm.price"
            placeholder="请输入学分"
            style="width: 300px"
            class="b"
          ></el-input>
        </el-form-item>
      </el-form>
      <div class="dialog-footer">
        <el-button type="primary" @click="handleTextBookSave">保 存</el-button>
        <el-button @click="dialogFormVisible = false">取 消</el-button>
      </div>
    </el-dialog>
  </el-card>
</template>

<script>
import { getAcademy, getMajor } from "@/api/Admin/College";
import {
  getClassList,
  getClassCourseList,
  getCourseDataList,
  getTeacherDataList,
  getBookDataList,
  postClassCourse,
  // postCourseTeacher,
  // postCourseTextBook,
  delClassCourse,
} from "@/api/Admin/Class";
var icnow = new Date();
export default {
  data() {
    var verify = (rule, value, callback) => {
      //包含小数的数字
      let reg = /^[+-]?(0|([1-9]\d*))(\.\d+)?$/g;
      if (value === "") {
        callback(new Error("请输入内容"));
      } else if (!reg.test(value)) {
        callback(new Error("请输入数字"));
      } else {
        callback();
      }
    };
    return {
      deptName: "",
      deptName2: "",
      oneyear: "",
      twoyear: "",
      threeyear: "",
      yeardate: icnow.getFullYear(),
      nowSemester: 0,
      closeOnClickModal: false, // 是否可以通过点击modal关闭Dialog
      dialogFormVisible: false, // 控制模态框是否显示
      closeOnClickModal2: false, // 是否可以通过点击modal关闭Dialog
      dialogFormVisible2: false, // 控制模态框是否显示
      collapseVisible: false, // 折叠面版是否显示
      tableData: [],
      pageTotal: 0,
      data: {
        collegeId: null,
        majorId: null,
        classId: null,
        tchId: null,
        courseId: null,
        textBookId: null,
        semesterId: null,
      },
      // 获取院系列表查询参数对象
      queryInfo: {
        // keyword: null,
        // 当前页数
        pageIndex: 1,
        // 每页显示多少数据
        pageSize: 10,
      },
      rules_form: {
        courseName: [{ required: true, message: "课程名不能为空" }],
        weekPeriod: [{ required: true, validator: verify, trigger: "blur" }],
        countPeriod: [{ required: true, validator: verify, trigger: "blur" }],
        creditHour: [{ required: true, validator: verify, trigger: "blur" }],
      },
      rules_book: {
        bookName: [{ required: true, message: "教材名不能为空" }],
        author: [{ required: true, message: "作者名不能为空" }],
        press: [{ required: true, message: "出版社名不能为空" }],
        price: [{ required: true, validator: verify, trigger: "blur" }],
      },
      form: {
        courseId: "",
        weekPeriod: "",
        countPeriod: "",
        creditHour: "",
      },
      // 教材表单信息
      textBookForm: {
        bookName: "",
        author: "",
        press: "",
        price: "",
      },
      //学期
      year: [],
      //第一级
      firstOptions: [], //'软件工程学院', '财经商贸学院', '医学护理学院'
      //第二级
      secondOptions: [],
      //第三级
      thirdOptions: [],
      //开设课程界面
      departmentOptions: [],
      majorOptions: [],
      classOptions: [],
      teacherOptions: [],
      courseOptions: [],
      bookOptions: [],
      loading: true,
    };
  },
  methods: {
    init() {
      // 初始化
      this.getTableData(); // 初始化表格
      this.getSemester();
      this.getCollege();
      // this.getTeacher();
      // this.getCourseList();classCourse
      // this.getTextBookList();
    },
    async getTableData() {
      let classCourseList = (await getClassCourseList(this.data.classId)).data;
      for (const classCourse of classCourseList.data) {
        for (const teacher of this.teacherOptions) {
          if (classCourse.teacherId === teacher.id) {
            classCourse.teacherName = teacher.name;
            
          }
        }
        for (const course of this.courseOptions) {
          if (classCourse.courseId === course.id) {
            classCourse.courseName = course.courseName;
            
          }
        }
        for (const book of this.bookOptions) {
          if (classCourse.bookId === book.id) {
            classCourse.bookName = book.bookName;
  
          }
        }
      }
      this.tableData = classCourseList.data;
      console.log(this.tableData)
    },

    // let params = {
    //   pageIndex: this.queryInfo.pageIndex,
    //   pageSize: this.queryInfo.pageSize,
    //   semesterId: this.data.semesterId,
    //   collegeId: this.data.collegeId,
    //   majorId: this.data.majorId,
    //   classId: this.data.classId
    // }
    // console.log(params)

    // getCourseSettingList(params).then(({ data }) => {
    //   if (data.code === 1000) {
    //     this.tableData = data.data
    //     console.log(this.tableData);
    //     this.pageTotal = data.total;
    //   } else {
    //     this.$message(data.msg);
    //   }
    // })
    // 课程开设保存
    async coursesSettingSave() {
      // 判断传输的对象是否为空
      let isNull = false;
      for (const i in this.data) {
        if (this.data[i] === null) {
          isNull = true;
        }
      }
      if (isNull) {
        this.$message("请选择完全部数据再来开设课程！！！");
      } else {
        console.log(this.data);
        let Course = {
          CourseId: this.data.courseId,
          BookId:this.data.textBookId,
          TeacherId: this.data.tchId,
          ClassId: this.data.classId,
          Term: this.data.semesterId,
        };
        console.log(Course);
        let ClassCourse = (await postClassCourse(Course)).data;
        console.log(ClassCourse);
        // let CourseTeacher = (await postCourseTeacher(courseTeacher)).data;
        // let CourseTextBook = (await postCourseTextBook(courseTextBook)).data;
        // console.log(ClassCourse);
        // console.log(CourseTeacher);
        // console.log(CourseTextBook);
        // addCourseSetting(this.data).then(({ data }) => {
        //   if (data.code === 1000) {
        //     this.data.courseId = null;
        //     this.data.textBookId = null;
        //     this.data.tchId = null;
        //     this.getTableData();
        //   } else {
        //     this.$message(data.msg);
        //   }
        // })
      }
    },
    changeTableData() {
      console.log(this.tableData);
    },
    addSemester() {
      //获取当前年份
      var year = new Date().getFullYear();
      var year2 = year + 1;
      //上半学期
      var halfYear = year + "-" + year2 + "-1";
      //下半学期
      var secondHalfYear = year + "-" + year2 + "-2";

      if (this.semester2.map((item) => item.label).indexOf(halfYear) === -1) {
        let num = this.semester2.length + 1;
        var addhalfYear = {
          label: halfYear,
          semester2: "选项" + num,
        };
        this.semester2.push(addhalfYear);
        this.semester.push(addhalfYear);

        let num2 = this.semester2.length + 1;
        var addsecondHalfYear = {
          label: secondHalfYear,
          semester2: "选项" + num2,
        };
        this.semester2.push(addsecondHalfYear);
        this.semester.push(addsecondHalfYear);

        this.$message.success("添加成功");
      } else {
        this.$message.warning("当前已是最新学期，请勿重复添加");
      }
    },
    handleDelete(index, row) {
      this.$confirm("此操作将永久删除该信息，请谨慎操作！, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          delClassCourse(row.id).then((data) => {
            console.log(data)
            this.tableData.splice(index,1)
          });

          // deleteCourseSetting(row.id).then(({ data }) => {
          //   console.log(data);
          //   if (data.code === 400) {
          //     this.$message.error(data.msg);
          //   } else {
          //     this.tableData.splice(index, 1);
          //     this.$message({
          //       type: 'success',
          //       message: data.msg
          //     });
          //   }
          // })
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除",
          });
        });
    },
    // 检测折叠面板的状态
    handleChange() {},
    // 添加课程信息保存按钮
    handleSave() {
      this.$refs.ruleForm.validate((valid) => {
        if (valid) {
          // AddCourse(this.form).then(({ data }) => {
          //   if (data.code === 1000) {
          //     this.dialogFormVisible2 = false; // 关闭对话框
          //     this.$message.success(data.msg) // 消息提示
          //     this.getCourseList() // 刷新课程信息
          //   } else {
          //     this.$message.error(data.msg)
          //   }
          // })
        } else {
          this.$message.error("请输入完成后再保存");
          return false;
        }
      });
    },
    // 添加教材信息保存按钮
    handleTextBookSave() {
      this.$refs.ruleBookForm.validate((valid) => {
        if (valid) {
          // AddTextBook(this.textBookForm).then(({ data }) => {
          //   if (data.code === 1000) {
          //     this.dialogFormVisible = false; // 关闭对话框
          //     this.$message.success(data.msg) // 消息提示
          //     this.getTextBookList() // 刷新教材信息
          //   } else {
          //     this.$message.error(data.msg)
          //   }
          // })
        } else {
          this.$message.error("请输入完成后再保存");
          return false;
        }
      });
    },
    // 获取学期数据
    getSemester() {
      // //当月
      // let month = icnow.getMonth() + 1;
      // //判断学年学期
      // if (month >= 8) {
      //   this.oneyear = this.yeardate + "-" + (this.yeardate + 1);
      //   this.twoyear = this.yeardate - 1 + "-" + this.yeardate;
      //   this.threeyear = this.yeardate - 2 + "-" + (this.yeardate - 1);
      //   this.nowSemester = 1;
      // } else {
      //   if (month >= 2) {
      //     this.oneyear = this.yeardate - 1 + "-" + this.yeardate;
      //     this.twoyear = this.yeardate - 2 + "-" + (this.yeardate - 1);
      //     this.twoyear = this.yeardate - 3 + "-" + (this.yeardate - 2);
      //     this.nowSemester = 2;
      //   } else {
      //     this.oneyear = this.yeardate - 1 + "-" + this.yeardate;
      //     this.twoyear = this.yeardate - 2 + "-" + (this.yeardate - 1);
      //     this.twoyear = this.yeardate - 3 + "-" + (this.yeardate - 2);
      //     this.nowSemester = 1;
      //   }
      // }

      // if (this.nowSemester == 1) {
      //   this.year = [
      //     {
      //       value: this.oneyear,
      //       label: "大一上学期",
      //     },
      //     {
      //       value: this.twoyear,
      //       label: "大二上学期",
      //     },
      //     {
      //       value: this.threeyear,
      //       label: "大三上学期",
      //     },
      //   ];
      // } else {
      //   this.year = [
      //     {
      //       value: this.oneyear,
      //       label: "大一下学期",
      //     },
      //     {
      //       value: this.twoyear,
      //       label: "大二下学期",
      //     },
      //     {
      //       value: this.threeyear,
      //       label: "大三下学期",
      //     },
      //   ];
      // }
      this.year = [
        {
          value: 1,
          label: "大一上学期",
        },
        {
          value: 2,
          label: "大一下学期",
        },
        {
          value: 3,
          label: "大二上学期",
        },
        {
          value: 4,
          label: "大二下学期",
        },
        {
          value: 5,
          label: "大三上学期",
        },
        {
          value: 6,
          label: "大三下学期",
        },
      ];
    },
    // 获取院系数据
    getCollege() {
      getAcademy().then(({ data }) => {
        this.firstOptions = data.data;
        this.departmentOptions = data.data;
      });
      // getCollegeList().then(({ data }) => {
      //   this.firstOptions = data.data
      //   this.departmentOptions = data.data
      // })
    },
    // 获取专业数据
    getMajor(val) {
      this.deptName = val
        ? this.firstOptions.find((ele) => ele.id === val).academyName
        : "";
      this.secondOptions = [];
      this.data.majorId = null;
      this.thirdOptions = [];
      this.data.classId = null;
      getMajor(this.data.collegeId).then(({ data }) => {
        this.secondOptions = data.data;
      });
      // getMajorList(this.data.collegeId).then(({ data }) => {
      //   // console.log(data);
      //   this.secondOptions = data.data
      // })
    },
    // 获取班级数据
    getClass(val) {
      this.deptName2 = val
        ? this.secondOptions.find((ele) => ele.id === val).majorName
        : "";
      this.thirdOptions = [];
      this.data.classId = null;
      getClassList(this.data.majorId).then(({ data }) => {
        if (data.data.length > 0) {
          this.thirdOptions = data.data;
        } else {
          this.$message.error("暂无班级信息");
        }
      });
      // getClassList(this.data.majorId).then(({ data }) => {
      //   if (data.code === 1000) {
      //     this.thirdOptions = data.data
      //   } else {
      //     this.$message.error(data.msg)
      //   }
      // })
    },
    // 获取教师课程数据
    async getCourseList() {
      let classCourseList = (await getCourseDataList(this.data.classId)).data;
      this.courseOptions = classCourseList.data;
    },
    // 获取教材数据
    async getTextBookList() {
      let classBookList = (await getBookDataList()).data;
      console.log(classBookList);
      this.bookOptions = classBookList.data;
    },
    // 获取教师数据
    async getTeacher() {
      let classTeacherList = (await getTeacherDataList()).data;
      this.teacherOptions = classTeacherList.data;
    },
    handleSizeChange(val) {
      this.queryInfo.pageSize = val;
      this.getTableData();
    },
    handleCurrentChange(val) {
      this.queryInfo.pageIndex = val;
      this.getTableData();
    },
  },
  watch: {
    "data.classId": {
      handler() {
        if (this.data.classId !== null) {
          this.collapseVisible = true;
          this.getTeacher();
          this.getCourseList();
          this.getTextBookList();
          this.getTableData();
        } else {
          this.collapseVisible = false;
        }
      },
    },
    dialogFormVisible: {
      handler() {
        if (this.dialogFormVisible === false) {
          this.$refs.ruleBookForm.resetFields();
        }
      },
    },
    dialogFormVisible2: {
      handler() {
        if (this.dialogFormVisible2 === false) {
          this.$refs.ruleForm.resetFields();
        }
      },
    },
    collapseVisible: {
      handler() {
        if (this.collapseVisible === false) {
          this.data.courseId = null;
          this.data.textBookId = null;
          this.data.tchId = null;
        }
      },
    },
  },
  created: function () {
    this.init();
    setTimeout(() => {
      this.loading = false;
    }, 2500);
  },
};
</script>

<style scoped>
.Curriculum_form {
  margin: 20px 0px 0px 10px;
}

.el-icon-plus {
  margin-left: 10px;
}

.el-collapse-item__content {
  padding: 20px;
}

.addBtn {
  margin-right: 20px;
}

.choice-box {
  display: flex;
  flex-wrap: nowrap;
  width: 100%;
  justify-content: space-between;
  margin-bottom: 10px;
}

.info-list {
  margin-top: 10px;
  /* display: flex; */
  /* width: 1280px; */
}

.dialog-footer {
  margin-top: 10px;
  /* margin-right: 20px; */
}

div.el-dialog {
  margin: 0 auto !important;
  top: 50%;
  transform: translateY(-50%);
}

.el-dialog__header {
  background: #f7f7f7;
  text-align: left;
  font-weight: 600;
}
</style>
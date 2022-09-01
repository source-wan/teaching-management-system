<template>
  <div>
    <div style="margin-top: 15px">
      <!-- <el-input
        placeholder="请输入内容"
        v-model="input3"
        class="input-with-select"
      >
        <el-select v-model="select" slot="prepend" placeholder="请选择">
          <el-option label="宿舍号查找" value="1"></el-option>
          <el-option label="姓名查找" value="2"></el-option>
        </el-select>
        <el-button slot="append" icon="el-icon-search"></el-button>
      </el-input> -->
      <el-select v-model="form.region" placeholder="请选择班级">
        <el-option
          v-for="item in firstOptions"
          :key="item.id"
          :label="item.className"
          :value="item.id"
        >
        </el-option>
      </el-select>
      <el-button @click="allot">分配班级</el-button>
    </div>
    <el-table
      :data="tableData"
      border
      style="width: 100%"
      align="center"
      @selection-change="handleSelectionChange"
    >
      <el-table-column type="selection" width="55"> </el-table-column>
      <el-table-column
        type="index"
        :index="indexMethod"
        width="80px"
        align="center"
        fixed
        laber="序号"
      ></el-table-column>

      <el-table-column prop="name" label="姓名" align="center">
        <template slot-scope="scope">
          {{ scope.row.name }}
        </template>
      </el-table-column>

      <el-table-column prop="dormitoryNo" label="宿舍号" align="center">
        <template slot-scope="scope">
          <el-input
            v-show="scope.row.isEdit"
            v-model="scope.row.dormitoryNo"
          ></el-input>
          <span v-show="!scope.row.isEdit">
            {{ scope.row.dormitoryNo }}
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
</template>

<script>
import jwtDecode from "jwt-decode";
import { getToken } from "@/utils/auth";
import {
  getTeacherId,
  getClassList,
  getStudentList,
  putStudentList,
  allotStudent
} from "@/api/Instructor/DevideClass";
var inow = Date.parse(new Date());
export default {
  data() {
    return {
      cancelButton: false,
      editButton: false,
      majorId: "",
      InstroctorId: "",
      UserId: "",
      input3: "",
      select: "",
      tableData: [],
      multipleSelection: [],
      firstOptions: [],
      form: {
        region: null,
      },
    };
  },
  methods: {

    async allot() {
      let isNull = false;
      for (const i in this.form) {
        if (this.form[i] === null) {
          isNull = true;
        }
      }
      if (isNull) {
        this.$message("请选择班级分配班级！！！");
      } else {
        let model = [];
        for (let i = 0; i < this.multipleSelection.length; i++) {
          let a = {};
          a.StudentId = this.multipleSelection[i].id;
          a.JoinTime = inow/1000;
          a.MajorId=this.multipleSelection[i].majorId
          model.push(a);
        }
        console.log(model)
        let Student=(await allotStudent(this.form.region,model)).data
        if(Student.data.length>0){
          this.$message('说不定成功了呢')
        }else
        {
          this.$message('肯定失败了')
        }
      }
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },

    handleEdit(index, row) {
      console.log(index);
      row.isEdit = true;
      row.editButton = true;
      row.cancelButton = true;
    },
    cancel(index, row) {
      row.isEdit = false;
      row.editButton = false;
      row.cancelButton = false;
    },
    confirm(index, row) {
      let model = {
        dormitoryNo: row.dormitoryNo,
      };
      putStudentList(row.id, model).then((data) => {
        console.log(data);
      });
      // putCourse(row.id, model).then((data) => {
      //   console.log(data);
      // });
      row.isEdit = false;
      row.editButton = false;
      row.cancelButton = false;
    },
    //序号
    indexMethod(index) {
      return index + 1;
    },
  },
  async created() {
    let token = getToken();
    let decodeToken = jwtDecode(token);
    this.UserId = decodeToken.UserId;
    let TeacherInfo = (await getTeacherId(this.UserId)).data;
    this.InstroctorId = TeacherInfo.data[0].id;
    let classList = (await getClassList(this.InstroctorId)).data;
    this.majorId = classList.data[0].majorId;
    let studentList = (await getStudentList(this.majorId)).data;

    for (let item of studentList.data) {
      item.isEdit = false;
      this.tableData.push(item);
    }
    this.firstOptions = classList.data;
    // this.tableData=studentList.data
  },
};
</script>
<style>
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
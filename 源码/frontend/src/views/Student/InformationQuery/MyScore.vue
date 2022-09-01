<template>
  <div>
    <el-card>
      <div>
        <span>学期选择：</span>
        <el-select v-model="value" placeholder="请选择" @change="selectTerm">
          <el-option
            v-for="item in options"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </div>
      <div class="score_table">
        <div class="score">
          <el-card
            class="subject_score"
            v-for="item in tableData"
            :key="item.courseName"
          >
            <span>课程名称：{{ item.courseName }}</span
            ><br />
            <span>课程成绩：{{ item.score }}</span
            ><br />
            <span>课程学分：{{ item.credit }}</span>
          </el-card>
        </div>
      </div>
    </el-card>
  </div>
</template>

<script>
import jwtDecode from "jwt-decode";
import { getToken } from "@/utils/auth";
import { getScore, findStudent } from "@/api/Student/student";
export default {
  data() {
    return {
      studentId: "",
      // 选择学期
      options: [
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
      ],
      value: "",
      // 成绩表
      tableData: [],
    };
  },

  methods: {
    async selectTerm(val) {
      let score = (await getScore()).data;
      this.tableData = score;
      for (let i = 0; i < this.tableData.length; i++) {
        if (this.tableData[i].term !== val) {
          this.tableData.splice(i,1)
        }
      }
      console.log(val);
    },
  },
  async created() {
    let token = getToken();
    let decodeToken = jwtDecode(token);
    console.log(decodeToken);
    this.UserId = decodeToken.UserId;
    let student = (await findStudent(this.UserId)).data;
    this.studentId = student.data[0].id;
    let score = (await getScore()).data;
    console.log(score);
    this.tableData = score;
  },
};
</script>

<style>
/* .subject {
  width: 240px;
  height: 160px;
  background-color: aqua;
  margin-top: 50px;
} */
.score {
  display: grid;
  grid-template-areas: "model model";
}

.subject_score {
  margin-top: 10px;
  width: 60%;
  height: 120px;
  float: left;
  padding-left: 25%;
  line-height: 30px;
  grid-area: "model";
}
</style>
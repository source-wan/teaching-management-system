<template>
  <div>
    <el-card>
      <div class="el-form-style">
        <el-form :label-position="labelPosition" :model="formLabelAlign">
          <el-form-item>
            <div class="el-form-title">
              {{ this.formLabelAlign.title }}
            </div>
          </el-form-item>
          <el-form-item>
            {{ this.formLabelAlign.publisher }}
          </el-form-item>
          <el-form-item>
            {{ this.formLabelAlign.publishName }}
          </el-form-item>
          <el-form-item>
            {{ this.formLabelAlign.publishTime }}
          </el-form-item>
          <el-form-item
            v-for="(item, index) in formLabelAlign.tableData"
            :key="item.id"
          >
            <div class="el-title">
              {{ index + 1 + "." + item.questionName }}
            </div>
            <el-slider :min="0" :max="10" :disabled="true"></el-slider>
          </el-form-item>
        </el-form>
      </div>
    </el-card>
  </div>
</template>

<script>
import jwtDecode from "jwt-decode";
import { getToken } from "@/utils/auth";
import { getUrlParamId } from "@/api/auth";
import { getQuestion, getSurvey, getTeacher } from "@/api/Teacher/Survey";
import dayjs from "dayjs";

export default {
  data() {
    return {
      userId: "",
      labelPosition: "left",
      formLabelAlign: {
        publishName: "",
        title: "闽西职业技术学院评价",
        publishTime: "2022/08/15",
        tableData: [],
      },
    };
  },
  async created() {
    let token = getToken();
    let decodeToken = jwtDecode(token);
    console.log(decodeToken);
    this.userId = decodeToken.UserId;
    var url = location.search;
    var id = getUrlParamId(url);
    let survey = (await getSurvey(this.userId)).data;
    this.formLabelAlign.title = survey.data[0].name;
    this.formLabelAlign.publishTime = dayjs(
      survey.data[0].publishAt * 1000
    ).format("YYYY-MM-DD");
    let questionList = (await getQuestion(id)).data;
    this.formLabelAlign.tableData = questionList.data;
    let Teacher=(await getTeacher(this.userId)).data
    this.formLabelAlign.publishName=Teacher.data[0].name
    console.log(Teacher.data[0].name)
  },
};
</script>

<style scoped>
.el-slider {
  padding-top: 20px;
  border-bottom: none;
  width: 500px;
}
.el-form-title {
  font-size: 20px;
  line-height: 32px;
  font-weight: bold;
  margin: 0;
  padding: 0;
  text-align: center;
  color: #1ea0fa;
}
.el-title {
  font-size: 16px;

  line-height: 26px;
  margin-bottom: 10px;
}
.el-form-style {
  text-align: center;
}
.el-slider {
  margin: 0 auto;
}
</style>
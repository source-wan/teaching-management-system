<template>
  <div>
    <el-card>
      <el-table :data="tableData" style="width: 100%">
        <el-table-column
          fixed
          prop="name"
          label="标题"
          align="center"
        ></el-table-column>
        <el-table-column
          fixed
          prop="status"
          label="发布状态"
          align="center"
        ></el-table-column>
        <el-table-column fixed label="操作" align="right">
          <template slot="header">
            <el-button
              icon="el-icon-refresh"
              @click="handleRefresh"
              type="primary"
            ></el-button>
            <el-button type="primary" @click="newSurvey()">新增问卷</el-button>
          </template>
          <template slot-scope="scope">
            <el-button type="primary" @click="checkSurvey(scope.row)"
              >查看问卷</el-button
            >
            <el-button
              v-if="scope.row.status === '未发布'"
              type="primary"
              @click="edit(scope.$index, scope.row)"
              style="width: 100px"
              >添加问题</el-button
            >
            <el-button v-else @click="checkData(scope.row)" type="primary"
              >查看数据</el-button
            >
            <el-button
              type="primary"
              @click="Remove(scope.$index, scope.row)"
              style="width: 100px"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
    </el-card>
    <el-dialog
      title="新增问卷"
      :visible.sync="DialogVisible"
      width="50%"
      center
      :before-close="handleClose"
    >
      <div>
        <el-button v-if="isEdit" @click="addQuestion">添加问题</el-button>
      </div>
      <div></div>
      <el-divider></el-divider>
      <div>
        <el-form
          :label-position="labelPosition"
          label-width="80px"
          :model="formLabelAlign"
        >
          <div>
            <el-form-item v-if="!isEdit" label="问卷名称">
              <el-input
                v-model="formLabelAlign.name"
                style="width: 200px"
              ></el-input>
            </el-form-item>
          </div>
          <div
            v-for="(item, index) in formLabelAlign.questionData"
            :key="index"
          >
            <el-form-item label="问题">
              <el-input
                v-model="item.QuestionName"
                style="width: 300px"
              ></el-input>
            </el-form-item>
          </div>
        </el-form>
      </div>
      <el-divider></el-divider>
      <div class="divider"></div>
      <span slot="footer" class="dialog-footer">
        <div class="el-date-picker">
          <el-date-picker
            v-if="!isEdit"
            v-model="formLabelAlign.publishAt"
            type="date"
            placeholder="选择发布日期"
          >
          </el-date-picker>
          <el-input
            v-if="!isEdit"
            style="width: 200px"
            v-model="formLabelAlign.expireTime"
            placeholder="问卷有效期/天"
          ></el-input>
        </div>

        <div class="el-footerButton">
          <el-button @click="cancelSurvey">取 消</el-button>
          <el-button v-if="!isEdit" type="primary" @click="saveSurvey()"
            >保存问卷</el-button
          >
          <el-button v-if="isEdit" type="primary" @click="publishSurvey()"
            >发布问卷</el-button
          >
        </div>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import jwtDecode from "jwt-decode";
import { getToken } from "@/utils/auth";
import { getSurvey, postQuestion, postSurvey } from "@/api/Teacher/Survey";
export default {
  data() {
    return {
      id: "",
      isEdit: false,
      tableData: [],
      DialogVisible: false,
      labelPosition: "right",
      formLabelAlign: {
        name: "",
        questionData: [],
        publishAt: "",
        expireTime: null,
      },
    };
  },
  methods: {
   async handleRefresh() {
      let survey = (await getSurvey(this.UserId)).data;
      let inow = new Date() / 1000;

      for (const item of survey.data) {
        if (item.publishAt < inow) {
          item.status = "已发布";
        } else {
          item.status = "未发布";
        }
      }
      this.tableData = survey.data;
      console.log(this.tableData);
    },
    newSurvey() {
      this.isEdit = false;
      this.formLabelAlign.name = "";
      this.formLabelAlign.questionData = [];
      this.formLabelAlign.publishAt = "";
      this.formLabelAlign.expireTime = null;
      this.DialogVisible = true;
    },
    handleClose(done) {
      this.formLabelAlign.questionData = [];
      done();
    },
    cancelSurvey() {
      this.DialogVisible = false;
      this.formLabelAlign.questionData = [];
    },

    async publishSurvey() {
      for (const item of this.formLabelAlign.questionData) {
        console.log(item.QuestionName);
        let model = {
          SurveyId: this.id,
          QuestionName: item.QuestionName,
        };
        let question = (await postQuestion(model)).data;
        console.log(question);
      }
      // let model={
      //   SurveyId:this.id
      //   QuestionName:
      // }
    },
    async saveSurvey() {
      let model = {
        Name: this.formLabelAlign.name,
        UserId: this.UserId,
        PublishAt: this.formLabelAlign.publishAt / 1000,
        ExpireTime: this.formLabelAlign.expireTime / 1,
      };
      let survey = (await postSurvey(model)).data;
      console.log(survey);
      this.DialogVisible = false;
      this.formLabelAlign.questionData = [];
    },
    addQuestion() {
      let arr = { QuestionName: "" };
      this.formLabelAlign.questionData.push(arr);
    },
    async edit(index, row) {
      this.id = row.id;
      this.isEdit = true;
      this.formLabelAlign.name = this.tableData[index].name;
      this.formLabelAlign.questionData = [];
      this.DialogVisible = true;
    },
    checkSurvey(row) {
      this.$router.push(`/CheckSurvey?id=${row.id}`);
    },
    checkData(row) {
      console.log(row.id);
      this.$router.push(`/CheckSurveyData?id=${row.id}`);
    },
    Remove(index) {
      console.log(index);
      this.tableData.splice(index, 1);
    },
  },
  async created() {
    let token = getToken();
    let decodeToken = jwtDecode(token);
    console.log(decodeToken);
    this.UserId = decodeToken.UserId;
    let survey = (await getSurvey(this.UserId)).data;
    let inow = new Date() / 1000;

    for (const item of survey.data) {
      if (item.publishAt < inow) {
        item.status = "已发布";
      } else {
        item.status = "未发布";
      }
    }
    this.tableData = survey.data;
    console.log(this.tableData);
  },
};
</script>

<style scoped>
.divider {
  padding: 0;
}
.el-dialog__footer {
  display: flex;
}
.el-date-picker {
  display: flex;
}
.el-footerButton {
  display: inline-block;
  position: relative;
  left: 250px;
}
</style>
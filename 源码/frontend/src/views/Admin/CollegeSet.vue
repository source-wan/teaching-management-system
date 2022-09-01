<template>
  <div>
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
        <el-button type="primary" icon="el-icon-refresh" @click="handleRefresh">
        </el-button>
      </div>
      <el-dialog
        title="新增专业"
        :visible.sync="dialogFormVisible"
        :append-to-body="true"
        class="dialog"
      >
        <div class="dialog-main">
          <el-form ref="form" label-width="80px">
            <el-form-item label="院系">
              <el-select
                v-model="form.region"
                placeholder="选择院系"
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
            <el-form-item label="专业名称">
              <el-input
                v-model="form.majorName"
                style="width: 200px"
              ></el-input>
            </el-form-item>
            <el-button @click="addMajor" type="primary" class="notarizeButton"
              >确认</el-button
            >
          </el-form>
        </div>
      </el-dialog>
    </div>
    <div class="table-main">
      <el-table
        :data="
          tableData.filter(
            (data) =>
              !state ||
              data.academyName.toLowerCase().includes(state.toLowerCase()) ||
              data.campusName.toLowerCase().includes(state.toLowerCase())
          )
        "
        style="width: 100%"
      >
        <el-table-column type="expand">
          <template slot-scope="scope">
            <el-tag
              v-for="item in tableData[scope.$index].major"
              :key="item.id"
              class="tag"
            >
              <span>{{ item.majorName }}</span>
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column
          label="序号"
          prop="tableData.index"
          type="index"
          width="200px"
          :index="indexMethod"
        >
        </el-table-column>
        <!-- <template slot-scope="scope">
      </template> -->
        <el-table-column label="院系" prop="academyName"> </el-table-column>
        <el-table-column label="校区" prop="campusName"> </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<script>
import {
  getAcademy,
  getCampus,
  getMajor,
  postMajor,
} from "@/api/Admin/College";

export default {
  data() {
    return {
      firstOptions:[],
      state: "",
      dialogFormVisible: false,
      direction: "rtl",
      tableData: [],
      form: {
        majorName: "",
        region: "",
      },
    };
  },
  methods: {
    async handleRefresh() {
      let data = (await getAcademy()).data;
      let arr = [];
      for (let academy of data.data) {
        let campus = await getCampus(academy.campusId);
        let major = await getMajor(academy.id);
        console.log(major.data.data);
        academy.campusName = campus.data.campusName;
        academy.major = major.data.data;
        arr.push(academy);
      }
      this.tableData = arr;
    },
    //序号
    indexMethod(index) {
      return index + 1;
    },
    handleAdd() {
      this.dialogFormVisible = true;
    },
    //新增专业
    async addMajor() {
      let model = {
        academyId: this.form.region,
        majorName: this.form.majorName,
      };
      console.log(model);
      let postMajorList = (await postMajor(model)).data;
      console.log(postMajorList.data);
      // postMajor(model).then((data) => {
      //   console.log(data);
      // });
      this.dialogFormVisible = false;
    },
  },
  async created() {

    
    let data = (await getAcademy()).data;
    // let arr=[];
    for (let academy of data.data) {
      let campus = await getCampus(academy.campusId);
      let major = await getMajor(academy.id);
      console.log(major.data.data);
      // arr.push(major.data.data[0])
      // academy.major=arr
      academy.campusName = campus.data.campusName;
      academy.major = major.data.data;
      this.tableData.push(academy);
      this.firstOptions=data.data
    }
    console.log(this.tableData)
  },
};
</script>

<style>
.demo-table-expand {
  font-size: 0;
}
.demo-table-expand label {
  width: 90px;
  color: #99a9bf;
  margin-left: 20px;
}
.demo-table-expand .el-form-item {
  margin-right: 0;
  margin-bottom: 0;
  width: 50%;
}
.tag {
  margin-left: 40px;
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
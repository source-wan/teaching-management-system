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
          title="新增院系"
          :visible.sync="dialogFormVisible"
          :append-to-body="true"
          class="dialog"
        >
          <div class="dialog-main">
            <el-form ref="form" label-width="70px">
              <el-form-item label="院系名称" style="width: 60vh">
                <el-input v-model="form.academyName"></el-input>
              </el-form-item>
              <el-form-item label="校区">
                <el-select v-model="form.region" placeholder="请选择校区">
                  <el-option
                    v-for="item in firstOptions"
                    :key="item.id"
                    :label="item.campusName"
                    :value="item.id"
                  >
                  </el-option>
                </el-select>
              </el-form-item>
              <el-button
                @click="addAcademy"
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
              data.academyName.toLowerCase().includes(state.toLowerCase()) ||
              data.campusName.toLowerCase().includes(state.toLowerCase())
          )
        "
        style="width: 100%"
      >
        <el-table-column label="序号" width="200px" type="index" align="center">
        </el-table-column>
        <el-table-column label="学院名称" prop="academyName" align="center">
        </el-table-column>
        <el-table-column label="校区" prop="campusName" align="center">
        </el-table-column>

        <el-table-column label="操作" align="center">
          <template slot-scope="scope">
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
import {
  delAcademy,
  getAcademy,
  getCampus,
  postAcademy,
  getCampusList,
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
        academyName: "",
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
        academy.campusName = campus.data.campusName;
        arr.push(academy);
      }
      console.log(arr);
      this.tableData = arr;
    },
    //添加
    addAcademy() {
      let model = {
        AcademyName: this.form.academyName,
        CampusId: this.form.region,
      };
      postAcademy(model).then((data) => {
        console.log(data);
      });
      this.drawer = false;
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
    handleDelete(index, row) {
      delAcademy(row.id).then((data) => {
        console.log(data);
      });
    },
    handleAdd() {
      this.dialogFormVisible = true;
    },
  },
  async created() {
    
    let data = (await getAcademy()).data;
    let data2=(await getCampusList()).data
    // let arr=[];
    for (let academy of data.data) {
      let campus = await getCampus(academy.campusId);
      academy.campusName = campus.data.campusName;
      this.tableData.push(academy);
    }
    this.firstOptions=data2.data
    console.log(this.firstOptions)
    console.log(this.tableData);
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
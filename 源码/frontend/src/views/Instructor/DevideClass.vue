 <template>
  <div>
    <div style="margin-top: 15px">
      <el-select
        v-model="data.semesterId"
        @change="changeTable"
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
      <el-input
        placeholder="请输入内容"
        v-model="input3"
        class="input-with-select"
      >
        <el-button slot="append" icon="el-icon-search"></el-button>
      </el-input>
    </div>
    <el-table :data="classTable" style="width: 100%" class="margin">
      <el-table-column type="expand">
        <template slot-scope="scope">
          <el-table
            :data="classTable[scope.$index].tableData"
            height="250"
            border
            style="width: 100%"
          >
            <el-table-column
              prop="name"
              label="姓名"
              width="180"
              align="center"
            >
            </el-table-column>
            <el-table-column prop="studentCode" label="学号" align="center">
            </el-table-column>
            <el-table-column prop="dormitoryNo" label="宿舍号" align="center">
            </el-table-column>
            <el-table-column prop="caozuo" label="操作" align="center">
              <template slot-scope="scope">
                <el-button
                  type="primary"
                  icon="el-icon-edit"
                  align="center"
                  @click="viewMore(scope.$index, scope.row)"
                  >查看更多
                </el-button>
                <el-dialog
                  width="64%"
                  title="基本信息"
                  :visible.sync="open"
                  :append-to-body="true"
                >
                  <div class="form">
                    <div class="form-top">
                      <el-form :model="form">
                        <div class="form-left">
                          <el-form-item
                            label="姓名："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.name"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>
                          <el-form-item
                            label="学号："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.studentCode"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>

                          <el-form-item
                            label="身份证号："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.identityCode"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>
                          <el-form-item
                            label="父亲姓名："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.fatherName"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>
                          <el-form-item
                            label="母亲姓名："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.motherName"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>

                          <el-form-item
                            label="QQ号："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.qq"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>

                          <el-form-item
                            label="家庭住址："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.address"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>
                        </div>

                        <div class="form-right">
                          <el-form-item
                            label="性别："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.gender"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>
                          <el-form-item
                            label="手机号："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.phone"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>

                          <el-form-item
                            label="父亲联系方式："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.fatherPhone"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>

                          <el-form-item
                            label="母亲联系方式："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.motherPhone"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>
                          <el-form-item
                            width="100px"
                            label="微信号："
                            :label-width="formLabelWidth"
                          >
                            <el-input
                              disabled
                              v-model="form.weChat"
                              autocomplete="off"
                              class="label"
                            ></el-input>
                          </el-form-item>
                        </div>
                      </el-form>
                      <div slot="footer" class="dialog-footer"></div>
                    </div>
                  </div>
                </el-dialog>
                <el-button
                  type="danger"
                  icon="el-icon-delete"
                  align="center"
                  @click="changeClass(scope.$index, scope.row)"
                  >转班记录</el-button
                >
                <el-dialog
                  width="64%"
                  title="转班记录"
                  :visible.sync="open2"
                  :append-to-body="true"
                >
                  <div class="changeClass_table">
                    <div class="changeClass_tableMain">
                      <template>
                        <el-table
                          :data="changeClassData"
                          style="width: 100%"
                          align="center"
                        >
                          <!-- <el-table-column prop="JoinTime" label="时间">
                          </el-table-column> -->
                          <el-table-column
                            label="历史班级"
                            align="center"
                          >
                            <template slot-scope="scope">
                              {{ scope.row.className }}
                            </template>
                          </el-table-column>
                        </el-table>
                      </template>
                    </div>
                  </div>
                </el-dialog>
              </template>
            </el-table-column>
          </el-table>
        </template>
      </el-table-column>

      <el-table-column
        label="序号"
        prop="tableData.index"
        type="index"
        width="200px"
        :index="indexMethod"
        align="center"
      >
      </el-table-column>

      <el-table-column label="班级" prop="className" align="center">
      </el-table-column>
    </el-table>
  </div>
</template> 

<script>
import jwtDecode from "jwt-decode";
import { getToken } from "@/utils/auth";
import { getTeacherId } from "@/api/Instructor/DevideClass";
import { getClassList } from "@/api/Instructor/DevideClass";
import {
  getClassStudentList,
  getChangeClass,
} from "@/api/Instructor/DevideClass";
export default {
  data() {
    return {
      //  loading: true,
      changeClassData: [],
      InstroctorId: "",
      UserId: "",
      formLabelWidth: "120px",
      open: false,
      open2: false,
      input3: "",
      select: "",
      classTable: [],
      year: [],
      data: {
        semesterId: "",
      },
      form: {
        name: "",
        qq: "",
        identityCode: "",
        gender: "",
        address: "",
        dormitoryNo: "",
        fatherName: "",
        fatherPhone: "",
        mail: "",
        montherPhone: "",
        motherName: "",
        phone: "",
        studentCode: "",
        weChat: "",
      },
    };
  },

  methods: {
    async changeTable(val) {
      if (val !== 1) {
        for (const item of this.classTable) {
          item.tableData = [];
        }
        console.log(this.classTable);
      } else {
        let classList = (await getClassList(this.InstroctorId)).data;
        this.classTable = classList.data;
        for (const item of this.classTable) {
          let classstudent = (await getClassStudentList(item.id)).data;
          item.tableData = classstudent.data;
        }
        console.log(this.classTable);
      }
    },
    //序号
    indexMethod(index) {
      return index + 1;
    },
    viewMore(index, row) {
      this.open = true;
      console.log(row);
      (this.form.name = row.name),
        (this.form.qq = row.qq),
        (this.form.identityCode = row.identityCode),
        (this.form.gender = row.gender.toString()),
        (this.form.address = row.address),
        (this.form.dormitoryNo = row.dormitoryNo),
        (this.form.fatherName = row.fatherName),
        (this.form.fatherPhone = row.fatherPhone),
        (this.form.mail = row.mail),
        (this.form.montherPhone = row.montherPhone),
        (this.form.motherName = row.motherName),
        (this.form.phone = row.phone),
        (this.form.studentCode = row.studentCode),
        (this.form.weChat = row.weChat);
    },
    async changeClass(index, row) {
      this.open2 = true;
      this.changeClassData = [];
      console.log(row);
      let changeClass = (await getChangeClass(row.id)).data;
      console.log(changeClass)
      for (const item of changeClass.data) {
        let a={};
        a.className=item.className
      this.changeClassData.push(a)
      }
    },
  },
  async created() {
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
    let token = getToken();
    let decodeToken = jwtDecode(token);
    this.UserId = decodeToken.UserId;
    let TeacherInfo = (await getTeacherId(this.UserId)).data;
    console.log(TeacherInfo);
    this.InstroctorId = TeacherInfo.data[0].id;
    console.log(this.InstroctorId);
    let classList = (await getClassList(this.InstroctorId)).data;
    this.classTable = classList.data;
    for (const item of this.classTable) {
      let classstudent = (await getClassStudentList(item.id)).data;
      item.tableData = classstudent.data;
    }
    console.log(this.classTable);
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
.form {
  width: 100%;
  height: 100%;
}

.form-top {
  width: 100%;
  height: 100%;
}

.form-right {
  width: 40%;
  float: right;
  height: 80%;
}

.dialog-footer {
  margin-left: 60%;
  padding-top: 42%;
}

.form-left {
  float: left;
  width: 40%;
  height: 80%;
  margin-left: 5%;
}
.el-button {
  margin-left: 20px;
}
</style> 


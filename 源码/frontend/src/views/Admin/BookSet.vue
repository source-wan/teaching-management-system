<template slot-scope="scope">
  <div class="table">
    <div class="table-top">
      <!-- //头部工具栏 -->
      <div class="top-input">
        <!-- //收索 -->
        <el-input
          placeholder="请输入内容"
          v-model="input3"
          class="input-with-select"
        >
          <!-- //下拉菜单 -->
          <el-select
            v-model="select"
            slot="prepend"
            placeholder="请选择"
            style="width: 150px"
          >
            <el-option label=" 大一上学期" value="1"></el-option>
            <el-option label="大一下学期" value="2"></el-option>
            <el-option label="大二上学期" value="3"></el-option>
            <el-option label="大二下学期" value="4"></el-option>
          </el-select>
          <!-- //搜索按钮 -->
          <!-- <el-button
            slot="append"
            icon="el-icon-search"
            style="width: 10px"
          ></el-button> -->
        </el-input>
      </div>
      <!-- //头部右侧按钮 -->
      <div class="top-button">
        <!-- //新增按钮 -->
        <el-button
          type="primary"
          icon="el-icon-plus"
          circle
          @click="dialogFormVisible = true"
        ></el-button>
        <!-- //头部右侧按钮-刷新按钮 -->
        <el-button
          type="primary"
          circle
          icon="el-icon-refresh"
          @click="search()"
        ></el-button>
      </div>
    </div>
    <el-dialog
      title="教材详细信息"
      :visible.sync="dialogFormVisible"
      :append-to-body="true"
    >
      <el-form :model="formText">
        <el-form-item label="ISBN" :label-width="formLabelWidth">
          <el-input v-model="formText.ISBN" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="书名" :label-width="formLabelWidth">
          <el-input v-model="formText.BookName" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="作者" :label-width="formLabelWidth">
          <el-input v-model="formText.Author" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="出版社" :label-width="formLabelWidth">
          <el-input v-model="formText.Publisher" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="出版时间" :label-width="formLabelWidth">
          <el-input v-model="formText.PublishAt" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="价格" :label-width="formLabelWidth">
          <el-input v-model="formText.price" autocomplete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="handlePost">确认</el-button>
      </div>
    </el-dialog>

    <el-table :data="tableData">
      <el-table-column
        type="index"
        :index="indexMethod"
        laber="序号"
      ></el-table-column>

      <el-table-column prop="isbn" label="ISBN"> </el-table-column>

      <el-table-column prop="bookName" label="书名"> </el-table-column>
      <el-table-column prop="author" label="作者"> </el-table-column>
      <el-table-column prop="publisher" label="出版社"> </el-table-column>
      <el-table-column prop="publishAt" label="出版时间"> </el-table-column>
      <el-table-column prop="price" label="价格"> </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button
            size="large"
            type="danger"
            slot="reference"
            icon="el-icon-delete el-icon--left"
            @click="handleDelete(scope.$index, scope.row)"
            >删除</el-button
          >
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getBookList, postBook, delBook } from "@/api/Admin/book";
export default {
  data() {
    return {
      input3: "",
      dialogFormVisible: false,
      loading: false,
      handleAdd: false,
      tableData: [],
      select: "",
      form: {
        college: "",
        specialty: "",
        class: "",
        course: "",
        textbook: "",
        delivery: false,
        type: [],
        resource: "",
        desc: "",
      },
      formText: {
        BookName: "",
        ISBN: "",
        Author: "",
        Publisher: "",
        PublishAt: "",
        price: "",
      },
      formLabelWidth: "80px",
      timer: null,
    };
  },
  methods: {
    async search() {
      let getBook = (await getBookList()).data;
      console.log(getBook.data);
      this.tableData = getBook.data;
    },
    async handleDelete(index, row) {
      console.log(row);
      let remove = (await delBook(row.id)).data;
      console.log(remove);
    },
    async handlePost() {
      let model = {
        ISBN: this.formText.ISBN,
        BookName: this.formText.BookName,
        Author: this.formText.Author,
        Publisher: this.formText.Publisher,
        PublishAt: this.formText.PublishAt,
        Price: this.formText.price,
      };
      console.log(model);
      let newBook = await postBook(model);
      console.log(newBook);
      this.dialogFormVisible = false;
    },
    indexMethod(index) {
      return index + 1;
    },
  },
  async created() {
    let getBook = (await getBookList()).data;
    console.log(getBook.data);
    this.tableData = getBook.data;
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

.el-icon-search {
  margin: auto;
}
</style>>

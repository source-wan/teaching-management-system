<template>
  <div class="hello">
    <el-upload
    class="upload-demo" ref="upload" accept=".xls,.xlsx"
    action="#"
    :on-change="upload"
    :show-file-list="false"
    :auto-upload="false"
    >
    <!-- 导入的excel表格和展示的表头名el-table的prop对应 -->
      <el-button slot="trigger" size="large">导入excel</el-button>
    </el-upload>
    </div>
</template>

<script>
import XLSX from 'xlsx'
// import Sortable from 'sortablejs' // 该插件用于实现表格可拖拽功能，可不用，它相关的都注释了; 如果将其放开，需要在项目中安装sortablejs 插件；npm install sortablejs --save
export default {
  data () {
    return {
    }
  },
  mounted () {
    // this.implDropTable()
  },
  methods: {
    // implDropTable () {
    //   const table = document.querySelector('.el-table__body-wrapper tbody')
    //   const self = this
    //   Sortable.create(table, {
    //     onEnd ({ newIndex, oldIndex }) {
    //       const targetRow = self.tabData.splice(oldIndex, 1)[0]
    //       console.log(targetRow, 'targetrow')
    //       // here
    //       self.tabData.splice(newIndex, 1, targetRow)
    //     }
    //   })
    // },
    upload (file, fileList) {
      console.log(file, 'file')
      console.log(fileList, 'fileList')
      let files = { 0: file.raw }// 取到File
      this.readExcel(files)
    },
    readExcel (files) { // 表格导入
      console.log(files)
      if (files.length <= 0) { // 如果没有文件名
        return false
      } else if (!/\.(xls|xlsx)$/.test(files[0].name.toLowerCase())) {
        this.$Message.error('上传格式不正确，请上传xls或者xlsx格式')
        return false
      }

      const fileReader = new FileReader()
      fileReader.onload = (ev) => {
        try {
          const data = ev.target.result
          const workbook = XLSX.read(data, {
            type: 'binary'
          })
          const wsname = workbook.SheetNames[0]// 取第一张表
          const ws = XLSX.utils.sheet_to_json(workbook.Sheets[wsname])// 生成json表格内容
          console.log(ws, 'ws是表格里的数据，且是json格式')
          this.tabData = ws
          console.log(this.$emit('getTableData',this.tabData))
          this.$emit('getTableData',this.tabData)
          console.log(this.tabData, 'tabdata')
          // 重写数据
          this.$refs.upload.value = ''
        } catch (e) {
          return false
        }
      }
      fileReader.readAsBinaryString(files[0])
    }

  }
}
</script>
<style>

</style>
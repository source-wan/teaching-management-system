<template>
  <div class="export-excel-bg">
    <el-button @click="exportBtn">导出Excel</el-button>
  </div>
</template>
<script>
import XLSX from "xlsx";
export default {
  name: "export-excel",
  props: ["sheetData", "sheetDataName", "Pagename"],
  data() {
    return {
      tableData: this.sheetData,
      dataName: this.sheetDataName,
      PageName: this.Pagename,
      index:0,
    };
  },
  methods: {
    workbook2blob(workbook) {
      // 生成excel的配置项
      var wopts = {
        // 要生成的文件类型
        bookType: "xlsx",
        // // 是否生成Shared String Table，官方解释是，如果开启生成速度会下降，但在低版本IOS设备上有更好的兼容性
        bookSST: false,
        type: "binary",
      };
      var wbout = XLSX.write(workbook, wopts);
      // 将字符串转ArrayBuffer
      function s2ab(s) {
        var buf = new ArrayBuffer(s.length);
        var view = new Uint8Array(buf);
        for (var i = 0; i !== s.length; ++i) view[i] = s.charCodeAt(i) & 0xff;
        return buf;
      }
      let buf = s2ab(wbout);
      var blob = new Blob([buf], {
        type: "application/octet-stream",
      });
      return blob;
    },

    // 将blob对象 创建bloburl,然后用a标签实现弹出下载框
    openDownloadDialog(blob, fileName) {
      if (typeof blob === "object" && blob instanceof Blob) {
        blob = URL.createObjectURL(blob); // 创建blob地址
      }
      var aLink = document.createElement("a");
      aLink.href = blob;
      // HTML5新增的属性，指定保存文件名，可以不要后缀，注意，有时候 file:///模式下不会生效
      aLink.download = fileName || "";
      var event;
      if (window.MouseEvent) event = new MouseEvent("click");
      //   移动端
      else {
        event = document.createEvent("MouseEvents");
        event.initMouseEvent(
          "click",
          true,
          false,
          window,
          0,
          0,
          0,
          0,
          0,
          false,
          false,
          false,
          false,
          0,
          null
        );
      }
      aLink.dispatchEvent(event);
    },
    exportBtn() {
      this.exportExcel();
    },
    exportExcel() {
      var wb = XLSX.utils.book_new();
      // let sheet1data = this.tableData;
      let sheet2data = [{ name: '张三', do: '整理文件' }, { name: '李四', do: '打印' }]
      let sheet3data = [{ name: '王五', do: 'Vue' }, { name: '二楞', do: 'react' }]
      // var sheet1 = XLSX.utils.json_to_sheet(sheet1data);
      var sheet2 = XLSX.utils.json_to_sheet(sheet2data)
      var sheet3 = XLSX.utils.json_to_sheet(sheet3data)
      console.log(sheet2, sheet3, 'sheet123')
      // 创建一个新的空的workbook
        //到时候直接发axios传班级ID获取学生
        // let sheetdata=this.tableData;
        // var sheet=XLSX.utils.json_to_sheet(sheetdata)
        XLSX.utils.book_append_sheet(wb,sheet2,'sb')
      // XLSX.utils.book_append_sheet(wb, sheet1, this.DataName);
      XLSX.utils.book_append_sheet(wb, sheet2, '行政部')
      XLSX.utils.book_append_sheet(wb, sheet3, '前端部')
      const workbookBlob = this.workbook2blob(wb);
      this.openDownloadDialog(workbookBlob, `${this.dataName}.xlsx`);
    },
  },
};
</script>
<style>
.export-excel-bg {
  display: flex;
}
</style>
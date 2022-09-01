<template>
  <div class="class-table">
    <table>
      <!-- 周几 -->
      <thead>
        <tr>
          <th>时间</th>
          <th v-for="(week, index) in weeks" :key="index">
            {{ "周" + digital2Chinese(index + 1, "week") }}
          </th>
        </tr>
      </thead>
      <!-- 上课时间 -->
      <tbody>
        <tr v-for="(item, index) in classTableData" :key="index">
          <td class="table-time">
            <p>{{ "第" + digital2Chinese(index + 1) + "节" }}</p>
            <p class="period">{{ item.classesTime }}</p>
          </td>

          <td v-for="(week, index) in weeks" :key="index" class="table-class">
            {{ item[week] || "-" }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  data() {
    return {
      weeks: [
        "monday",
        "tuesday",
        "wednesday",
        "thursday",
        "friday",
        "saturday",
        "sunday",
      ],
      classTableData: [],
      spanArr: [],
      position: 0,
    };
  },
  methods: {
    // 用于计算周几以及第几节课
    digital2Chinese(num, identifier) {
      const character = [
        "零",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六",
        "七",
        "八",
        "九",
      ];
      return identifier === "week" && (num === 0 || num === 7)
        ? "日"
        : character[num];
    },

    // 查找
    queryData() {
      this.classTableData = [
        {
          classesTime: "08:00-08:45",
          monday: "高数",
        },
        {
          classesTime: "08:55-09:40",
          monday: "Vue开发",
        },
        {
          classesTime: "10:00-10:45",
        },
        {
          classesTime: "10:55-11:40",
        },
        {
          classesTime: "14:00-14:45",
        },
        {
          classesTime: "14:55-15:40",
        },
        {
          classesTime: "16:00-16:45",
        },
        {
          classesTime: "16:55-17:40",
        },
      ];
      this.rowspan();
    },

    rowspan() {
      this.classTableData.forEach((item, index) => {
        if (index === 0) {
          this.spanArr.push(1);
          this.position = 0;
        } else {
          if (
            this.classTableData[index].type ===
            this.classTableData[index - 1].type
          ) {
            this.spanArr[this.position] += 1;
            this.spanArr.push(0);
          } else {
            this.spanArr.push(1);
            this.position = index;
          }
        }
      });
    },
    //表格合并行
    objectOneMethod({ rowIndex, columnIndex }) {
      if (columnIndex === 0) {
        const _row = this.setTable(this.ptableDate).one[rowIndex];
        const _col = _row > 0 ? 1 : 0;
        return {
          rowspan: _row,
          colspan: _col,
        };
      }
      if (columnIndex === 1) {
        const _row = this.setTable(this.ptableDate).two[rowIndex];
        const _col = _row > 0 ? 1 : 0;
        return {
          rowspan: _row,
          colspan: _col,
        };
      }
    },
    setTable(tableData) {
      let spanOneArr = [],
        spanTwoArr = [],
        concatOne = 0,
        concatTwo = 0;
      tableData.forEach((item, index) => {
        if (index === 0) {
          spanOneArr.push(1);
          spanTwoArr.push(1);
        } else {
          if (item.projName === tableData[index - 1].projName) {
            //第一列需合并相同内容的判断条件
            spanOneArr[concatOne] += 1;
            spanOneArr.push(0);
          } else {
            spanOneArr.push(1);
            concatOne = index;
          }
          if (item.dirtySection === tableData[index - 1].dirtySection) {
            //第二列和需合并相同内容的判断条件
            spanTwoArr[concatTwo] += 1;
            spanTwoArr.push(0);
          } else {
            spanTwoArr.push(1);
            concatTwo = index;
          }
        }
      });
      return {
        one: spanOneArr,
        two: spanTwoArr,
      };
    },
  },

  mounted() {
    this.queryData();
  },
};
</script>

<style scoped >
.class-table{
  margin-left: 15%;
}
th {
  width: 120px;
  height: 50px;
  background-color:#99CC33;
}
/* td {
  text-align: center;
  background-color: #dcdcdc;
} */
.table-time{
  background-color: #0099CC;
  text-align: center;
}
.table-class{
  background-color:#ebf6f7;
  text-align: center;
}
</style>
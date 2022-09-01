<template>
  <div>
    <div class="card">
      <el-card>
        {{ "当前时间为: " + newTime }}
        <br>
        {{ "当前学年为: " + schoolYearLong+'   学期为:'+semester }}
      </el-card>
    </div>


  </div>
  
</template>

<script>
const days = ["天", "一", "二", "三", "四", "五", "六"]; // 星期数组
var icnow = new Date(); // 初始化时间
var interval; // 定义全局定时器，用于清除定时器
export default {
  data() {
    return {
      schoolYearLong: "",
      semester: "",
      year: icnow.getFullYear(),
      month: icnow.getMonth() + 1,
      date: icnow.getDate(),
      day: days[icnow.getDay() - 1],
      time: icnow.toTimeString().substring(0, 8),
    };
  },
  methods: {
    shoolYearCount(month){
      if(month>=8){
        this.schoolYearLong=this.year+'-'+(this.year+1)
        this.semester=1
      }else
      {
        if(month>=2){
          this.schoolYearLong=(this.year-1)+'-'+this.year
          this.semester=2
        }else{
          this.schoolYearLong=(this.year-1)+'-'+this.year
          this.semester=1
        }
      }
    }
  },
  created() {
    interval = setInterval(() => {
      let icnow = new Date();
      this.year = icnow.getFullYear();
      this.month = icnow.getMonth() + 1;
      this.date = icnow.getDate();
      this.day = days[icnow.getDay()];
      this.time = icnow.toTimeString().substring(0, 8);
    }, 1000);
    this.shoolYearCount(this.month);
  },
  computed: {
    // 当前时间
    newTime: function () {
      return (
        this.year +
        "年" +
        this.month +
        "月" +
        this.date +
        "日 星期" +
        this.day +
        " " +
        this.time
      );
    },
  },
  beforeDestroy() {
    clearInterval(interval);
  },
};
</script>

<style scoped>
body,html{
  height: 100%;
  width: 100%;
}
.card{
  width: 30%;
  height: 30%;
  font-size:larger;
}
.main{
  width:100%;
  background-image: url(@/assets/main.jpg);
}
</style>
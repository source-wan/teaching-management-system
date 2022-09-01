<template>
  <div class="select_course">
    <template class="select_subject">
      <el-table :data="course" style="width: 100%" >
        <el-table-column prop="id" label="编号" width="50" align="center">
        </el-table-column>
        <el-table-column
          prop="course_name"
          label="选修课名称"
          width="180"
          align="center"
        >
        </el-table-column>
        <el-table-column
          prop="course_teacher"
          label="科任老师"
          width="100px"
          align="center"
        >
        </el-table-column>
        <el-table-column prop="course_time" label="上课时间" align="center">
        </el-table-column>
        <el-table-column prop="course_place" label="上课地点" align="center">
        </el-table-column>
        <el-table-column prop="isActive" label="是否选择" align="center">
          <el-checkbox  @change="a(course.id)" :label="course.id" :key="index"></el-checkbox>
        </el-table-column>
      </el-table>
      <el-button type="primary" class="submit" @click="submit">提交</el-button>
    </template>
  </div>
</template>


<script>
export default {
  data() {
    return {
      course_sum:[],
      course: [
        {
          id: 1,
          course_name: "中国传统文化",
          course_teacher: "马冬梅",
          course_time: "每周二、四 8:00-10:00",
          course_place: "良才楼201",
          isactive:false
        },{
          id: 2,
          course_name: "中国古诗词",
          course_teacher: "林俞",
          course_time: "每周三、四 8:00-10:00",
          course_place: "良才楼201",
          isactive:false
        },{
          id: 3,
          course_name: "中国文化",
          course_teacher: "王建军",
          course_time: "每周三 8:00-10:00",
          course_place: "良才楼301",
          isactive:false
        },
      ],
    };
  },

  methods: {
    submit(){
      console.log(this.course_sum);
    },
    a(val,index){
      console.log(val);
      console.log(index);
    }
  },
};
</script>


<style>
.submit{
  float: right;
  margin-top: 15px;
}
</style>
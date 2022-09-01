<template>
  <div>
    <class-table
      @edit="handleEdit"
      @remove="handleRemove"
      @add="handleAdd"
      :modelValue="courses"
    ></class-table>
  </div>
</template>

<script>
import ClassTable from "../ClassTable/ClassTable.vue";
export default {
  components: { ClassTable },
  data() {
    return {
      courses: [
        {
          name: "早读",
          day: 4,
          start: [6, 0],
          end: [6, 45],
        },
      ],
    };
  },
  methods: {
    handleAdd(part) {
      console.log(part);
    },
    handleEdit(index, data) {
      console.log(index, data);
    },
    handleRemove(index, data) {
      this.courses.splice(index, 1);
      console.log(data)
    },
  },
};
</script>

<style>
</style>
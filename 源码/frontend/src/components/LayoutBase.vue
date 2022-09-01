<template>
  <div>
    <el-container>
      <el-aside width="200px">
        <el-row class="tac">
          <el-col :span="12">
            <nav-menu :menus="menus" :isCollapse="isCollapse"></nav-menu>
          </el-col>
        </el-row>
      </el-aside>
      <el-container>
        <el-header>
          <header-info @child-change="countChange"></header-info>
        </el-header>
        <el-main>
          <router-view></router-view>
        </el-main>
        <el-footer>
          <footer-info></footer-info>
        </el-footer>
      </el-container>
    </el-container>
  </div>
</template>
<script>
import FooterInfo from "./FooterInfo.vue";
import HeaderInfo from "./HeaderInfo.vue";
import NavMenu from "@/components/nav/NavMenu.vue";
import routes from "../router/routes";
export default {
  components: {
    FooterInfo,
    HeaderInfo,
    NavMenu,
  },
  data: function () {
    return {
      isCollapse: false,
    };
  },
  methods: {
    countChange(isCollapse) {
      this.isCollapse = isCollapse;
    },
    handleOpen(key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath);
    },
    // 处理路由定义中的想要在菜单中隐藏的项-第二版
    routesToMenus(routes, path) {
      let arr = [];
      routes.forEach((item) => {
        // 判断当前元素是否需要在菜单中隐藏
        if (item.meta.hidden && item.meta.hidden === true) {
          // 当前元素有children节点，如果有则升格children节点，否则不做其它
          if (item.children && item.children.length > 0) {
            let tmpArr = this.routesToMenus(item.children, item.path);
            arr = arr.concat(tmpArr);
          }
        } else {
          // 这个路径的处理，必须在调用递归方法之前，才会保存最上一层的路径
          if (path) {
            if (path === "/") {
              item.path = path + item.path;
            } else {
              item.path = path + "/" + item.path;
            }
          }
          // 处理当有children节点的时候，递归调用自己
          if (item.children && item.children.length > 0) {
            // console.log(item.path);
            item.children = this.routesToMenus(item.children, item.path);
          }
          arr.push(item);
        }
      });
      // console.log(arr);
      return arr;
    },
    // 处理路由定义中的想要在菜单中隐藏的项-第一版
    processRoutes(routes) {
      /*  
        1、遍历定义的路由
        2、判断当前元素/节点是否需要隐藏，是的话，再判断它是否具有children节点，并且children节点有数据
        3、接上一步，将其children下的元素都往上提一层（升格）
        4、将其装入新的数组
      */
      let arr = [];
      routes.forEach((item) => {
        if (
          item.meta.hidden &&
          item.meta.hidden === true &&
          item.children &&
          item.children.length > 0
        ) {
          item.children.forEach((subItem) => {
            arr.push(subItem);
          });
        } else {
          if (!item.meta.hidden || item.meta.hidden === false) {
            arr.push(item);
          }
        }
      });
      return arr;
    },
  },
  created() {
    
  },
  computed: {
    menus: function () {
      // console.log(routes);
      let list = this.routesToMenus(routes, "");
      return list;
    },
  },
};
</script>
<style>
body {
  margin: 0;
  padding: 0;
  position: relative;
  height: 100vh;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  font-size: 14px;
  background: #f6f8f9;
}
.el-header {
  padding: 0px !important;
  /* #fff; */
  background: #fff;
}

.el-footer {
  background: #f6f8f9;
  border-top: 1px dashed #dcdfe6;
  color: rgba(0, 0, 0, 0.45);
  /* text-align: center;
  line-height: 60px; */
  display: flex;
  justify-content: center;
  align-items: center;
}

.el-aside {
  color: #333;
  width: auto !important;
  background-color: rgb(33, 37, 43) !important;
}

.el-main {
  background: #f2f4f5;
  color: #333;
  /* text-align: center; */
  /* max-height: calc(100vh - 122px); */
  max-height: calc(100vh - 120px) !important;
}

body > .el-container {
  margin-bottom: 40px;
}

.el-container {
  /*设置内部填充为0，几个布局元素之间没有间距*/
  padding: 0px !important;
  /*外部间距也是如此设置*/
  margin: 0px !important;
  /*统一设置高度为100%*/
  height: 100vh;
}

/* .el-submenu__title :not(:first-child) {
  display: none;
} */
/* .el-submenu__title {
  width: 200px;
} */

/* .el-tooltip {
  width: 65px !important;
} */

.menuTitle {
  display: flex;
  align-items: center;
  justify-content: center;
  color: #fff;
  margin: 0px !important;
  width: 200px !important;
  height: 60px !important;
  text-align: center;
}

.el-icon-eleme {
  padding: 20px 0px;
}
.menuTitle > i {
  /* color: fffff; */
  font-size: 25px;
  /* width: 100% !important;
  text-align: center; */
}
</style>
<template>
<div>
        <template v-if="menu.children && menu.children.length > 0">
            <el-submenu :index="menu.path" :key="menu.path">
                <template slot="title">
                    <i :class="menu.meta.icon"></i>
                    <span>{{ menu.meta.title }}</span>
                </template>
                <template>
                    <nav-menu-item v-for="item in menu.children" :menu="item" :key="item.path"></nav-menu-item>
                </template>
            </el-submenu>
        </template>
        <template v-else>
            <el-menu-item :index="menu.path" :key="menu.path">
                <i :class="menu.meta.icon"></i>
                <span slot="title">{{ menu.meta.title }}</span>
            </el-menu-item>
        </template>
    </div>
</template>
<script>
export default {
    name: "NavMenuItem",
    props: ["menu"],
};
</script>

<style>

.sideNavMenu.el-submenu{
  width: 200px
}
.el-menu-item.sideNavMenu{
  width: 200px;
}
</style>
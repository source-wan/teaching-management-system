<template>
    <div class="breadDiv" id="domBread">
        <el-breadcrumb separator="/">
            <el-breadcrumb-item v-for="(item, index) in breadList" :to="item.path" :key="index">{{ item.meta.title }}
            </el-breadcrumb-item>
        </el-breadcrumb>
    </div>
</template>

<script>
export default {
    data() {
        return {
            breadList: []
        }
    },
    watch: {
        $route: {
            handler(route) {
                let allList = route.matched.filter(item => {
                    if (item.meta && item.meta.title) {
                        if (item.redirect) {
                            item.path = ''
                        }
                        return true
                    }
                })
                if (allList[0].path !== '/' && allList[0].path !== '/dashbord') {
                    allList.unshift({ path: '/', meta: { title: '仪表盘' } })
                }
                this.breadList = allList
            },
            immediate: true
        }
    }
}
</script>
<style scoped>
#domBread {
    /* widows: 60%; */
    width: 500px;
    display: flex;
    align-items: center;
    font-size: 16px;
}

/* .el-breadcrumb {
    float: left;
    line-height: 2;
} */
</style>
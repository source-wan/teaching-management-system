<template>
    <div class="sideCollapse" id="domColapse">
        <i :class="{ 'el-icon-s-unfold': opened, 'el-icon-s-fold': !opened }" @click="toggleOpen()"></i>
    </div>
</template>

<script>
export default {
    computed: {
    },
    data() {
        return {
            opened: false
        }
    },
    methods: {
        toggleOpen() {
            this.opened = !this.opened;
            this.$emit('child-change', this.opened);
        },
    }
}
</script>
<style scoped>
.sideCollapse>i {
    margin-top: 20px;
    font-size: 20px;
    color: #363f44;
    cursor: pointer;
}

#domColapse {
    padding: 0px 12px;
    height: 100%;
}
</style>
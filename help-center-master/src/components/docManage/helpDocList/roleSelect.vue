<template>
    <el-select v-model="currentValue" multiple placeholder="请选择角色"  @change="onChange">
        <el-option
            v-for="item in role"
            :key="item.roleId"
            :label="item.roleName"
            :value="item.roleId">
        </el-option>
    </el-select>
</template>

<script>
    /**
     *
     * @Author Administrator
     * @Date 2018-11-09 15:34.
     */

    export default {
        props:{
            value:{
                required: true
            }
        },
        mounted() {
            this.loadSelectData();
        },
        components: {},
        data() {
            return {
                role:[],
                currentValue:this.value
            }
        },
        computed: {},
        methods: {
            onChange: function (val) {
                this.$emit('input', val);
            },
            /**
             * @Type:数据
             * @Position:下拉框
             * @Description:获取所有角色
             */
            loadSelectData(){
                this.$axios.get('api/Role/getAllList')
                    .then((res)=>{
                        if(!res.data.result){
                            this.$message.info(res.data.tips)
                        }else {
                            this.role = res.data.data
                            console.log(this.role);
                        }
                    })
                    .catch(function (error) {
                        console.log('下拉框异常');
                        console.log(error);
                    });
            },
        },
        watch: {
            value:function (val) {
                this.currentValue=val;
            }
        },
        filters: {},
        beforeDestroy() {

        }
    }
</script>

<style scoped>

</style>







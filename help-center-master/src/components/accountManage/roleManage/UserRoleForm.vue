<template>
    <el-form ref="form" :model="uRForm" label-width="70px">
        <el-form-item label="角色">
            <el-input v-model="uRForm.roleName"></el-input>
        </el-form-item>
        <el-form-item label="角色描述">
            <el-input type="textarea" v-model="uRForm.roleRemark"></el-input>
        </el-form-item>
        <el-form-item label="状态">
            <el-radio-group v-model="uRForm.roleState">
                <el-radio :label="1">启用</el-radio>
                <el-radio :label="0">禁用</el-radio>
            </el-radio-group>
        </el-form-item>

        <el-form-item style="text-align: right;">
            <el-button @click="urClose">取 消</el-button>
            <el-button type="primary" @click="onSubmit">确 定</el-button>
        </el-form-item>

    </el-form>
</template>

<script>

    /**
     *  用户管理添加/修改表单
     */
    export default {
        props: {
            userRoleForm:{},
            formProps: {
                type: Object,
                default() {
                    return {
                        method: undefined,
                    }
                }
            }
        },
        mounted() {
        this.loadRowData();
        },
        data() {
            return {
                uRForm: {}
            }
        },
        methods: {
            /**
             * @Type:数据
             * @Position:表格行修改
             * @Description: 查询当前行，获取修改框的默认值
             */
            loadRowData(id){
                let Id = id||this.formProps.roleId;
                this.$axios.get('/api/Role',{
                    params:{
                        roleId:Id
                    }
                   })
                    .then((res)=>{
                        if (res.data.result) {
                            this.uRForm = res.data.data;
                            console.log(this.uRForm);
                        }
                    })
                    .catch(function (error) {
                        console.log('DepartPeopleFrom的loadRowData方法catch到异常了');
                        console.log(error);
                    });

            },

            onSubmit() {
                if(this.formProps.methods ==='post'){
                    let _this = this;
                    let param = this.uRForm;

                    _this.$axios.post('/api/Role',param)
                        .then(res => {
                            let data = res.data
                            if (data.result == false) {
                                _this.$message.error(data.tips);
                            } else {
                                _this.$message({//添加成功
                                    message:data.tips,
                                    type: 'success'
                                });
                                this.$emit('UrFormAddSuccess');
                            }
                        })
                        .catch(function (error) {
                            console.log(error);
                        });
                }
                if(this.formProps.methods ==='put'){
                    let _this = this;
                    _this.$axios.put('api/Role'+"?roleId="+this.formProps.roleId,this.uRForm)
                        .then(res => {
                            let data = res.data
                            if (data.result == false) {
                                _this.$message.error(data.tips);
                                this.$emit('UrFormAddSuccess');
                            } else {
                                _this.$message({
                                    message:res.data.tips,
                                    type: 'success'
                                });
                                this.$emit('UrFormAddSuccess');
                            }
                        })
                        .catch(function (error) {
                            console.log(error);
                        });

                }
            },

            /*新增提交*/
            onUrfSubmit: function () {
//                let _this = this;
//                let param = this.uRForm;
//                _this.$axios.post('/api/Role',param)
//                    .then(res => {
//                        let data = res.data
//                        if (data.result == false) {
//                            _this.$message.error(data.tips);
//                        } else {
//                            _this.$message({//添加成功
//                                message:data.tips,
//                                type: 'success'
//                            });
//                            this.$emit('UrFormAddSuccess');
//                        }
//                    })
//                    .catch(function (error) {
//                        console.log(error);
//                    });

            },
          /*修改提交*/
            modifyUrfSubmit: function () {
//                let _this = this;
//                _this.$axios.put('api/Dept'+"?deptId="+this.departForm.deptId,this.form)
//                    .then(res => {
//                        let data = res.data
//                        if (data.result == false) {
//                            _this.$message.error(data.tips);
//                        } else {
//                            _this.$message({
//                                message:res.data.tips,
//                                type: 'success'
//                            });
//                            this.$emit('departFormSubmitSuccess');
//                        }
//                    })
//                    .catch(function (error) {
//                        console.log(error);
//                    });

            },
            /*查询当前修改行的值，修改框的默认值*/
            URloadData(id){
//                let Id = id||this.departForm.deptId;
//                this.$axios.get('/api/Dept',{
//                    params:{
//                        deptId:Id
//                    }
//                }).then((res)=>{
//                    this.form = res.data.data.detail
//                })
            },

            urClose() {
                this.$emit('urClose')
            },
            /**
             * 表单重置
             */
            resetForm() {
                this.$refs.form.resetFields();
            },
        },
        watch:{
           /* ['userForm.role']:function (val) {
                this.userForm.funIds = []
            }*/
        }
    };

</script>

<style scoped>
    .toolbar {
        float: right;
    }

</style>

<template>
    <div class="depart-form">
        <el-form label-width="80px" ref="form" :model="userManageForm2" :rules="rules">
            <el-form-item label="手机号"  prop="usrPhoneNum">
                <el-input v-model="userManageForm2.usrPhoneNum"  placeholder="请输入手机号"></el-input>
            </el-form-item>
            <el-form-item label="账号"  prop="usrAccount">
                <el-input v-model="userManageForm2.usrAccount"  placeholder="请输入账号"></el-input>
            </el-form-item>
            <el-form-item label="密码"  prop="usrPsw">
                <el-input v-model="userManageForm2.usrPsw"  placeholder="请输入登录密码"></el-input>
            </el-form-item>
            <el-form-item label="昵称"  prop="usrName">
                <el-input v-model="userManageForm2.usrName"  placeholder="请输入昵称"></el-input>
            </el-form-item>
            <el-form-item label="角色" prop="roleIdList">
                <xy-user-manage-role-select
                    @input="getUserRole"
                    :value="userManageForm2.roleIdList"
                    ref="userRole"
                ></xy-user-manage-role-select>
            </el-form-item>
            <el-form-item label="状态"  prop="usrState">
                <el-radio-group v-model="userManageForm2.usrState">
                    <el-radio :label="1">启用</el-radio>
                    <el-radio :label="0">禁用</el-radio>
                </el-radio-group>
            </el-form-item>

            <!--表单不要填，但是接口需要，否则就返回错误-->
            <el-form-item label="用户类型"  prop="usrType" style="display: none;">
                <el-input v-model="userManageForm2.usrType"></el-input>
            </el-form-item>
            <el-form-item label="用户部门id"  prop="UsrDeptId"  style="display: none">
                <el-input v-model="userManageForm2.UsrDeptId"></el-input>
            </el-form-item>
            <el-form-item label="审核状态"  prop="usrVerifyState"  style="display: none">
                <el-radio-group v-model="userManageForm2.usrVerifyState">
                    <el-radio label="1">审核通过</el-radio>
                    <el-radio label="0">审核不通过</el-radio>
                </el-radio-group>
            </el-form-item>

            <el-form-item style="text-align: right;">
                <el-button @click="umClose">取 消</el-button>
                <el-button type="primary" @click="onSubmit">确 定</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>


    import xyUserManageRoleSelect from './UserManageRoleSelect.vue'


    /**
     *  用户管理添加/修改表单
     */
    export default {
        props: {
            userManageForm:{
                type:Object
            },
            formProps: {
                type: Object,
                default() {
                    return {
                        method: undefined,
                    }
                }
            }
//            preSelectData:'',
        },
        mounted() {
            this.loadData();
        },
        components: {
            xyUserManageRoleSelect
        },
        data() {
            return {
                userManageForm2:{
                    "usrPhoneNum":'',
                    "usrAccount": "",
                    "usrPsw": "",
                    "usrName": "",
                    "roleIdList": [
                    ],
                    "usrType": 2,
                    "usrDeptId":"",
                    "usrVerifyState": 1,
                    "usrState": 1,
                },
                umfOptions: [],
                role:'',
                rules: {
                    usrPhoneNum: [
                        { required: true, message: '请输入手机号', trigger: 'blur' }
                    ],
                    usrAccount: [
                        { required: true, message: '请输入账号', trigger: 'blur' }
                    ],
                    usrPsw: [
                        { required: true, message: '请输入密码', trigger: 'blur' }
                    ],
                    usrName: [
                        { required: true, message: '请输入昵称', trigger: 'blur' }
                    ]
                }
            }
        },
        methods: {
            /**
             * @Type:数据
             * @Position:表格行修改
             * @Description: 查询当前行，获取修改框的默认值
             */
            loadData(id){
                let _this=this;
                let Id = id||_this.formProps.usrId||1;

                this.$refs.userRole.loadSelectData();

                _this.$axios.get('/api/User',{
                    params:{
                        usrId:Id
                    }
                })
                    .then((res)=>{
                        if (res.data.result) {
                            _this.userManageForm2 = res.data.data;
                            _this.userManageForm2.roleIdList=res.data.data.usrRoleIdLst
//                            debugger
//                            this.$axios.get('/api/UserRole',{
//                                params:{
//                                    userId:Id
//                                }
//                            }).then((res)=>{
//                                if(!res.data.result){
//                                    this.$message.info(res.data.tips)
//                                }else {
//                                    this.userManageForm2.roleIdList.push(res.data.data[0].roleId);
//                                }
//                            })
                        }
                    })
                    .catch(function (error) {
                        console.log('DepartPeopleFrom的loadRowData方法catch到异常了');
                        console.log(error);
                    });

            },

            /*获取角色select*/
            getUserRole(val){
                this.userManageForm2.roleIdList = val
            },
            /**
             * @Type:提交
             * @Description:新增和修改
             */
            onSubmit() {
                if(this.formProps.methods ==='post'){
                    let _this = this;
                    let param = this.userManageForm2;
                    this.$refs.form.validate((valid) => {
                        if (valid) {
                            _this.$axios.post('api/User',param)
                                .then(res => {
                                    let data = res.data
                                    if (data.result == false) {
                                        _this.$message.error(data.tips);
                                    } else {
                                        _this.$message(data.tips); //添加成功
                                    }

                                    _this.$emit('modifyUmfSuccess');//接收从父组件传过来的方法
                                })
                                .catch(function (error) {
                                    console.log(error);
                                });
                        } else {
                            _this.$message.error('添加失败');
                            return false;
                        }
                    });
                }
                if(this.formProps.methods ==='put'){
                    let _this = this;

                    console.log(this.userManageForm2);
//                    debugger
//                    return

                    _this.$axios.put('api/User'+"?UsrId="+this.formProps.usrId,this.userManageForm2)
                        .then(res => {
                            let data = res.data
                            if (data.result == false) {
                                _this.$message.error(data.tips);
                            } else {
                                _this.$message({
                                    message:res.data.tips,
                                    type: 'success'
                                });
                                _this.$emit('modifyUmfSuccess');
                            }
                        })
                        .catch(function (error) {
                            console.log(error);
                        });

                }
            },


            umClose() {
                this.$emit('umClose')
            },
            /**
             * 表单重置
             */
            resetForm() {
                this.$refs.form.resetFields();
            },
        },
        watch:{
        }
    };

</script>

<style scoped lang="less">
    .depart-form{
    .depart-el-select {
        width: 100%;
    }
    }

</style>

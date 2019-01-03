<template>
    <div class="depart-form">
        <el-form label-width="80px" ref="form" :model="dpfForm" :rules="dpfRules">
            <el-form-item label="部门名称" prop="usrDeptId">
                <el-input v-model="dpfForm.usrDeptId" style="display: none"></el-input>
                <xy-depart-tree-select
                    @transferDTSelect="getTreeSelectData"
                    :departMsg="departMsg"
                ></xy-depart-tree-select>

            </el-form-item>
            <el-form-item label="人员名称" prop="usrName">
                <el-input v-model="dpfForm.usrName"  placeholder="请输入名字"></el-input>
            </el-form-item>
            <el-form-item label="账号" prop="usrAccount">
                <el-input v-model="dpfForm.usrAccount" placeholder="请输入登录账号"></el-input>
            </el-form-item>
            <el-form-item label="密码" prop="usrPsw">
                <el-input v-model="dpfForm.usrPsw"  placeholder="请输入登录密码"></el-input>
            </el-form-item>

            <el-form-item label="人员类型" prop="usrType">
                <el-radio-group v-model="dpfForm.usrType">
                    <el-radio :label="2">正式人员</el-radio>
                    <el-radio :label="3">试用人员</el-radio>
                </el-radio-group>
            </el-form-item>

            <el-form-item label="审核状态" prop="usrVerifyState">
                <el-radio-group v-model="dpfForm.usrVerifyState">
                    <el-radio :label="1">审核通过</el-radio>
                    <el-radio :label="0">审核不通过</el-radio>
                </el-radio-group>
            </el-form-item>


            <!--表单不要填，但是接口需要，否则就返回错误-->
            <el-form-item label="电话" prop="usrPhoneNum" style="display: none">
                <el-input v-model="dpfForm.usrPhoneNum"></el-input>
            </el-form-item>
            <el-form-item label="角色数组" prop="roleIdList" style="display: none">
                <el-input v-model="dpfForm.roleIdList"></el-input>
            </el-form-item>
            <el-form-item label="用户状态" prop="usrState" style="display: none">
                <el-input v-model="dpfForm.usrState"></el-input>
            </el-form-item>

            <el-form-item style="text-align: right;">
                <el-button @click="dpfClose">取 消</el-button>
                <el-button type="primary" @click="onSubmit">确 定</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>


    import xyDepartTreeSelect from '@/components/departManage/DepartTreeSelect.vue';


    /**
     *  用户管理添加/修改表单
     */
    export default {
        props: {
            departPeopleForm:{
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
            this.loadRowData();
            this.getTreeSelectData();
        },
        components: {
            xyDepartTreeSelect
        },
        data() {
            return {
                dpfForm:{
                "usrAccount": "",
                "usrDeptId":"",
                "usrName": "",
                "usrPsw": "",
                "usrType": 2,
                "usrVerifyState": 1,

                 "usrPhoneNum": 1,
                 "roleIdList": [
                    "string"
                  ],
                  "usrState": 0,
                 },
                treeSelectData:'',
                treeSelectName:'',
                departMsg:'',
                dpfRules:{
                    usrName: [
                        { required: true, message: '请输入名字', trigger: 'blur' }
                    ],
                    usrAccount: [
                        { required: true, message: '请输入登录账号', trigger: 'blur' }
                    ],
                    usrPsw: [
                        { required: true, message: '请输入登录密码', trigger: 'blur' }
                    ],
                    usrType: [
                        { required: true, message: '请选择人员类型', trigger: 'blur' }
                    ],
                    usrVerifyState: [
                        { required: true, message: '请选择审核状态', trigger: 'blur' }
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
            loadRowData(id){
                let Id = id||this.formProps.usrid||1;
//             debugger
                this.$axios.get('/api/User',{
                    params:{
                        usrId:Id
                    }
                })
                .then((res)=>{
                    if (res.data.result) {
                        this.dpfForm = res.data.data;
                        this.departMsg = res.data.data.usrDeptId;
                    }
                })
                .catch(function (error) {
                    console.log('DepartPeopleFrom的loadRowData方法catch到异常了');
                    console.log(error);
                });

            },

            /**
             * @Type:数据-接值
             * @Description:接受treeSelect子组件传过来的数据
             */
            getTreeSelectData(msg){
                this.treeSelectData = msg;
                console.log(msg);
            },

            /**
             * @Type:提交
             * @Description:新增和修改
             */
            onSubmit() {
                if(this.formProps.methods ==='post'){
                    let _this = this;
                    _this.dpfForm.usrDeptId = _this.treeSelectData;
                    let param = _this.dpfForm;

                    this.$refs.form.validate((valid) => {
                        if (valid) {
                            _this.$axios.post('api/User',param)
                                .then(res => {
                                    console.log(res);
                                    let data = res.data
                                    if (data.result == false) {
                                        _this.$message.error(data.tips);
                                    } else {
                                        _this.$message(data.tips); //添加成功
                                    }

                                    this.$emit('dpfSubmitSuccess');//接收从父组件传过来的方法
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

                    if(_this.treeSelectData==undefined){
                        _this.dpfForm.usrDeptId= this.departMsg;
                    }else{
                        _this.dpfForm.usrDeptId = _this.treeSelectData;
                    }

                    _this.$axios.put('api/User'+"?UsrId="+this.formProps.usrid,this.dpfForm)
                        .then(res => {
                            let data = res.data
                            if (data.result == false) {
                                _this.$message.error(data.tips);
                            } else {
                                _this.$message({
                                    message:res.data.tips,
                                    type: 'success'
                                });
                                this.$emit('dpfSubmitSuccess');
                            }
                        })
                        .catch(function (error) {
                            console.log(error);
                        });

                }
            },


            dpfClose() {
                this.$emit('dpfClose')
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

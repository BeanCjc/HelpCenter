<template>
    <div>
        <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="0px" class="ms-content">
            <el-form-item prop="usrAccount">
                <el-input v-model="ruleForm.usrAccount" placeholder="请输入账号" @keyup.enter.native="submitForm('ruleForm')">
                    <!--<el-button slot="prepend" icon="el-icon-lx-people"></el-button>-->
                </el-input>
            </el-form-item>
            <el-form-item prop="usrDeptId">
                <!--<el-input placeholder="请输入部门" v-model="ruleForm.nickname" @keyup.enter.native="submitForm('ruleForm')">-->
                    <!--<el-button slot="prepend" icon="el-icon-lx-lock"></el-button>-->
                <!--</el-input>-->
                <xy-tree-select
                    v-model="selectValue"
                    :options="options"
                    :normalizer="normalizer"
                    :load-options="dfLoadOptions"
                    :auto-load-root-options="loading"
                    placeholder="请选择部门"
                >
                </xy-tree-select>


            </el-form-item>
            <el-form-item prop="usrName">
                <el-input placeholder="请输入昵称" v-model="ruleForm.usrName" @keyup.enter.native="submitForm('ruleForm')">
                    <!--<el-button slot="prepend" icon="el-icon-lx-lock"></el-button>-->
                </el-input>
            </el-form-item>
            <el-form-item prop="usrPsw">
                <el-input placeholder="请输入密码" v-model="ruleForm.usrPsw" @keyup.enter.native="submitForm('ruleForm')">
                    <!--<el-button slot="prepend" icon="el-icon-lx-lock"></el-button>-->
                </el-input>
            </el-form-item>
            <!--<el-form-item prop="password2">-->
                <!--<el-input placeholder="请再次输入密码" v-model="ruleForm.password2" @keyup.enter.native="submitForm('ruleForm')">-->
                    <!--&lt;!&ndash;<el-button slot="prepend" icon="el-icon-lx-lock"></el-button>&ndash;&gt;-->
                <!--</el-input>-->
            <!--</el-form-item>-->


            <!--表单不要填，但是接口需要，否则就返回错误-->
            <el-form-item label="电话" style="display: none">
                <el-input v-model="ruleForm.usrPhoneNum"></el-input>
            </el-form-item>
            <!--<el-form-item label="角色数组" style="display: none">-->
                <!--<el-input v-model="ruleForm.roleIdList"></el-input>-->
            <!--</el-form-item>-->
            <el-form-item label="用户状态" style="display: none">
                <el-input v-model="ruleForm.usrState"></el-input>
            </el-form-item>
            <el-form-item label="审核状态"style="display: none">
                <el-radio-group v-model="ruleForm.usrVerifyState">
                    <el-radio :label="1">审核通过</el-radio>
                    <el-radio :label="0">审核不通过</el-radio>
                </el-radio-group>
            </el-form-item>
            <el-form-item label="人员类型"style="display: none">
                <el-radio-group v-model="ruleForm.usrType">
                    <el-radio :label="2">正式人员</el-radio>
                    <el-radio :label="3">试用人员</el-radio>
                </el-radio-group>
            </el-form-item>


            <div class="login-btn">
                <el-button type="primary" @click="submitForm('ruleForm')">注册</el-button>
            </div>
            <!--<p class="login-tips">Tips : 这里用于错误提示。</p>-->
        </el-form>
    </div>
</template>

<script>

    import TreeSelect from '@riophae/vue-treeselect';
    import '@riophae/vue-treeselect/dist/vue-treeselect.css';

    export default {
        data() {
            return {
                selectValue: null,
                loading: true,
                options:[],
                normalizer(node) {
                    return {
                        id: node.deptId,
                        label: node.deptName,
                        children: node.child,
                    }
                },
                ruleForm: {
                    "usrPhoneNum": "string",
                    "usrAccount": "",
                    "usrPsw": "",
                    "usrType": 0,
                    "usrName": "",
                    "usrDeptId": "",
                    "roleIdList": [
                        "string"
                    ],
                    "usrState": 0,
                    "usrVerifyState": 0
                },
                rules: {
                    usrAccount: [
                        { required: true, message: '请输入账号', trigger: 'blur' }
                    ],
                    usrName: [
                        { required: true, message: '请输入昵称', trigger: 'blur' }
                    ],
                    usrPsw: [
                        { required: true, message: '请输入密码', trigger: 'blur' }
                    ]
                }
            }
        },
        mounted() {
            this.dfTreeData();
        },
        components: {
            xyTreeSelect: TreeSelect,
        },
        methods: {
            /**
             * @Type:数据
             * @Position:下拉框
             * @Description:树初始化
             */
            dfTreeData() {
                this.$axios.get('/api/Dept/toplist')
                    .then((res) => {
                        let resTemp = res.data
                        if (resTemp.result === false) {
                            _this.$message.error(resTemp.tips);
                        } else {
                            if(resTemp.data.length !== 0){
                                resTemp.data.forEach(item => {
                                    if (item.child) {
                                        item.child = null
                                    }
                                });
                            }else {
                                resTemp.data.child = undefined
                            }

                        }
                        this.options = resTemp.data;
                    })
            },

            /**
             * @Type:数据
             * @Position:下拉框
             * @Description:树子节点
             */
            dfLoadOptions({ action, parentNode, callback }) {
                if (action === 'LOAD_CHILDREN_OPTIONS') {
                    let _this = this;
                    let params = {
                        deptId:parentNode.deptId
                    };
                    _this.$axios.get('api/Dept',{
                        params: params,
                    }).then((res) => {
                        let resTemp = res.data;
                        console.log(res);

                        if (resTemp.result === false) {
                            _this.$message.error(res.msg);
                        } else {
                            if(resTemp.data.childNodes.length !== 0){

                                parentNode.child = resTemp.data.childNodes;
                                parentNode.child.forEach(item => {
                                    if (item.child) {
                                        item.child = null
                                    }
                                });

                            }else {
                                parentNode.child = undefined
                            }
                            callback()
                        }
                    }).catch(function (error) {
                        console.log(error);
                    });
                }

            },
            submitForm(formName) {
                let _this = this;
                _this.ruleForm.usrDeptId = _this.selectValue;
                let param = _this.ruleForm;
                this.$refs[formName].validate((valid) => {
                    if (valid) {
                        _this.$axios.post('api/User',param)
                            .then(res => {
                                console.log(res);
                                let data = res.data
                                if (data.result == false) {
                                    _this.$message.error(data.tips);
                                    _this.ruleForm = [];
                                    _this.selectValue = [];

                                } else {
                                    _this.$message(data.tips); //添加成功
                                    _this.$router.replace('/login');
                                    _this.ruleForm = [];
                                    _this.selectValue = [];
                                }
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
        },
    }
</script>

<style lang="less" >
    .ms-content{
        padding: 30px 30px;
    }
    .login-btn{
        text-align: center;
    }
    .login-btn button{
        width:100%;
        height:36px;
        margin-bottom: 10px;
    }
    .login-tips{
        font-size:12px;
        line-height:30px;
        color:#fff;
    }
    .iconBtn{
        width: 47px;
        height: 31px;
    }
    .el-select>.el-input{
        width: 243px;
        margin-left: -5px;
    }
</style>

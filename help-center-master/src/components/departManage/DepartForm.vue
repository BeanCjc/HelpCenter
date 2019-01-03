<template>
    <div class="depart-form">
        <el-form label-width="120px" ref="form" :model="dfForm" :rules="dfRules">
            <el-form-item label="上级部门名称"  prop="preDeptId">
                <el-input v-model="dfForm.preDeptId" style="display: none"></el-input>
                <xy-depart-tree-select
                    @transferDTSelect="getTreeSelectData"
                    @dtreeSelect="dTreeRowData"
                    :departMsg="departMsg"
                ></xy-depart-tree-select>
            </el-form-item>
            <el-form-item label="部门名称"  prop="deptName">
                <el-input  v-model="dfForm.deptName"  placeholder="请输入部门名称"></el-input>
            </el-form-item>
            <el-form-item label="部门账号"  prop="deptAccount">
                <el-input  v-model="dfForm.deptAccount" placeholder="请输入部门登录账号"></el-input>
            </el-form-item>
            <el-form-item label="部门密码" prop="deptPsw">
                <el-input  v-model="dfForm.deptPsw"  placeholder="请输入部门登录密码"></el-input>
            </el-form-item>


            <el-form-item style="text-align: right;">
                <el-button @click="dfClose">取 消</el-button>
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
            departForm:{
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
            this.getTreeSelectData();
        },
        components: {
            xyDepartTreeSelect
        },
        data() {
            return {
                departMsg:'',
                dfForm:{
                    deptName:'',
                    deptAccount:'',
                    deptPsw:''
                },
                preSelectData:'',
                preSelectName:'',
                dfRules: {
                    deptName: [
                        { required: true, message: '请输入部门名称', trigger: 'blur' }
                    ],
                    deptAccount: [
                        { required: true, message: '请输入部门账号', trigger: 'blur' }
                    ],
                    deptPsw: [
                        { required: true, message: '请输入密码', trigger: 'blur' }
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
                let Id = id||this.formProps.deptId;
                this.$axios.get('/api/Dept',{
                    params:{
                        deptId:Id
                    }
                    })
                    .then((res)=>{
                        if (res.data.result) {
                            this.dfForm = res.data.data.detail;
                            this.departMsg = res.data.data.detail.preDeptId;//上级部门分类
//                            this.preSelectName = res.data.data.detail.deptName;
                        }
                     })
                    .catch(function (error) {
                        console.log('DepartFrom的loadData方法catch到异常了');
                        console.log(error);
                    });

            },

            dTreeRowData(){
                let Id =this.preSelectData;
                this.$axios.get('/api/Dept',{
                    params:{
                        deptId:Id
                    }
                })
                    .then((res)=>{
                        if (res.data.result) {
                            this.preSelectName = res.data.data.detail.deptName;
                        }
                    })
                    .catch(function (error) {
                        console.log('DepartFrom的loadData方法catch到异常了');
                        console.log(error);
                    });

            },


            /**
             * @Type:数据-接值
             * @Description:接受treeSelect子组件传过来的数据
             */
            getTreeSelectData(msg){
                this.preSelectData = msg;
                console.log(msg);
            },

            /**
             * @Type:提交
             * @Description:新增和修改
             */
            onSubmit() {
                if(this.formProps.methods ==='post'){
                    let _this = this;
                    let param = this.dfForm;
                    this.dfForm.preDeptId = this.preSelectData;
                    if(this.dfForm.preDeptId===undefined){
                        this.$message({
                            message: '请输入上级部门',
                            type: 'warning'
                        });
                        return
                    }
                    if(this.preSelectName === this.dfForm.deptName){
                        this.$message({
                            message: '上级部门不能与部门相同',
                            type: 'warning'
                        });
                        return
                    }

                    this.$refs.form.validate((valid) => {
                        if (valid) {
                            _this.$axios.post('api/Dept',param)
                                .then(res => {
                                    let data = res.data;
                                    if (data.result == false) {
                                        _this.$message.error(data.tips);
                                    } else {
                                        _this.$message(data.tips); //添加成功
                                    }

                                    _this.$emit('departFormSubmitSuccess');//接收从父组件传过来的方法
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
                    if( this.preSelectData==undefined){
                        this.dfForm.preDeptId = this.departMsg;
                    }else{
                        this.dfForm.preDeptId = this.preSelectData;
                    }
                    console.log(this.dfForm);
                    _this.$axios.put('api/Dept'+"?deptId="+this.formProps.deptId,this.dfForm)
                        .then(res => {
                            let data = res.data;
//                            debugger
                            if (data.result == false) {
                                _this.$message.error(data.tips);
                            } else {
                                _this.$message({
                                    message:res.data.tips,
                                    type: 'success'
                                });
                                this.$emit('departFormSubmitSuccess');
                            }
                        })
                        .catch(function (error) {
                            console.log(error);
                        });
                }
            },


            dfClose() {
                this.$emit('dfClose')
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

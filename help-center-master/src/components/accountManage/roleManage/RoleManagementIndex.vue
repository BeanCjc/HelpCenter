<template>
    <div class="table">
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-lx-calendar"></i> 账户管理</el-breadcrumb-item>
                <el-breadcrumb-item>角色管理</el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="container">
            <div class="handle-box">
                <el-button type="primary" icon="add" class="handle-add" @click="addRm()">新增</el-button>
                <!--<el-button type="primary" icon="add" class="handle-add" @click="add()">授权</el-button>-->
            </div>
            <xy-user-role-table
                ref="rmTable"
                :userRoleForm="userRoleForm"
                @rmEvent="getUrtData"
                @modifyVisibleChange = "modifyVisibleFun($event)"
                @resetVisibleChange = "resetVisibleFun($event)"
            ></xy-user-role-table>
        </div>

        <!--弹出框-->
        <el-dialog :title="tip" :visible.sync="addVisible" width="30%">
            <xy-user-role-form
                ref="urForm"
                :formProps="formProps"
                :userRoleForm="userRoleForm"
                @UrFormAddSuccess="UrFormAddSuccess"
                @urClose="urClose"
            ></xy-user-role-form>
        </el-dialog>


        <!-- 删除提示框 -->
        <el-dialog title="提示" :visible.sync="delVisible" width="300px" center>
            <div class="del-dialog-cnt">删除不可恢复，是否确定删除？</div>
            <span slot="footer" class="dialog-footer">
                <el-button @click="delVisible = false">取 消</el-button>
                <el-button type="primary" @click="deleteRow">确 定</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script>

    import xyUserRoleForm from '@/components/accountManage/roleManage/userRoleForm.vue';
    import xyUserRoleTable from '@/components/accountManage/roleManage/userRoleTable.vue';

    export default {
        name: 'roleMange',
        data() {
            var validatePass = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('请输入密码'));
                } else {
                    if (this.ruleForm2.checkPass !== '') {
                        this.$refs.ruleForm2.validateField('checkPass');
                    }
                    callback();
                }
            };
            var validatePass2 = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('请再次输入密码'));
                } else if (value !== this.ruleForm2.pass) {
                    callback(new Error('两次输入密码不一致!'));
                } else {
                    callback();
                }
            };

            return {
                formProps: {
                    method:'put'
                },
                tip: '',
                tableData: [],
                cur_page: 1,
                multipleSelection: [],
                select_cate: '',
                select_word: '',
                del_list: [],
                is_search: false,
                modifyVisible: false,
                addVisible:false,
                resetVisible:false,
                delVisible: false,
                userRoleForm:{},
                idx: -1,
                ruleForm2: {
                    pass: '',
                    checkPass: '',
                    age: ''
                },
                rules2: {
                    pass: [
                        { validator: validatePass, trigger: 'blur' }
                    ],
                    checkPass: [
                        { validator: validatePass2, trigger: 'blur' }
                    ]
                }
            }
        },
        mounted() {

        },
        created() {
//            this.getData();
        },
        components: {
                xyUserRoleForm,
                xyUserRoleTable
        },
        computed: {
            data() {

            }
        },
        methods: {
            /**
             * @Type:打开弹层
             * @Description:新增
             */
            addRm() {
                this.tip = '新增角色';
                this.formProps.methods = 'post';
                this.addVisible = true;
                if (this.$refs.urForm){
                    this.$refs.urForm.resetForm();
                }
            },
            /**
             * @Type:打开弹层
             * @Description:修改
             */
            modifyVisibleFun(row){//接受从table来的值
                this.tip = '修改角色';
                this.formProps = row;
                this.formProps.methods = 'put';
                if (this.$refs.urForm) {
                    this.$refs.urForm.loadRowData(row.roleId);
                }
                this.addVisible = true;
            },
            /**
             * @Type:关闭弹层
             * @Description:关闭弹层,传给Form使用
             */
            urClose() {
                this.addVisible = false;
            },
            /**
             * @Type:数据
             * @Description:子组件提交修改成功重置表格数据
             */
            UrFormAddSuccess(){
                this.$refs.rmTable.loadUrtData();
                this.addVisible = false;
            },

            onUrfSubmit(){
                this.$refs.rmAdd.onUrfSubmit();
                this.addVisible = false;
            },

            resetVisibleFun(e){
                this.resetVisible = e;
            },

            // 分页导航
            handleCurrentChange(val) {
                this.cur_page = val;
//                this.getData();
            },
            // 获取 easy-mock 的模拟数据
//            getData() {
//                this.$axios.get('http://localhost:8082/static/uservuetable.json').then((res) => {
//                    //用axios的方法引入地址
//                    this.res=res
//                    this.tableData = res.data.list;
//                    //赋值
////                    console.log(res.data.list)
//                })
//            },
            getUrtData(data){
                this.userRoleForm = data;
                /*if (this.$refs.dmModify){

                }*/
            },
            search() {
                this.is_search = true;
            },
            filterTag(value, row) {
                return row.tag === value;
            },
            handleEdit(index, row) {
                this.idx = index;
                const item = this.tableData[index];
                this.userRoleForm = item;
                this.editVisible = true;
            },
            handleModify(){
                this.modifyVisible = true;
            },
            handleDelete(index, row) {
                this.idx = index;
                this.delVisible = true;
            },
            handleDisable(){

            },
            handleSelectionChange(val) {
                this.multipleSelection = val;
            },
            delAll() {
                const length = this.multipleSelection.length;
                let str = '';
                this.del_list = this.del_list.concat(this.multipleSelection);
                for (let i = 0; i < length; i++) {
                    str += this.multipleSelection[i].name + ' ';
                }
                this.$message.error('删除了' + str);
                this.multipleSelection = [];
            },

            // 确定删除
            deleteRow(){
                this.tableData.splice(this.idx, 1);
                this.$message.success('删除成功');
                this.delVisible = false;
            },
            submitForm(formName) {
                this.$refs[formName].validate((valid) => {
                    if (valid) {
                        alert('submit!');
                    } else {
                        console.log('error submit!!');
                        return false;
                    }
                });
            },
            resetForm(formName) {
                this.$refs[formName].resetFields();
            }
        }
    }

</script>

<style scoped>
    .handle-box {
        margin-bottom: 20px;
    }

    .handle-select {
        width: 120px;
    }

    .handle-input {
        width: 300px;
        display: inline-block;
    }
    .handle-add{
        padding: 10px 30px;
    }
    .del-dialog-cnt{
        font-size: 16px;
        text-align: center
    }
    .table{
        width: 100%;
        font-size: 14px;
    }
    .red{
        color: #ff0000;
    }
</style>

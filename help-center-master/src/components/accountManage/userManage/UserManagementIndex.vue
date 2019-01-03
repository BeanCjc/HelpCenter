<template>
    <div class="table">
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-lx-calendar"></i> 账户管理</el-breadcrumb-item>
                <el-breadcrumb-item>用户管理</el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="container">

            <el-row :gutter="20">
                <el-col :span="8">
                    <div class="handle-box">
                        <el-button type="primary" icon="add" class="handle-add" @click="addUM()">新增</el-button>
                    </div>

                </el-col>
                <el-col :span="16">
                    <div class="handle-search-box">
                        <span>昵称:</span>
                        <el-input v-model="dptinput" class="search-w search"  size="small" clearable placeholder="请输入关键字进行搜索"  @clear="uMloadData">
                            <!--<el-input class="search"  size="small" v-model="keyword" clearable placeholder="请输入关键字进行搜索">-->
                        </el-input>
                        <el-button type="primary" @click="dsmSearch">搜索</el-button>
                        <el-button type="primary" @click="uMloadData">重置</el-button>
                    </div>
                </el-col>
            </el-row>




          <xy-user-manage-table
              ref="umTable"
              :dptinput="dptinput"
              @modifyVisibleChange = "modifyVisibleFun($event)"
              @resetVisibleChange = "resetVisibleFun($event)"
              @umtEvent="getUmData"
          >
          </xy-user-manage-table>
        </div>


        <!--弹出框-->
        <el-dialog :title="tip" :visible.sync="addVisible" width="30%">
            <xy-user-manage-form
                ref="umFrom"
                :formProps="formProps"
                :userManageForm="userManageForm"
                @modifyUmfSuccess="modifyUmfSuccess"
                @umClose="umClose"
            ></xy-user-manage-form>
        </el-dialog>




        <!--修改密码-->
        <el-dialog title="修改密码" :visible.sync="resetVisible" width="30%">
            <xy-user-manage-pass
                ref="umPass"
                :userManageForm="userManageForm"
                ></xy-user-manage-pass>

            <span slot="footer" class="dialog-footer">
                <el-button @click="resetVisible = false">返回</el-button>
                <el-button type="primary" @click="umPassSubmit()">提交</el-button>
            </span>
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

    import xyUserManageForm from '@/components/accountManage/userManage/UserManageForm.vue';
    import xyUserManageTable from '@/components/accountManage/userManage/UserManageTable.vue';
    import xyUserManagePass from '@/components/accountManage/userManage/UserManagePass.vue';

    export default {
        name: 'userMange',
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
                dptinput:'',
                tableData: [],
                cur_page: 1,
                multipleSelection: [],
                select_cate: '',
                select_word: '',
                del_list: [],
                is_search: false,
                addVisible:false,
                modifyVisible:false,
                resetVisible:false,
                delVisible: false,
                userManageForm:{
                    usrName: '',
                    usrAccount: '',
                    usrPsw: '',
                    usrType: 0,
                    usrVerifyState:0,
                    usrPhoneNum: '',
                    UsrDeptId: 'a',
                    roleIdList:'',
                    usrState: 0,
                    usrId:''
                },
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

        created() {
//            this.getData();
        },
        components: {
            xyUserManageForm,
            xyUserManageTable,
            xyUserManagePass
        },
        computed: {
            data() {

            }
        },
        methods: {
            /**
             * @Type:数据
             * @Description:接受table子组件传过来的数据
             */
            getUmData(data){
                this.userManageForm = data;
                if (this.$refs.umModify){
                    this.$refs.umModify.loadData(data.usrId);
                }
            },
            /**
             * @Type:数据
             * @Description:重置表格数据
             */
            uMloadData(){
                this.$refs.umTable.uMloadData(1);
            },
            /**
             * @Type:数据
             * @Description:子组件提交修改成功重置表格数据
             */
            modifyUmfSuccess(){
                this.$refs.umTable.uMloadData();
                this.addVisible = false;
            },

            /**
             * @Type:数据
             * @Description:搜索，展示表格数据
             */
            dsmSearch(){
                this.$refs.umTable.searchDptData();
            },

            /**
             * @Type:打开弹层
             * @Description:新增
             */
            addUM() {
                this.tip = '用户新增';
                this.formProps.methods = 'post';
                this.addVisible = true;
                if (this.$refs.umFrom){
                    this.$refs.umFrom.resetForm();
                }
            },


            /**
             * @Type:打开弹层
             * @Description:修改
             */
            modifyVisibleFun(row){//接受从table来的值
                this.tip = '用户修改';
                this.formProps = row;
                this.formProps.methods = 'put';
                if (this.$refs.umFrom) {
                    this.$refs.umFrom.loadData(row.usrId);
                }
                this.addVisible = true;
            },

            /**
             * @Type:关闭弹层
             * @Description:关闭弹层,传给Form使用
             */
            umClose() {
                this.addVisible = false;
            },
            /**
             * @Type:打开弹层
             * @Description:重置密码
             */
            resetVisibleFun(e){
                this.resetVisible = e;
            },


            /**
             * @Type:提交
             * @Description:新增
             */
            onUmfSubmit(){
                this.$refs.umAdd.onUmfSubmit();
                this.$refs.umTable.uMloadData();//子传给父，父将要传给子
                this.addVisible = false;
            },


            /**
             * @Type:提交
             * @Description:修改
             */
            modifyUmfSubmit(){
                this.$refs.umModify.modifyUmfSubmit(this.userManageForm);
            },

            /**
             * @Type:提交
             * @Description:重置密码
             */
            umPassSubmit() {
                this.$refs.umPass.umPassSubmit();
                this.resetVisible = false;
            },


            /**
             * @Type:提交
             * @Description:删除
             */
            deleteRow(){
                this.tableData.splice(this.idx, 1);
                this.$message.success('删除成功');
                this.delVisible = false;
            },
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
    .search-w{
        width: 20%;
    }
</style>

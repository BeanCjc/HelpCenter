<template>
    <div>
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-setting"></i>部门管理</el-breadcrumb-item>
                <el-breadcrumb-item>部门人员管理</el-breadcrumb-item>
            </el-breadcrumb>
        </div>

        <div class="container">
            <el-row :gutter="20">
                <el-col :span="4">
                    <el-container style="max-height: 636px">
                        <el-main>
                            <el-scrollbar class="custom-scrollbar" style="min-height: 587px;">
                                <xy-depart-people-tree ref="unitTree"
                                              :checkStrictly="true"
                                              @nodeClick="nodeClickHandle"
                                             ></xy-depart-people-tree>
                            </el-scrollbar>
                        </el-main>
                    </el-container>
                </el-col>
                <el-col :span="20">
                    <div class="handle-box">
                        <el-button class="handle-box-add" icon="el-icon-plus" @click="addHandle" type="primary">
                            添加
                        </el-button>
                        <div class="handle-search-box">
                            <span>人员名称:</span>
                            <el-input v-model="dptinput" class="search"  size="small" clearable placeholder="请输入关键字进行搜索"  @clear="dsfLoadTableData">
                            <!--<el-input class="search"  size="small" v-model="keyword" clearable placeholder="请输入关键字进行搜索">-->
                            </el-input>
                            <el-button type="primary" @click="dsmSearch">搜索</el-button>
                            <el-button type="primary" @click="dsfLoadTableData">重置</el-button>
                        </div>
                    </div>
                    <!--table-->
                    <xy-depart-people-table
                        ref="dpftable"
                        :resetVisible="resetVisible"
                        :dptinput="dptinput"
                        @dpfEvent="getDpfData"
                        @modifyVisibleChange = "modifyVisibleFun($event)"
                        @resetVisibleChange = "resetVisibleFun($event)"
                    ></xy-depart-people-table>
                </el-col>
            </el-row>
        </div>


        <!--弹出框-->
        <el-dialog :title="tip" :visible.sync="addVisible" width="30%">
            <xy-depart-people-form
                ref="dpfFrom"
                :formProps="formProps"
                :departPeopleForm="departPeopleForm"
                @dpfSubmitSuccess="dpfSubmitSuccess"
                @dpfClose="dpfClose"
            ></xy-depart-people-form>
        </el-dialog>

        <!-- 重置密码-->
        <el-dialog title="重置密码" :visible.sync="resetVisible" width="30%">
            <xy-depart-people-pass
                ref="dpfPass"
                :departPeopleForm="departPeopleForm"
            ></xy-depart-people-pass>
            <span slot="footer" class="dialog-footer">
                <el-button @click="resetVisible = false">取 消</el-button>
                <el-button type="primary" @click="onPassSubmit">确 定</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script>

    import xyDepartPeopleTree from '@/components/departManage/departPeople/DepartPeopleTree.vue';
    import xyDepartPeopleTable from '@/components/departManage/departPeople/DepartPeopleTable.vue';
    import xyDepartPeopleForm from '@/components/departManage/departPeople/DepartPeopleForm.vue';
    import xyDepartPeoplePass from '@/components/departManage/departPeople/DepartPeoplePass.vue';

    export default {
        name: 'departStaff',
        mounted() {
        },
        data() {
            return {
                modifyVisible: false,
                addVisible:false,
                resetVisible:false,
                delVisible: false,
                dptinput:'',
                departPeopleForm:{
                    preDeptId: '',
                    deptName: '',
                    usrName: '',
                    usrAccount: '',
                    usrPsw: '',
                    usrType: 0,
                    usrVerifyState:0,
                    usrPhoneNum:1,
                    usrDeptId: 1,
                    roleIdList: [1],
                    usrState: 0,
             },
            formProps: {
                method:'put'
            },
            tip: '',
            }
        },
        components: {
            xyDepartPeopleTree,
            xyDepartPeopleTable,
            xyDepartPeopleForm,
            xyDepartPeoplePass
        },
        computed: {
            data() {

            }
        },
        methods: {
          /**
           * @Type:数据
           * @Description:重置表格数据
           */
            dsfLoadTableData(){
                this.$refs.dpftable.dpfLoadData(1);//重置页码为1
            },
            dpfSubmitSuccess(){
                this.$refs.dpftable.dpfLoadData();
                this.addVisible = false;
            },

           /**
            * @Type:数据
            * @Description:搜索，展示表格数据
            */
            dsmSearch(){
                this.$refs.dpftable.searchDptData();
            },


            /**
             * @Type:数据
             * @Description:接受table子组件传过来的数据
             */
            getDpfData(data){
                this.departPeopleForm = data;
                if (this.$refs.dpfFrom){
                    this.$refs.dpfFrom.loadRowData(data.usrid);
                }
            },


            /**
             * @Type:数据
             * @Description:子组件提交修改成功重置表格数据
             */
//            dpfSubmitSuccess(){
//                this.departPeopleForm = '';
//                this.$refs.dpftable.dpfLoadData();
//            },


            /**
             * @Type:打开弹层
             * @Description:新增
             */
            addHandle() {
                this.tip = '新增部门人员';
                this.formProps.methods = 'post';
                this.addVisible = true;
                if (this.$refs.dpfFrom){
                    this.$refs.dpfFrom.resetForm();
                }
            },


            /**
             * @Type:打开弹层
             * @Description:修改
             */
            modifyVisibleFun(row){
                this.tip = '部门人员修改';
                this.formProps = row;
                this.formProps.methods = 'put';
                if (this.$refs.dpfFrom) {
                    this.$refs.dpfFrom.loadRowData(row.usrid);
                }
                this.addVisible = true;
            },
            /**
             * @Type:关闭弹层
             * @Description:关闭弹层,传给Form使用
             */
            dpfClose() {
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
            onDpfSubmit(){
                this.$refs.dpfAdd.onDpfSubmit();
                this.$refs.dpftable.dpfLoadData();
                this.addVisible = false;
            },


            /**
             * @Type:提交
             * @Description:修改
             */
            modifyDpfSubmit(){
                this.$refs.dpmModify.modifyDpfSubmit();
                this.modifyVisible = false;
            },


            /**
             * @Type:提交
             * @Description:重置密码
             */
            onPassSubmit(){
                this.$refs.dpfPass.onPassSubmit();
                this.resetVisible = false;
            },


            /**
             * @Type:树插件
             * @Description:点击事件
             */
            nodeClickHandle: function (data, node, self) {
                /*这里是树点击事件后查询table表格*/
                this.$refs.dpftable.searchTreeDptData(data.deptId);

            },
        }
    }

</script>

<style scoped>
    .el-input {
        width: auto;
        margin-right: 10px;
    }
    .handle-box{
        display: flex;
        margin-bottom: 15px;
    }

    .handle-box-add{
        margin-right: 100px;
    }
    .handle-search-box{
        display: flex;
    }
    .handle-search-box span{
        line-height: 35px;
        margin-right: 15px;
    }
</style>






































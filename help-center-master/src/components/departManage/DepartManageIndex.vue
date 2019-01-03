<template>
    <div>
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-setting"></i>部门管理</el-breadcrumb-item>
                <el-breadcrumb-item>部门管理</el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="container">
            <el-row :gutter="20">
                <el-col :span="4">
                    <el-container style="max-height: 636px">
                        <el-main>
                            <el-scrollbar class="custom-scrollbar" style="min-height: 587px;">
                                <xy-depart-tree ref="unitTree"
                                                :checkStrictly="true"
                                                @nodeClick="nodeClickHandle"
                                ></xy-depart-tree>
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
                            <span>部门名称:</span>
                            <el-input v-model="dminput" class="search"  size="small" clearable placeholder="请输入关键字进行搜索" @clear="dmLoadTableData">
                            </el-input>
                            <el-button type="primary" @click="dmsearch" >搜索</el-button>
                            <el-button type="primary" @click="dmLoadTableData" >重置</el-button>
                        </div>
                    </div>
                    <!--table-->
                    <xy-depart-table
                        ref="dmTable"
                        :resetVisible="resetVisible"
                        :dminput = "dminput"
                        @onDelete="delDepart"
                        @dmEvent="getDmData"
                        @modifyVisibleChange = "modifyVisibleFun($event)"
                        @resetVisibleChange = "resetVisibleFun($event)"
                    ></xy-depart-table>
                </el-col>
            </el-row>
        </div>

        <!--弹出框-->
        <el-dialog :title="tip" :visible.sync="addVisible" width="30%">
            <xy-depart-form
                ref="dmFrom"
                :formProps="formProps"
                :departForm="departForm"
                @departFormSubmitSuccess="departFormSubmitSuccess"
                @dfClose="dfClose"
            ></xy-depart-form>
            <!--<span slot="footer" class="dialog-footer">-->
                <!--<el-button @click="addVisible = false">取 消</el-button>-->
                <!--<el-button type="primary" @click="onSubmit">确 定</el-button>-->
            <!--</span>-->
        </el-dialog>


        <!--重置弹出框 -->
        <el-dialog title="重置密码" :visible.sync="resetVisible" width="30%">
            <xy-depart-pass
                ref="dmPass"
                :departForm="departForm"></xy-depart-pass>
            <span slot="footer" class="dialog-footer">
                <el-button @click="resetVisible = false">取 消</el-button>
                <el-button type="primary" @click="onPassSubmit">确 定</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script>

    import xyDepartTree from '@/components/departManage/DepartTree.vue';
    import xyDepartTable from '@/components/departManage/DepartTable.vue';
    import xyDepartForm from '@/components/departManage/DepartForm.vue';
    import xyDepartPass from '@/components/departManage/DepartPass.vue';


    export default {
        name: 'departManage',
        mounted() {
        },
        data() {
            return {
                modifyVisible: false,
                addVisible:false,
                resetVisible:false,
                delVisible: false,
                dminput: '',
                dfForm:'',
                departForm:{
                    preDeptId: '',
                    deptName: '',
                    deptAccount: '',
                    deptPsw:''
                },
                formProps: {
                    method:'put'
                },
                tip: '',
            }
        },
        components: {
            xyDepartTree,
            xyDepartTable,
            xyDepartForm,
            xyDepartPass

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
            dmLoadTableData(){
                this.$refs.dmTable.dmLoadTableData(1);
            },

            /**
             * @Type:数据
             * @Description:搜索，展示表格数据
             */
            dmsearch(){
                this.$refs.dmTable.searchData();
            },

            /**
             * @Type:数据-接值
             * @Description:接受table子组件传过来的数据
             */
            getDmData(data){
                this.departForm = data;
//                if (this.$refs.dmModify){
//                    this.$refs.dmModify.loadData(data.deptId);
//                }
                if (this.$refs.dmFrom){
                    this.$refs.dmFrom.loadData(data.deptId);
                }
            },


            /**
             * @Type:数据-接值
             * @Description:子组件提交修改成功重置表格数据
             */
            departFormSubmitSuccess(){
                this.$refs.dmTable.dmLoadTableData();
                this.$refs.unitTree.DTreeloadData();
                this.addVisible = false;
            },

            /**
             * @Type:打开弹层
             * @Description:新增
             */
            addHandle() {
                this.tip = '部门新增';
                this.formProps.methods = 'post';
                this.addVisible = true;
                if (this.$refs.dmFrom){
                    this.$refs.dmFrom.resetForm();
                }
            },
            /**
             * @Type:打开弹层
             * @Description:修改
             */
            modifyVisibleFun(row){
                this.tip = '部门修改';
                this.formProps = row;
                this.formProps.methods = 'put';
                if (this.$refs.dmFrom) {
                    this.$refs.dmFrom.loadData(row.deptId);
                }
                this.addVisible = true;
            },
            /**
             * @Type:提交
             * @Description:删除
             */
            delDepart(row){
                let _this = this;
                let params = {
                    deptId:row.deptId
                }
                console.log(params);
                this.$confirm('你真的删除这个部门吗?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    _this.$axios.delete('api/Dept',{
                        params: params,
                    }).then(res => {
                        let data = res.data
                         console.log(res);
                        if (res.result === false) {
                            _this.$message.error(data.tips);
                        } else {
                            _this.$message(data.tips);
                            this.$refs.dmTable.dmLoadTableData();
                            this.$refs.unitTree.DTreeloadData();
                        }


                    });
                })
            },

            /**
             * @Type:关闭弹层
             * @Description:关闭弹层,传给Form使用
             */
            dfClose() {
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
             * @Description:重置密码
             */
            onPassSubmit(){
                this.$refs.dmPass.onPassSubmit();
                if (this.$refs.dmPass){
                    this.$refs.dmPass.resetForm();
                }
                this.resetVisible = false;
            },
            /**
             * @Type:树插件
             * @Description:点击事件
             */
            nodeClickHandle: function (data, node, self) {
                /*这里是树点击事件后查询table表格*/
                this.$refs.dmTable.searchData(data.deptName);

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






































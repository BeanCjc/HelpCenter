<template>
    <div>
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-setting"></i>文档管理</el-breadcrumb-item>
                <el-breadcrumb-item>分类设置</el-breadcrumb-item>
            </el-breadcrumb>
        </div>

        <div class="container">
            <el-row :gutter="20">
                <el-col :span="4">
                    <el-container style="max-height: 636px">
                        <el-main>
                            <el-scrollbar class="custom-scrollbar" style="min-height: 587px;">
                                <xy-doc-tree ref="unitTree"
                                             :checkStrictly="true"
                                             @nodeClick="nodeClickHandle"
                                ></xy-doc-tree>
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
                            <span>分类名称:</span>
                            <el-input v-model="docSetInput" class="search" size="small" clearable placeholder="请输入关键字进行搜索" @clear="DocSetloadData">
                                <!--<el-input class="search"  size="small" v-model="keyword" clearable placeholder="请输入关键字进行搜索">-->
                            </el-input>
                            <el-button type="primary" @click="docSetSearch">搜索</el-button>
                            <el-button type="primary" @click="DocSetloadData">重置</el-button>
                        </div>
                    </div>
                    <!--table-->
                    <xy-doc-table
                        ref="unitTable"
                        :docSetInput="docSetInput"
                        :modifyVisible="modifyVisible"
                        @onDelete="delDocSetting"
                        @modifyVisibleChange="modifyVisibleFun"
                    ></xy-doc-table>
                </el-col>
            </el-row>
        </div>
        <!-- 弹出框 -->
        <el-dialog :title="tip" :visible.sync="modifyVisible" width="30%">
            <xy-doc-form
                ref="docForm"
                :formProps="formProps"
                @docFormSubmitSuccess="docFormSubmitSuccess"
                         @docClose="docClose"></xy-doc-form>
        </el-dialog>

    </div>
</template>

<script>

    import xyDocTree from '@/components/docManage/helpDocSetting/DocTree.vue';
    import xyDocTable from '@/components/docManage/helpDocSetting/DocTable.vue';
    import xyDocForm from '@/components/docManage/helpDocSetting/DocForm.vue';


    export default {
        name: 'sortSetting',
        data() {
            return {
                modifyVisible: false,
                delVisible: false,
                docSetInput:'',
                formProps: {
                    method:'put'
                },
                tip: '',
            }
        },
        components: {
            xyDocTree,
            xyDocTable,
            xyDocForm
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
            DocSetloadData(){
                this.$refs.unitTable.dsAllLoadData('',0,1);
                this.docSetInput='';
//                this.$refs.unitTable.DocSetloadData();
            },
            /**
             * @Type:数据
             * @Description:搜索，展示表格数据
             */
            docSetSearch(){
                this.$refs.unitTable.dsAllLoadData(this.docSetInput,1,1);
//                this.$refs.unitTable.searchData();
            },
            /**
             * @Type:树插件
             * @Description:点击事件
             */
            nodeClickHandle:function (data, node, self) {
                /*这里是树点击事件后查询table表格*/
                this.$refs.unitTable.dsAllLoadData(data.docTypeId,2,1);
//                this.$refs.unitTable.searchData(data.docTypeName);

            },
            /**
             * @Type:打开弹层
             * @Description:新增
             */
            addHandle() {
                this.tip = '新增分类';
                this.formProps.methods = 'post';
                this.modifyVisible = true;
                if (this.$refs.docForm){
                    this.$refs.docForm.resetForm();
                }
            },

            /**
             * @Type:打开弹层
             * @Description:修改
             */
            modifyVisibleFun(row) {
                this.tip = '修改分类';
                this.formProps = row;
                this.formProps.methods = 'put';
                if (this.$refs.docForm) {
                    this.$refs.docForm.loadData(row.docTypeId);
                }
                this.modifyVisible = true;
            },

            docClose() {
                this.modifyVisible = false;
            },

            /**
             * @Type:数据
             * @Description:子组件提交修改成功重置表格数据，树结构
             */
            docFormSubmitSuccess() {
//                this.$refs.unitTree.loadData();
//                this.$refs.unitTable.DocSetloadData();
                this.$refs.unitTable.dsAllLoadData('',0,1);
                this.$refs.unitTree.loadData();

                this.modifyVisible = false;
            },

            /**
             * @Type:提交
             * @Description:删除
             */
            delDocSetting(row){
                let _this = this;
                this.$confirm('确定要删除此分类吗?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    _this.$axios.delete('/api/DocTypeConfig/',{
                        params:{
                            docTypeId :row.docTypeId
                        }
                    }).then(res => {
                        if (res.data.result === false) {
                            this.$message.error(res.data.tips);
                        }else {
                            this.$message({
                                message:res.data.tips,
                                type: 'success'
                            });
//                            this.$refs.unitTable.DocSetloadData();

                            this.$refs.unitTable.dsAllLoadData('',0,1);
                            this.$refs.unitTree.loadData();
                        }
                    });
                }).catch(() => {
                });
            },

        }
    }

</script>

<style scoped>
    .el-input {
        width: auto;
        margin-right: 10px;
    }

    .handle-box {
        display: flex;
        margin-bottom: 15px;
    }

    .handle-box-add {
        margin-right: 100px;
    }

    .handle-search-box {
        display: flex;
    }

    .handle-search-box span {
        line-height: 35px;
        margin-right: 15px;
    }
</style>






































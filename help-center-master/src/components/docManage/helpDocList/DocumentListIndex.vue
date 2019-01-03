<template>
    <div>
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-setting"></i>文档管理</el-breadcrumb-item>
                <el-breadcrumb-item>文档列表</el-breadcrumb-item>
            </el-breadcrumb>
        </div>

        <div class="container">
            <el-row :gutter="20">
                <el-col :span="5">
                    <el-container style="max-height: 636px">
                        <el-main>
                            <el-scrollbar class="custom-scrollbar" style="min-height: 587px;">
                                <xy-doc-list-tree ref="unitTree"
                                              :checkStrictly="true"
                                                  @nodeClick="nodeClickHandle"
                                             ></xy-doc-list-tree>
                            </el-scrollbar>
                        </el-main>
                    </el-container>
                </el-col>
                <el-col :span="19">
                    <div class="handle-box">
                        <el-button class="handle-box-add" icon="el-icon-plus" @click="addHandle" type="primary">
                            添加
                        </el-button>
                        <div class="handle-search-box">
                            <span>文档名称:</span>
                            <el-input class="search" v-model="docListKey" size="small" clearable placeholder="请输入关键字进行搜索" @clear="DocListloadData">
                            <!--<el-input class="search"  size="small" v-model="keyword" clearable placeholder="请输入关键字进行搜索">-->
                            </el-input>
                            <el-button type="primary" @click="docListSearch">搜索</el-button>
                            <el-button type="primary" @click="DocListloadData">重置</el-button>
                        </div>
                    </div>
                    <!--table-->
                    <xy-doc-list-table
                        ref="unitTable"
                        :docListKey="docListKey"
                        :modifyVisible="modifyVisible"
                        @delDocList="delDocList"
                        @modifyVisibleChange = "modifyVisibleFun($event)"
                    ></xy-doc-list-table>
                </el-col>
            </el-row>
        </div>

    </div>
</template>

<script>

    import xyDocListTree from './DocListTree.vue';
    import xyDocListTable from './DocListTable.vue';

    export default {
        name: 'sortSetting',
        data() {
            return {
                modifyVisible: false,
                delVisible: false,
                docListKey:''
            }
        },
        mounted(){
          this.$bus.$on('DocListOperateAdd',()=>{
              this.$refs.unitTable.loadData()
          })
        },
        components: {
            xyDocListTree,
            xyDocListTable
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
            DocListloadData(){
//                this.$refs.unitTable.loadData();
                this.$refs.unitTable.dlAllLoadData('',0,1);
                this.docListKey='';
            },
            /**
             * @Type:数据
             * @Description:搜索，展示表格数据
             */
            docListSearch(){
//                this.$refs.unitTable.searchData();
                this.$refs.unitTable.dlAllLoadData(this.docListKey,1,1);
            },
            /**
             * @Type:树插件
             * @Description:点击事件
             */
            nodeClickHandle:function (data, node, self) {
                /*这里是树点击事件后查询table表格*/
                this.$refs.unitTable.dlAllLoadData(data.docTypeId,2,1);
//                this.$refs.unitTable.searchDataTree(data.docTypeId);

            },
            /**
             * @Type:
             * @Position:
             * @Description:添加文章
             */
            addHandle() {
                this.$router.push({
                    path: '/docListOperateAdd'
                })
            },
            /**
             * @Type:
             * @Position:
             * @Description:
             */
            modifyVisibleFun(e){
              console.log("父元素");
              console.log(e);
              this.modifyVisible = e;
            },
            /**
             * @Type:
             * @Position:
             * @Description:删除列表信息
             */
            delDocList(row){
                let _this = this;
                this.$confirm('确定要删除此文档吗?', '提示', {
//                this.$confirm('您真的删除【' + row.docTitle + '】吗?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    _this.$axios.delete('/api/HelpDoc',{
                        params:{
                            helpDocId :row.docId
                        }
                    }).then(res => {
                        if (res.data.result === false) {
                            this.$message.error(res.data.tips);
                        }else {
                            this.$refs.unitTable.dlAllLoadData('',0,1);
                            this.$message({
                                message:res.data.tips,
                                type: 'success'
                            });
//                            this.$refs.unitTable.loadData();

                        }
                    });
                }).catch(() => {
                });
            }
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






































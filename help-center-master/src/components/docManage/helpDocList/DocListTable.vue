<template>
    <div class="doc-list-table">
        <el-table :data="tableData" border class="table" ref="multipleTable" :empty-text="emptyText" :row-style="rowClass">
            <el-table-column prop="docTypeName" label="文档分类" align="center">
            </el-table-column>
            <el-table-column prop="docDeptName" label="文档所属部门" align="center">
            </el-table-column>
            <el-table-column prop="docTitle" label="文档标题" align="center">
            </el-table-column>

            <!--<el-table-column prop="docFullText" label="文档纯文字内容"align="center" label-class-name="hide-column" class-name="hide-column">-->
            <el-table-column prop="docFullText" label="文档纯文字内容"align="center"  :show-overflow-tooltip="true">
            </el-table-column>

            <el-table-column prop="docNum" label="文档排序号码" align="center">
            </el-table-column>
            <el-table-column prop="docCount" label="点击数" align="center">
            </el-table-column>
            <el-table-column label="操作" width="280" align="center">
                <template slot-scope="scope">
                    <el-button  type="text"  @click="DltView(scope.row)">查看</el-button>
                    <el-button v-show="scope.row.editRole" type="text"  @click="DltModify(scope.row)">修改</el-button>
                    <el-button v-show="scope.row.editRole" type="text"  class="red" @click="handleDelete(scope.row)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>

        <div class="pagination">
            <el-pagination
                @current-change="handleCurrentChange"
                :current-page.sync="currentPage"
                :page-size="pageSize"
                layout="total, prev, pager, next"
                :total="pageTotal">
            </el-pagination>
        </div>
    </div>
</template>

<script>

    export default {
        mounted() {
//            this.loadData();
            this.dlAllLoadData();
        },
        props: {
            modifyVisible:'',
            docListKey:'',
            strDocTypeId:''
        },
        components: {

        },
        data() {
            return {
                tableData: [],
                emptyText:'暂无数据',
                currentPage: 1,
                pageTotal: 0,
                pageSize: 10,
                judgePage:0,
                parameterKey:'',
                parameterJudge:0

            }
        },
        computed: {
            rowClass(){
                return {"height":"20px"}
            }
        },
        methods: {

          /**
           * @Type:
           * @Position:表格初始化，搜索，树查询
           * @Description:
           */

          dlAllLoadData: function (key,judge,currentPage) {
                let _this = this;
                let dLTabledata;

                judge==undefined?_this.judgePage=0:_this.judgePage=judge;
                currentPage==undefined?_this.currentPage=_this.currentPage:_this.currentPage=currentPage;

//                debugger
                if(_this.judgePage==0){//初始化
                    dLTabledata = {
                        url:'/api/HelpDoc/pagelist',
                        params:{
                            pageIndex:_this.currentPage,
                            rowCount:_this.pageSize,
                        }
                    };
                    _this.parameterKey='';
                    _this.parameterJudge=0;

                }else if(_this.judgePage==1){//搜索
                    dLTabledata={
                      url:'api/HelpDoc/pagelist',
                      params:{
                          rowCount: _this.pageSize,
                          pageIndex:_this.currentPage,//只有为1才能搜索全部
//                          pageIndex:1,//只有为1才能搜索全部
                          keyword:key
                      }
                    }
                    _this.parameterKey=key;
                    _this.parameterJudge=1;
                }else if(_this.judgePage==2){//树查询
                    dLTabledata={
                      url:'api/HelpDoc/doctypeid',
                      params:{
                          rowCount: _this.pageSize,
                          pageIndex: _this.currentPage,
                          docTypeId:key
                      }
                  }
                    _this.parameterKey=key;
                    _this.parameterJudge=2;
                }
                _this.$axios.get(dLTabledata.url,
                    {params:dLTabledata.params}
                ).then((res) => {
                    //用axios的方法引入地址
                    if(!res.data.result){
                        _this.$message.error(res.data.tips);
                    }else {
                        _this.tableData = res.data.data.info;
                        _this.pageTotal = res.data.data.totalCount;
                    }
                })

//              console.log(_this.currentPage);
    },


            /**
             * @Type:
             * @Position:表格数据
             * @Description:
             */
            loadData: function (filter) {
//                let _this = this;
//
//                _this.$axios.get('/api/HelpDoc/pagelist',{
//                    params:{
//                        pageIndex:_this.currentPage,
//                        rowCount:_this.pageSize,
//                    }
//                }).then((res) => {
//                    //用axios的方法引入地址
//                    if(!res.data.result){
//                        _this.$message.error(res.data.tips);
//                    }else {
//                        _this.tableData = res.data.data.info;
//                        _this.pageTotal = res.data.data.totalCount;
//                        _this.judgePage =0;
//                    }
//                    //赋值
////                    console.log(res.data.list)
//                })
            },


            /**
             * @Type:数据
             * @Description:搜索表格数据
             */
            searchData() {
//                let _this = this;
//                _this.judgePage =1;
//                this.$axios.get('api/HelpDoc/pagelist',{
//                    params:{
//                        rowCount: _this.pageSize,
//                        pageIndex:1,//只有为1才能搜索全部
////                        pageIndex: _this.currentPage,
//                        keyword:_this.docListKey
//                    }
//                })
//
//                    .then(res => {
//                        let data = res.data.data;
//                        _this.tableData = data.info;
//                        _this.pageTotal = data.totalCount;
//                        _this.judgePage =1;
//                    })
//                    .catch(function (error) {
//                        console.log(error);
//                    });
            },

            /**
             * @Type:数据
             * @Description:根据树搜索表格数据
             */
            searchDataTree(strDocTypeName) {
//                let _this = this;
//                this.$axios.get('api/HelpDoc/doctypeid',{
//                    params:{
//                        rowCount: _this.pageSize,
//                        pageIndex: _this.currentPage,
////                        strDocTypeName:_this.docSetInput
//                        docTypeId:strDocTypeName
//                    }
//                })
//                .then(res => {
//                    let data = res.data.data;
//                    _this.tableData = data.info;
//                    _this.pageTotal = data.totalCount;
//
//                })
//                .catch(function (error) {
//                    console.log(error);
//                });
            },



            /**
             * 分页事件
             */
            handleCurrentChange: function (val) {
                let _this = this;
                _this.currentPage = val;
                this.dlAllLoadData(_this.parameterKey, _this.parameterJudge);
//                if(this.judgePage==0){
//                    this.loadData();
//                }else if(this.judgePage==1){
//                    this.searchData();
//                }
            },
            /**
             * @Type:
             * @Position:
             * @Description:查看
             */
            DltView(row){
//                this.$router.push({path:'/docListOperateView',query:{id:row.docId}});
                this.$router.push({
                    name: 'DocListOperateView',
                    params: {
                        id: row.docId
                    }
                })
            },
            /**
             * @Type:
             * @Position:
             * @Description:修改
             */
            DltModify(row){
//                this.$router.push({path:'/docListOperateModify',query:{id:row.docId}});
                this.$router.push({
                    name: 'DocListOperateModify',
                    params: {
                        id: row.docId
                    }
                })

            },
            handleDelete(row){
                this.$emit('delDocList',row)
            },

        },
        watch: {

        },
        beforeDestroy() {

        }
    }
</script>

<style scoped lang="less">


</style>

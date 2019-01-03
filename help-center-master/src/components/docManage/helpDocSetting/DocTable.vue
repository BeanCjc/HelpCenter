<template>
    <div>
        <el-table :data="tableData" border class="table" ref="multipleTable">
            <el-table-column prop="crdt" label="创建时间" align="center">
            </el-table-column>
            <el-table-column prop="docTypeNum" label="排序号" align="center">
            </el-table-column>
            <el-table-column prop="docPreTypeName" label="上级分类名称"align="center">
            </el-table-column>
            <el-table-column prop="docTypeName" label="分类名称"align="center">
            </el-table-column>
            <el-table-column prop="docTypeDeptName" label="归属部门" align="center">
            </el-table-column>

            <el-table-column label="操作" width="280" align="center">
                <template slot-scope="scope">
                    <el-button type="text"  @click="handleModify(scope.row)">修改</el-button>
                    <el-button type="text"  class="red"  @click="deleteHandle(scope.row)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>

        <div class="pagination">
            <el-pagination
                @current-change="handleCurrentChange"
                :page-size="pageSize"
                layout="total, prev, pager, next"
                :total="pageTotal">
            </el-pagination>
        </div>

    </div>
</template>

<script>

    export default {
        props: {
            modifyVisible:'',
            docSetInput:''
        },
        components: {

        },
        data() {
            return {
                tableData: [],
                currentPage: 1,
                pageTotal: 0,
                pageSize: 10,
                judgePage:0,
                parameterKey:'',
                parameterJudge:0,
                idx: -1,
            }
        },
        computed: {

        },
        mounted(){
//            this.DocSetloadData();
            this.dsAllLoadData();

        },
        methods: {

            /**
             * @Type:
             * @Position:表格初始化，搜索，树查询
             * @Description:
             */

            dsAllLoadData: function (key,judge,currentPage) {
                let _this = this;
                let dLTabledata;

                judge==undefined?_this.judgePage=0:_this.judgePage=judge;
                currentPage==undefined?_this.currentPage=_this.currentPage:_this.currentPage=currentPage;
//debugger
                if(_this.judgePage==0){//初始化
                    dLTabledata = {
                        url:'/api/DocTypeConfig/pagelist',
                        params:{
                            pageIndex:_this.currentPage,
                            rowCount:_this.pageSize,
                        }
                    };
                    _this.parameterKey='';
                    _this.parameterJudge=0;

                }else if(_this.judgePage==1){//搜索
                    dLTabledata={
                        url:'api/DocTypeConfig/keyword/doctypename',
                        params:{
                            rowCount: _this.pageSize,
                            pageIndex:_this.currentPage,//只有为1才能搜索全部
//                          pageIndex:1,//只有为1才能搜索全部
                            strDocTypeName:key
                        }
                    }
                    _this.parameterKey=key;
                    _this.parameterJudge=1;
                }else if(_this.judgePage==2){//树查询
                    dLTabledata={
                        url:'api/DocTypeConfig/doctypelist/doctypeid',
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
//                    debugger
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
             * @Type:数据
             * @Description:初始化表格
             */
            DocSetloadData: function (filter) {
//                this.$axios.get('/api/DocTypeConfig/pagelist',{
//                    params:{
//                        pageIndex:this.currentPage,
//                        rowCount:this.pageSize
//                    }
//                })
//                .then((res) => {
//                this.tableData = res.data.data.info;
//                  this.pageTotal =  res.data.data.totalCount;
//                    this.judgePage =0;
//                })
//                .catch(function (error) {
//                    console.log(error);
//                });
            },
            /**
             * @Type:数据
             * @Description:搜索表格数据
             */
            searchData(strDocTypeName) {
//                let _this = this;
//                _this.judgePage =1;
//                strDocTypeName==undefined?strDocTypeName=this.docSetInput:strDocTypeName;
//                this.$axios.get('api/DocTypeConfig/keyword/doctypename',{
//                    params:{
//                        rowCount: _this.pageSize,
////                        pageIndex:1,
//                        pageIndex: _this.currentPage,
////                        strDocTypeName:_this.docSetInput
//                        strDocTypeName:strDocTypeName
//                    }
//                })
//
//                .then(res => {
//                         let data = res.data.data;
//                         debugger
//                        _this.tableData = data.info;
//                        _this.pageTotal = data.totalCount;
//                    _this.judgePage =1;
//
//                })
//                .catch(function (error) {
//                    console.log(error);
//                });
            },
            /**
             * @Type:事件
             * @Description:分页事件
             */
            handleCurrentChange: function (val) {
//              debugger
//                this.currentPage = val;
//
//                if(this.judgePage==0){
//                    this.DocSetloadData();
//                }else if(this.judgePage==1){
//                    this.searchData();
//                }

                let _this = this;
                _this.currentPage = val;
                this.dsAllLoadData(_this.parameterKey, _this.parameterJudge);
            },


            /**
             * @Type:
             * @Position:表格
             * @Description:修改选中行
             */
            handleModify(row){
              this.$emit("modifyVisibleChange",row);
            },
            /**
             * @Type:
             * @Position:表格
             * @Description:删除
             */
            deleteHandle(row) {
                this.$emit('onDelete', row);
            }

        },
        watch: {

        },
        beforeDestroy() {

        }
    }
</script>

<style scoped>

</style>

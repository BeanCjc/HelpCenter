<template>
    <div>
        <el-table :data="tableData" border class="table" ref="multipleTable">
            <el-table-column prop="crdt" label="创建时间" align="center">
            </el-table-column>
            <el-table-column prop="preDeptName" label="上级部门名称" align="center">
            </el-table-column>
            <el-table-column prop="deptName" label="部门名称"align="center">
            </el-table-column>
            <el-table-column prop="deptAccount" label="账号"align="center">
            </el-table-column>

            <el-table-column label="操作" width="280" align="center">
                <template slot-scope="scope">
                    <el-button type="text"  @click="handleModify(scope.$index, scope.row)">修改</el-button>
                    <el-button type="text"  @click="handleModifyPass(scope.$index, scope.row)">密码重置</el-button>
                    <el-button type="text"  class="red" @click="handleDelete(scope.row)">删除</el-button>
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
    import bus from '../common/bus';
    export default {
        mounted() {
            this.dmLoadTableData();
        },
        props: {
            modifyVisible:'',
            resetVisible:'',
            dminput:'',
        },
        components: {

        },
        data() {
            return {
                selectionCol:false,
                tableData: [],
                departForm:{
                    preDeptName:'',
                    preDeptId: '',
                    deptName: '',
                    deptAccount: '',
                    deptPsw: ''
                },
                judgePage:0,
                currentPage: 1,
                pageTotal: 0,
                pageSize: 10,
                idx: -1,
            }
        },
        computed: {

        },
        methods: {
           /**
            * @Type:数据
            * @Description:初始化表格
            */
            dmLoadTableData: function (currentPage,filter) {
                let _this = this;
               currentPage===undefined?currentPage=_this.currentPage:currentPage;
                let params = Object.assign({}, {
                    rowCount: _this.pageSize,
                    pageIndex: currentPage

                }, filter);
                _this.$axios.get('api/Dept/pagelist',{
                    params: params,
                }).then(res => {
                    let data = res.data.data
                    data.info.forEach(item => {
                        if (item.preDeptName===null) {
                            item.preDeptName = '无上级部门'
                        }
                    });

                    _this.tableData = data.info;
                    _this.pageTotal = data.totalCount;
                    _this.currentPage = currentPage;
                    _this.judgePage =0;

                })
                    .catch(function (error) {
                        console.log(error);
                    });
            },


            /**
             * @Type:数据
             * @Description:搜索表格数据
             */
            searchData(deptName,filter) {
                let _this = this;
                _this.judgePage =1;
                deptName==undefined?'':_this.dminput=deptName;

//                debugger
                let params = Object.assign({}, {
                    rowCount: _this.pageSize,
                    pageIndex: 1,
//                    pageIndex: _this.currentPage,
                    deptName:_this.dminput

                }, filter);
                this.$axios.get('api/Dept/keyword/deptname',{
                    params: params,
                }).then(res => {
                    let data = res.data.data;

                    data.info.forEach(item => {
                        if (item.preDeptName===null) {
                            item.preDeptName = '无上级部门'
                        }
                    });
                    _this.tableData = data.info;
                    _this.pageTotal = data.totalCount;
                    _this.judgePage =1;
                })
                    .catch(function (error) {
                        console.log(error);
                    });
            },


            /**
             * @Type:事件
             * @Description:分页事件
             */
            handleCurrentChange: function (val) {
                this.currentPage = val;
//                debugger
                if(this.judgePage==0){
                    this.dmLoadTableData();
                }else if(this.judgePage==1){
                    this.searchData();
                }
            },


            /**
             * @Type:
             * @Position:表格
             * @Description:修改选中行
             */
            handleModify(index, row){

                this.$emit("modifyVisibleChange",row);

//                this.$emit("modifyVisibleChange",true);
//                this.idx = index;
//                const item = this.tableData[index];
//                this.departForm = item;
//                this.$emit("dmEvent", this.departForm);//子组件向父组件传值
            },

            /**
             * @Type:
             * @Position:表格
             * @Description:重置密码
             */
            handleModifyPass(index, row){
                this.$emit("resetVisibleChange",true);
                this.idx = index;
                const item = this.tableData[index];
                this.departForm = item;
                this.$emit("dmEvent", this.departForm);//子组件向父组件传值
            },

            /**
             * @Type:
             * @Position:表格
             * @Description:删除选中行
             */
            handleDelete(row){
                this.$emit('onDelete', row);
            }

//            handleDelete(row){
//                let _this = this;
//                let params = {
//                    deptId:row.deptId
//                }
//                console.log(params);
//                this.$confirm('你真的删除这个部门吗?', '提示', {
//                    confirmButtonText: '确定',
//                    cancelButtonText: '取消',
//                    type: 'warning'
//                }).then(() => {
//                    _this.$axios.delete('api/Dept',{
//                        params: params,
//                    }).then(res => {
//                        let data = res.data
//                         console.log(res);
//                        if (res.result === false) {
//                            _this.$message.error(data.tips);
//                        } else {
//                            _this.$message(data.tips);
//                            this.dmLoadTableData();
//                        }
//                    });
//                })
//            },

        },
        watch: {

        },
        beforeDestroy() {

        }
    }
</script>

<style scoped>

</style>

<template>
    <div>
        <el-table :data="tableData" border class="table" ref="multipleTable">
            <el-table-column prop="crdt" label="创建时间" align="center">
            </el-table-column>
            <el-table-column prop="deptName" label="部门名称" align="center">
            </el-table-column>
            <el-table-column prop="usrName" label="人员名称"align="center">
            </el-table-column>
            <el-table-column prop="usrVerifystate" label="审核状态"align="center">
            </el-table-column>
            <el-table-column prop="usrAccount" label="账号" align="center">
            </el-table-column>

            <el-table-column label="操作" width="280" align="center">
                <template slot-scope="scope">
                    <el-button type="text"  @click="modifyTableDpf(scope.$index, scope.row)">修改</el-button>
                    <el-button type="text"  @click="modifyTableDpfPass(scope.$index, scope.row)">密码重置</el-button>
                    <el-button type="text"  class="red" @click="dpfTableDelete(scope.row)">删除</el-button>
                    <!--<el-button type="text"  class="red" @click="dpfTableDisable(scope.row)">禁用</el-button>-->
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
            this.dpfLoadData();
        },
        props: {
            modifyVisible:'',
            resetVisible:'',
            dptinput:'',
            judgePageInIt:''
        },
        components: {

        },
        data() {
            return {
                selectionCol:false,
                tableData: [],
                departPeopleForm:{
                    preDeptId: '',
                    deptName: '',
                    deptAccount: '',
                    deptPsw: ''
                },
                judgePage:0,
                currentPage: 1,
                pageTotal: 0,
                pageSize: 10,
                treeNodeDeptId:''

            }
        },
        computed: {

        },
        methods: {
            /**
             * @Type:数据
             * @Description:初始化表格
             */
            dpfLoadData: function (currentPage,filter) {
                let _this = this;
                currentPage===undefined?currentPage=_this.currentPage:currentPage;
                let params = Object.assign({}, {
                    rowCount: _this.pageSize,
                    pageIndex:currentPage
                }, filter);


                this.$axios.get('api/Dept/userlist',{
                    params: params,
                }).then(res => {
                    let data = res.data.data;
                    data.info.forEach(item => {
                        if (item.usrVerifystate===1) {
                            item.usrVerifystate = '审核通过'
                        }else if(item.usrVerifystate===0){
                            item.usrVerifystate = '审核不通过'
                        }
                    });
                    _this.tableData = data.info;
                    _this.pageTotal = data.totalCount;
                    _this.currentPage = currentPage;
                    _this.judgePage =0;
                })
            },
            /**
             * @Type:数据
             * @Description:搜索表格数据
             */
            searchDptData(userName,filter) {
                let _this = this;
                _this.judgePage =1;
                userName==undefined?'':_this.dptinput=userName;
                let params = Object.assign({}, {
                    rowCount: _this.pageSize,
                    pageIndex: _this.currentPage,
                    userName:_this.dptinput

                }, filter);
                this.$axios.get('api/User/keyword/username',{
                    params: params,
                }).then(res => {
                    let data = res.data.data;
                    data.info.forEach(item => {
                        if (item.usrVerifystate===1) {
                            item.usrVerifystate = '审核通过'
                        }else if(item.usrVerifystate===0){
                            item.usrVerifystate = '审核不通过'
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
             * @Type:数据
             * @Description:根据树搜索表格数据
             */
            searchTreeDptData(deptId) {
                let _this = this;
                let params ={
                    rowCount: _this.pageSize,
                    pageIndex: _this.currentPage,
                    deptId:deptId

                };
                _this.judgePage=2;
                _this.treeNodeDeptId = deptId;

                this.$axios.get('api/Dept/userlist',{
                    params: params,
                }).then(res => {
                    let data = res.data.data;
                    data.info.forEach(item => {
                        if (item.usrVerifystate===1) {
                            item.usrVerifystate = '审核通过'
                        }else if(item.usrVerifystate===0){
                            item.usrVerifystate = '审核不通过'
                        }
                    });
                    _this.tableData = data.info;
                    _this.pageTotal = data.totalCount;
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
                  if(this.judgePage==0){
                      this.dpfLoadData();
                  }else if(this.judgePage==1){
                      this.searchDptData();
                  }else if(this.judgePage==2){
                      this.searchTreeDptData(this.treeNodeDeptId);
                  }

            },

            /**
             * @Type:
             * @Position:表格
             * @Description:修改选中行
             */
            modifyTableDpf(index, row){
                this.$emit("modifyVisibleChange",row);
//                this.$emit("modifyVisibleChange",true);
//                this.idx = index;
//                const item = this.tableData[index];
//                this.departPeopleForm = item;
//                this.$emit("dpfEvent", this.departPeopleForm);//子组件向父组件传值
            },

            /**
             * @Type:
             * @Position:表格
             * @Description:重置密码
             */
            modifyTableDpfPass(index, row){
                this.$emit("resetVisibleChange",true);
                this.idx = index;
                const item = this.tableData[index];
                this.departPeopleForm = item;
                this.$emit("dpfEvent", this.departPeopleForm);//子组件向父组件传值
            },

            /**
             * @Type:
             * @Position:表格
             * @Description:删除选中行
             */
            dpfTableDelete(row){
                let _this = this;
                let params = {
                    usrId:row.usrId
                }

                console.log(row.usrid);

//                debugger
                this.$confirm('你真的删除这个部门人员吗?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
//                    _this.$axios.delete('api/User',{
//                        params: params,
//                    })
                    _this.$axios.delete('api/User'+"?usrId="+row.usrid)
                        .then(res => {
                        let data = res.data
                        console.log(res);
                        if (res.result === false) {
                            _this.$message.error(data.tips);
                        } else {
                            _this.$message(data.tips);
                            this.dpfLoadData();
                        }
                    });
                })
            },

            /**
             * @Type:
             * @Position:表格
             * @Description:禁用选中行
             */
            dpfTableDisable(row){
                let _this = this;
                row.usrVerifystate == "启用"?row.usrVerifystate = 0:row.usrVerifystate = 1;
                _this.$axios.put('api/User/enable/userid'+"?UsrId="+row.usrid,{
                    usrVerifystate: row.usrVerifystate
                })
                    .then(res => {
                        if (data.result == false) {
                            _this.$message.error(data.tips);
                        } else {
                            _this.$message({
                                message:res.data.tips,
                                type: 'success'
                            });
                        }
                      this.dpfLoadData();
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },



        },
        watch: {

        },
        beforeDestroy() {

        }
    }
</script>

<style scoped>

</style>

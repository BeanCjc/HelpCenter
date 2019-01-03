<template>
    <div>
        <el-table :data="tableData" border class="table" ref="multipleTable">
            <el-table-column prop="crdt" label="创建时间" align="center">
            </el-table-column>
            <el-table-column prop="usrPhoneNum" label="手机号" align="center">
            </el-table-column>
            <el-table-column prop="usrAccount" label="账号"align="center">
            </el-table-column>
            <el-table-column prop="usrState" label="状态"align="center">
            </el-table-column>
            <el-table-column prop="usrName" label="昵称" align="center">
            </el-table-column>
            <el-table-column prop="roleName" label="角色" align="center">
            </el-table-column>

            <el-table-column label="操作" width="280" align="center">
                <template slot-scope="scope">
                    <el-button type="text"  @click="handleModify(scope.$index, scope.row)">修改</el-button>
                    <el-button type="text"  @click="handleModifyPass(scope.$index, scope.row)">密码重置</el-button>
                    <el-button type="text"  class="red" @click="handleDelete(scope.row)">删除</el-button>
                    <el-button type="text"  class="red" @click="handleDisable(scope.row)">禁用</el-button>
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
            this.uMloadData();
        },
        props: {
            modifyVisible:'',
            resetVisible:'',
            dptinput:''
        },
        components: {

        },
        data() {
            return {
                selectionCol:false,
                tableData: [],
                userManageForm:{
                    usrName: '',
                    usrAccount: '',
                    usrPsw: '',
                    usrType: 0,
                    usrVerifyState:0,
                    usrPhoneNum: '',
                    UsrDeptId: 'a',
                    roleIdList:[],
                    usrState: 0
                },
                idx:-1,
                judgePage:0,
                currentPage: 1,
                pageTotal: 0,
                pageSize: 10,
            }
        },
        computed: {

        },
        methods: {
            /**
             * @Type:数据
             * @Description:初始化表格
             */
            uMloadData: function (currentPage,filter) {
                let _this = this;
                currentPage===undefined?currentPage=_this.currentPage:currentPage;
                let params = Object.assign({}, {
                    rowCount: _this.pageSize,
                    pageIndex: currentPage,
                    usrType:'1,2,3,4'

                }, filter);
                this.$axios.get('/api/User/pagelist',{
                    params: params,
                }).then(res => {
                    let data = res.data.data
                    data.info.forEach(item => {
                        if (item.usrState===1) {
                            item.usrState = '启用'
                        }else if(item.usrState===0){
                            item.usrState = '禁用'
                        }
                    });
                    _this.tableData = data.info;
                    _this.pageTotal = data.totalCount;
                    _this.currentPage = currentPage;
                    _this.judgePage =0;



                    console.log(_this.currentPage);
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
//                    pageIndex: 1,
                    pageIndex: _this.currentPage,
                    userName:_this.dptinput,
                    usrType:'1,2,3,4'

                }, filter);
//                debugger
                this.$axios.get('api/User/keyword/username',{
                    params: params,
                }).then(res => {
//                  debugger
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
             * @Type:事件
             * @Description:分页事件
             */
            handleCurrentChange: function (val) {

                this.currentPage = val;
                if(this.judgePage==0){
                    this.uMloadData();
                }else if(this.judgePage==1){
                    this.searchDptData();
                }
            },

            /**
             * @Type:
             * @Position:表格
             * @Description:修改选中行
             */
            handleModify(index, row){
//              debugger
                this.$emit("modifyVisibleChange",row);


//                this.$emit("modifyVisibleChange",true);
//                this.idx = index;
//                const item = this.tableData[index];
//                this.userManageForm = item;
//                this.userManageForm.roleIdList = [];
//
//                this.$emit("umtEvent", this.userManageForm);//子组件向父组件传值
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
                this.userManageForm = item;
                this.$emit("umtEvent", this.userManageForm);//子组件向父组件传值
            },

            /**
             * @Type:
             * @Position:表格
             * @Description:删除选中行
             */
            handleDelete(row){
                let _this = this;
                let params = {
                    usrId:row.usrId
                }
                console.log(params);
                this.$confirm('你真的删除这个用户吗?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    _this.$axios.delete('api/User',{
                        params: params,
                    }).then(res => {
                        let data = res.data
                        console.log(res);
                        if (res.result === false) {
                            _this.$message.error(data.tips);
                        } else {
                            _this.$message(data.tips);
                            this.uMloadData();
                        }
                    });
                })
            },

            /**
             * @Type:
             * @Position:表格
             * @Description:禁止
             */
            handleDisable(row){
                    let _this = this;
                    row.usrState == "禁用"?row.usrState = 1:row.usrState = 0;
                    _this.$axios.put('api/User/enable/userid'+"?UsrId="+row.usrId+'&usrState='+row.usrState)
                    .then(res => {
                        _this.$message({
                            message:res.data.tips,
                            type: 'success'
                        });
                        this.uMloadData();
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

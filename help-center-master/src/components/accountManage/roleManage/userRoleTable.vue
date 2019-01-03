<template>
    <div>
        <el-table :data="tableData" border class="table" ref="multipleTable">
            <el-table-column prop="crdt" label="创建时间" align="center">
            </el-table-column>
            <el-table-column prop="roleName" label="角色名称" align="center">
            </el-table-column>
            <el-table-column prop="crUsrName" label="创建人"align="center">
            </el-table-column>
            <el-table-column prop="roleState" label="状态"align="center">
            </el-table-column>
            <el-table-column prop="updt" label="修改时间" align="center">
            </el-table-column>
            <el-table-column prop="upUsrName" label="修改人" align="center">
            </el-table-column>

            <el-table-column label="操作" width="280" align="center">
                <template slot-scope="scope">
                    <el-button type="text"  @click="handleModify(scope.$index, scope.row)">修改</el-button>
                    <!--<el-button type="text"  @click="authorization(scope.$index, scope.row)">授权</el-button>-->
                    <el-button type="text"  @click="authorization(scope.$index, scope.row)">授权</el-button>
                    <!--<el-button type="text"  @click="handleModifyPass(scope.$index, scope.row)">密码重置</el-button>-->
                    <el-button type="text"  class="red" @click="handleDelete(scope.row)">删除</el-button>
                    <el-button type="text"  class="red" @click="handleDisable(scope.row)">禁用</el-button>
                </template>
            </el-table-column>
        </el-table>

        <!--授权-->
        <el-dialog title="授权" :visible.sync="authVisible" width="30%">
          <xy-authorization
          :roleMsg="roleMsg"
          ref="authorizationTree"
          @authorizationClose="authorizationClose">
          </xy-authorization>
        </el-dialog>

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

    import xyAuthorization from './Authorization.vue'
    export default {
        mounted() {
            this.loadUrtData();
        },
        props: {
            modifyVisible:'',
            resetVisible:'',
        },
        components: {
            xyAuthorization
        },
        data() {
            return {
                selectionCol:false,
                tableData: [],
                userRoleForm:{},
                currentPage: 1,
                pageTotal: 0,
                pageSize: 10,
                authVisible:false,
                roleMsg:''
            }
        },
        computed: {

        },
        methods: {
          /*初始化表格数据*/
            loadUrtData: function (filter) {
                let _this = this;
                let params = Object.assign({}, {
                    rowCount: _this.pageSize,
                    pageIndex: _this.currentPage

                }, filter);
                this.$axios.get('/api/Role/pagelist',{
                    params: params,
                }).then(res => {
                    let data = res.data.data;
                    data.info.forEach(item => {
                        if (item.roleState===1) {
                            item.roleState = '启用'
                        }else if(item.roleState===0){
                            item.roleState = '禁用'
                        }
                    });
                    _this.tableData = data.info;
                    _this.pageTotal = data.totalCount;
                })
            },

            /**
             * @Type:事件
             * @Description:分页事件
             */
            handleCurrentChange: function (val) {
                this.currentPage = val;
                this.loadUrtData();
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
//                this.userRoleForm = item;
//                this.$emit("rmEvent", this.userRoleForm);//子组件向父组件传值
            },
            /**
             * @Type:
             * @Position:表格
             * @Description:删除选中行
             */
            handleDelete(row){
                let _this = this;
                this.$confirm('你真的删除这个角色吗?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    _this.$axios.delete('api/Role'+"?roleId="+row.roleId)
                        .then(res => {
                            let data = res.data
                            console.log(res);
                            if (res.result === false) {
                                _this.$message.error(data.tips);
                            } else {
                                _this.$message(data.tips);
                                this.loadUrtData();
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
                row.roleState == "禁用"?row.roleState = 1:row.roleState = 0;
                _this.$axios.put('api/Role/enable/roleid'+"?roleId="+row.roleId+'&roleState='+row.roleState)
                    .then(res => {
                        _this.$message({
                            message:res.data.tips,
                            type: 'success'
                        });
                        this.loadUrtData();
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },

            /**
             * @Type:
             * @Position:
             * @Description:角色授权
             */
            authorization(index,row){
                this.authVisible = true;
                this.roleMsg = row;

                setTimeout(()=>{
                    this.$refs.authorizationTree.resetChecked();//先清空
                    this.$refs.authorizationTree.LoadRoleModule(this.roleMsg.roleId);//后赋值
                },10)
            },
            /**
             * @Type:
             * @Position:
             * @Description:角色授权关闭
             */
            authorizationClose(){
                this.authVisible = false;
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

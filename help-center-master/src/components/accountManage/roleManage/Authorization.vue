<template>

    <div class="authorization-box">
        <p class="p-title">授权的角色:<span>{{roleMsg.roleName}}</span></p>
        <el-tree
            :data="treeData"
            ref="tree"
            show-checkbox
            node-key="moduleId"
            default-expand-all
            :default-checked-keys="RoleModuleId"
            :props="defaultProps">
        </el-tree>
        <div class="authorization-footer">
            <el-button @click="authorizeCancel()">取消</el-button>
            <el-button type="primary" @click="authorizeConfirm()">确定</el-button>
        </div>
    </div>


</template>

<script>

    export default {
        mounted() {
            this.loadUrtData();
//            this.LoadRoleModule();
            console.log(this.roleMsg.roleId);
        },
        props: {
            roleMsg:''
        },
        components: {

        },
        data() {
            return {
              roleName:'',
              pageIndex:1,
              rowCount:10,
              treeData:[],
              defaultProps: {
                   children: 'subs',
                    label: 'moduleName'
              },
                RoleModuleId:[]
            }
        },
        computed: {

        },
        methods: {
          /**
           * @Type:
           * @Position:
           * @Description:初始化树数据
           */
            loadUrtData: function (filter) {
                let _this = this;
                _this.$axios.get('api/SysModule/sysmoduletree').then(res => {
                    _this.treeData = res.data.data;
                })
            },
            /**
             * @Type:
             * @Position:
             * @Description:角色所拥有的模块
             */
            LoadRoleModule(e){
                let _this = this;
                let arrRoleModule = [];
//                let param = {
//                    roleId:this.roleMsg.roleId
//                };
//                _this.$axios.get('api/RoleSysModule/roleid'+"?roleId="+this.roleMsg.roleId).then(res => {
                _this.$axios.get('api/RoleSysModule/roleid'+"?roleId="+e).then(res => {
                    let arrTemp = res.data.data;
                    arrTemp.forEach(function (e,i) {
                        arrRoleModule.push(e.sysModuleId);
                    })
                    this.RoleModuleId = arrRoleModule;
//                    debugger
                })
                .catch(function (error) {
                    console.log(error);
                });
            },
            /**
             * @Type:
             * @Position:
             * @Description:取消
             */
            authorizeCancel(){
                this.$emit('authorizationClose');

            },
            /**
             * @Type:
             * @Position:
             * @Description:确定
             */
            authorizeConfirm(){
              let arr1 = this.$refs.tree.getCheckedKeys();
              let arr2 = this.$refs.tree.getHalfCheckedKeys();
              let arr3 = arr1.concat(arr2);
                let _this = this;
                let param = {
                    sysModuleIdList:arr3,
                    roleSysModuleState:1
                };
                _this.$axios.post('api/RoleSysModule'+"?roleId="+this.roleMsg.roleId,param)
                    .then(res => {
                        let data = res.data;
                        if (data.result == false) {
                            _this.$message.error(data.tips);
                        } else {
                            _this.$message({
                                message:res.data.tips,
                                type: 'success'
                            });
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
                this.$emit('authorizationClose');
            },

            /**
             * @Type:
             * @Position:
             * @Description:清除树状态
             */
            resetChecked() {
                this.$refs.tree.setCheckedKeys([]);
            }
        },
        watch: {

        },
        beforeDestroy() {

        }
    }
</script>

<style scoped>
    .authorization-box{
        padding: 4% 10% 20% 35%;
    }
    .authorization-box .p-title{
        height: 50px;
        line-height: 50px;
    }
    .authorization-box .p-title>span{
        color: #3a8ee6;
    }
    .authorization-box .authorization-footer{
        margin-top: 10%;
    }
</style>

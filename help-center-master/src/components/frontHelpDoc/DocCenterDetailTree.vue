<template>
    <div class="doc-detail-tree">
        <!--<el-tree-->
            <!--ref="tree"-->
            <!--:data="treeData"-->
            <!--:props="defaultProps"-->
            <!--lazy-->
            <!--:load="loadChild"-->
            <!--@node-click="handleNodeClick"-->
            <!--highlight-current="true"-->
        <!--&gt;-->
        <el-tree
            ref="tree"
            node-key="docTypeId"
            :data="treeData"
            :default-expanded-keys="docIdArr"
            :props="defaultProps"
            @node-click="handleNodeClick"
            :highlight-current="highCurr"
        >
        </el-tree>

        <!--<div class="buttons">-->
            <!--<el-button @click="getCheckedNodes">通过 node 获取</el-button>-->
            <!--<el-button @click="getCheckedKeys">通过 key 获取</el-button>-->
        <!--</div>-->
    </div>
</template>

<script>
    export default {
        props: {
            docId:''
        },
        mounted() {
            this.loadData();
        },
        data() {
            return {
                currentPage: 1,
                pageTotal: 0,
                pageSize: 10,
                treeData:[],
                defaultProps: {
                    children: 'subs',
                    label: 'docTypeName',
                    isLeaf: 'isLeaf'
                },
                highCurr:true,
                docIdArr:[],
            }
        },
        created(){
            this.docIdArr.push(this.docId);
            console.log(this.docIdArr);
//            this.$nextTick(() => {
//                this.$refs.tree.setCurrentKey('a5cefa3a-9a9d-4886-a9c3-5d524d1e0f24'); // treeBox 元素的ref   value 绑定的node-key
//            });
            setTimeout(() => {
                this.$refs.tree.setCurrentKey(this.docId);

            }, 300);

//            setTimeout
        },
        methods: {

            getCheckedNodes() {
                console.log(this.$refs.tree.getCheckedNodes());
            },
            getCheckedKeys() {
                console.log(this.$refs.tree.getCheckedKeys());
            },

            loadData() {

                this.$axios.get('api/DocTypeConfig/alllisttree')
                    .then((res) => {
                        this.treeData = res.data.data;
//                        debugger
                    })
                    .catch(function (error) {
                        console.log(error);
                    });


                /* 获取当前顶级文档分类*/
//                this.$axios.get('api/DocTypeConfig/toplist')
//                    .then((res) => {
//                        this.treeData = res.data.data;
//                    })
//                    .catch(function (error) {
//                        console.log(error);
//                    });

            },
            /*请求子节点*/
//            loadChild(node, resolve) {
//                if (!node.parent) {
//                    return;
//                }
//                if (node.data.child && node.data.child.length !== 0) {
//                    resolve(node.data.child);
//                    return;
//                }
//                let _this = this;
//                let params = {
//                    docTypeId:node.data.docTypeId
//                };
//                _this.$axios.get('api/DocTypeConfig',{
//                    params: params,
//                })
//                    .then((res) => {
//                        if (res.result == false) {
//                            _this.$message.error(res.tips);
//                        } else{
//                            resolve(res.data.data.childNodes);
//                        }
//                    })
//                    .catch(function (error) {
//                        console.log(error);
//                    });
//            },

            /**
             * @Type:树插件
             * @Description:节点被点击事件
             */
            handleNodeClick(data, node, self) {
                this.crUsrId = data.crUsrId;
                this.$emit('nodeClick', data, node, self);
            },
        }
    }
</script>


<style lang="less">

    .doc-detail-tree .el-tree--highlight-current .el-tree-node.is-current>.el-tree-node__content {
        background-color: #f0f7ff;
        color: #00c1de;
    }

</style>

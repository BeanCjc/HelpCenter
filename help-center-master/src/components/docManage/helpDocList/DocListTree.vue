<template>
    <div>
        <el-tree
            ref="tree"
            :data="treeData"
            :props="defaultProps"
            lazy
            :load="loadChild"
            @node-click="handleNodeClick"
        >
        <!--<el-tree-->
            <!--ref="tree"-->
            <!--:data="treeData"-->
            <!--:props="defaultProps"-->
        <!--&gt;-->
        </el-tree>
    </div>
</template>

<script>
    export default {
        mounted() {
            this.loadData();
        },
        data() {
            return {
                treeData:[],
                defaultProps: {
                    children: 'child',
                    label: 'docTypeName',
                    isLeaf: 'isLeaf'
                }
            }
        },
        props: {

        },
        methods: {
            loadData() {
                /* 获取当前顶级文档分类*/
                this.$axios.get('api/DocTypeConfig/toplist')
                    .then((res) => {
                        this.treeData = res.data.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });

            },
            /*请求子节点*/
            loadChild(node, resolve) {
                if (!node.parent) {
                    return;
                }
                if (node.data.child && node.data.child.length !== 0) {
                    resolve(node.data.child);
                    return;
                }
                let _this = this;
                let params = {
                    docTypeId:node.data.docTypeId
                };
                _this.$axios.get('api/DocTypeConfig',{
                    params: params,
                })
                    .then((res) => {
                        if (res.result == false) {
                            _this.$message.error(res.tips);
                        } else{
                            resolve(res.data.data.childNodes);
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },

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


<style scoped>

</style>

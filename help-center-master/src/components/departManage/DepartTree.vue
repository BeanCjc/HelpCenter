<template>
  <div>
      <el-tree
          ref="tree"
          node-key="deptId"
          :data="treeData"
          :props="defaultProps"
          lazy
          :load="loadChild"
          @node-click="handleNodeClick"
          >
      </el-tree>
  </div>
</template>

<script>
export default {
    mounted() {
        this.DTreeloadData();
    },
    data() {
        return {
            treeData:[],
            tableData:[],
            crUsrId:'',
            defaultProps: {
                children: 'child',
                label: 'deptName'
            },
//            currentPage: 1,
//            pageTotal: 0,
//            pageSize: 10,
        }
    },
    props: {
    },
      methods: {
          /**
           * @Type:数据
           * @Position:左侧
           * @Description:初始化树
           */
          DTreeloadData() {
              /* 获取当前用户所在部门及其子部门*/
              this.$axios.get('api/Dept/me')
                  .then((res) => {
                      this.treeData = res.data.data.detail;
                  })
                  .catch(function (error) {
                      console.log(error);
                  });

          },


          /**
           * @Type:数据
           * @Position:左侧
           * @Description:请求树子节点
           */
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
                  deptId:node.data.deptId
              };
              _this.$axios.get('api/Dept',{
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

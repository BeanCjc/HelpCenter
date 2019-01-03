<template>

    <xy-tree-select
        v-model="selectValue"
        :options="options"
        :normalizer="normalizer"
        :load-options="departLoadOptions"
        :auto-load-root-options="loading"
        :placeholder="placeholder"
        @input="setTreeSelectData"
    >
    </xy-tree-select>

</template>

<script>

    import TreeSelect from '@riophae/vue-treeselect';
    import '@riophae/vue-treeselect/dist/vue-treeselect.css';

    /**
     *  用户管理添加/修改表单
     */
    export default {
        props: {
            departMsg:''
        },
        mounted() {
            this.departTreeData();
        },
        components: {
            xyTreeSelect: TreeSelect,
        },
        data() {
            return {
//                selectValue: '默认',
                selectValue: null,
                loading: true,
                options:[],
                placeholder:'请选择部门',
                normalizer(node) {
                    return {
                        id: node.deptId,
                        label: node.deptName,
                        children: node.child,
                    }
                }
            }
        },
        methods: {
            /**
             * @Type:数据
             * @Position:下拉框
             * @Description:树初始化
             */
            departTreeData() {
                this.$axios.get('api/Dept/me')
                    .then((res) => {
                        let resTemp = res.data;
                        if (resTemp.result === false) {
                            _this.$message.error(resTemp.tips);
                        } else {
                          if(resTemp.data.detail === undefined){
                                return
                          }else{
                              if(resTemp.data.detail.length !== 0){
                                  resTemp.data.detail.forEach(item => {
                                      if (item.child) {
                                          item.child = null
                                      }
                                  });
                              }else {
                                  resTemp.data.detail.child = undefined
                              }
                          }
                        }
                        this.options = resTemp.data.detail;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },

            /**
             * @Type:数据
             * @Position:下拉框
             * @Description:树子节点
             */
            departLoadOptions({ action, parentNode, callback }) {
                if (action === 'LOAD_CHILDREN_OPTIONS') {
                    let _this = this;
                    let params = {
                        deptId:parentNode.deptId
                    };
                    _this.$axios.get('api/Dept',{
                        params: params,
                    }).then((res) => {
                        let resTemp = res.data;
                        if (resTemp.result === false) {
                            _this.$message.error(res.msg);
                        } else {
                            if(resTemp.data.childNodes.length !== 0){

                                parentNode.child = resTemp.data.childNodes;
                                parentNode.child.forEach(item => {
                                    if (item.child) {
                                        item.child = null
                                    }
                                });

                            }else {
                                parentNode.child = undefined
                            }
                            callback()
                        }
                    }).catch(function (error) {
                        console.log(error);
                    });
                }

            },

            /**
             * @Type:数据-传值
             * @Position:下拉框
             * @Description:把树下拉框的值传给父组件
             */
            setTreeSelectData(){
              this.$emit('transferDTSelect',this.selectValue);
              this.$emit('dtreeSelect');
            }


        },
        watch:{
//          部门树修改时的默认值
            departMsg:function (val) {
//              debugger
                let _this = this;
                let params = {
                    deptId:val
                };
//                this.selectValue = val;
                _this.$axios.get('api/Dept',{
                    params: params,
                })
                    .then((res) => {
                        _this.placeholder = res.data.data.detail.deptName
                    }).catch(function (error) {
                    console.log(error);
                });
            }
        }
    };

</script>

<style scoped lang="less">

</style>

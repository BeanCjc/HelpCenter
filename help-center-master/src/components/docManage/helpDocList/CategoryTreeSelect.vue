<template>

    <xy-tree-select
        v-model="selectValue"
        :options="options"
        :normalizer="normalizer"
        :load-options="cateLoadOptions"
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
            cateMsg:''
        },
        mounted() {
            this.departTreeData();
        },
        components: {
            xyTreeSelect: TreeSelect,
        },
        data() {
            return {
                selectValue: null,
                loading: true,
                options:[],
                placeholder:'请选择分类',
                normalizer(node) {
                    return {
                        id: node.docTypeId,
                        label: node.docTypeName,
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
                this.$axios.get('api/DocTypeConfig/toplist')
                    .then((res) => {
                        let _this = this;
                        let resTemp = res.data;
                        if (resTemp.result === false) {
                            _this.$message.error(resTemp.tips);
                        } else {
                          if(resTemp.data === undefined){
                                return
                          }else{
                              if(resTemp.data.length !== 0){
                                  resTemp.data.forEach(item => {
                                      if (item.child) {
                                          item.child = null
                                      }
                                  });
                              }else {
                                  resTemp.data.child = undefined
                              }
                          }
                        }
                        this.options = resTemp.data;
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
            cateLoadOptions({ action, parentNode, callback }) {
                if (action === 'LOAD_CHILDREN_OPTIONS') {
                    let _this = this;
                    let params = {
                        docTypeId:parentNode.docTypeId
                    };
                    _this.$axios.get('api/DocTypeConfig',{
                        params: params,
                    })
                        .then((res) => {
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
              console.log(this.selectValue);
              this.$emit('cateDTSelect',this.selectValue);
              this.$emit('dtreeSelect');
            }


        },
        watch:{
            cateMsg:function (val) {
//              debugger
                let _this = this;
                let params = {
                    docTypeId:val
                };
//                this.selectValue = val;

                _this.$axios.get('api/DocTypeConfig',{
                    params: params,
                })
                    .then((res) => {
                        _this.placeholder = res.data.data.detail.docTypeName
                    }).catch(function (error) {
                    console.log(error);
                });
            }
        }
    };

</script>

<style scoped lang="less">

</style>

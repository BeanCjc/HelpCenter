<template>
    <div class="depart-form">
        <el-form
            ref="form"
            :model="form"
            :rules="docRules"
            label-width="80px">
            <!--<el-form ref="departForm" :model="departForm" label-width="70px">-->

            <el-form-item label="排序号" prop="docTypeNum">
                <el-input v-model="form.docTypeNum"></el-input>
                <!--<el-input v-model="departForm.role"></el-input>-->
            </el-form-item>
            <el-form-item label="上级分类" prop="docPreTypeId">

                <xy-category-tree-select
                    @cateDTSelect="getDocTreeSelectData"
                    :cateMsg="cateMsg"
                ></xy-category-tree-select>


            </el-form-item>
            <el-form-item label="分类名称" prop="docTypeName">
                <el-input placeholder="" v-model="form.docTypeName"></el-input>
            </el-form-item>
            <el-form-item label="归属部门" prop="docTypeDeptId">

                <xy-depart-tree-select
                    @transferDTSelect="getTreeSelectData"
                    :departMsg="departMsg"
                ></xy-depart-tree-select>

            </el-form-item>
            <el-form-item style="text-align: right;">
                <el-button @click="docClose">取 消</el-button>
                <el-button type="primary" @click="onSubmit">确 定</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>
//    import TreeSelect from '@riophae/vue-treeselect';
//    import '@riophae/vue-treeselect/dist/vue-treeselect.css';

import xyDepartTreeSelect from '@/components/departManage/DepartTreeSelect.vue';
import xyCategoryTreeSelect from '@/components/docManage/helpDocList/CategoryTreeSelect.vue';


    /**
     *  用户管理添加/修改表单
     */
    export default {
        props: {
            formProps: {
                type: Object,
                default() {
                    return {
                        method: undefined,
                    }
                }
            }
        },
        mounted() {
            this.loadData();
            this.getTreeSelectData();
            this.getDocTreeSelectData();
//            this.dfTreeData();
//            this.dfTreeDataDept();
        },
        components: {
            xyDepartTreeSelect,
            xyCategoryTreeSelect,
        },
        data() {
            return {
                departMsg:'',
                cateMsg:'',
                preSelectData:'',
                DocSelectData:'',
                selectValue: null,
                selectDeptValue:null,
                loading: true,
                options:[],
                optionsDept:[],
                normalizer(node) {
                    return {
                        id: node.docTypeId,
                        label: node.docTypeName,
                        children: node.child,
                    }
                },
                normalizerDept(node) {
                    return {
                        id: node.deptId,
                        label: node.deptName,
                        children: node.child,
                    }
                },
                value: '',
                form: {
                    docTypeNum: undefined,
                    docPreTypeId: undefined,
                    docTypeName: undefined,
                    docTypeDeptId: undefined,
                },
                docRules: {
                    docTypeNum: [
                        {required: true, message: '请填写排序号', trigger: 'blur' },
//                        { type: 'number', message: '排序号必须为数字值', trigger: 'blur' },
                        { validator: this.pattern.isPositiveNumber,message: '请输入正数', trigger: ['blur', 'change']  },
                    ],
                    docTypeName: [
                        { required: true, message: '请输入分类名称', trigger: 'blur' }
                    ]
                }

            }
        },
        methods: {

            /**
             * @Type:数据-接值
             * @Description:接受部门treeSelect子组件传过来的数据
             */
            getTreeSelectData(msg){
                this.preSelectData = msg;
                console.log(msg);
            },

            /**
             * @Type:数据-接值
             * @Description:接受分类treeSelect子组件传过来的数据
             */
            getDocTreeSelectData(msg){
                this.DocSelectData = msg;
//                debugger
                console.log(msg);
            },

            /**
             * @Type:数据
             * @Position:表格行修改
             * @Description: 查询当前行，获取修改框的默认值
             */
            loadData(id) {
                let Id = id || this.formProps.docTypeId;
                this.$axios.get('/api/DocTypeConfig', {
                    params: {
                        docTypeId: Id
                    }
                }).then((res) => {
                    if (res.data.result) {
//                      debugger
                        this.form = res.data.data.detail;
                        this.departMsg = res.data.data.detail.docTypeDeptId;//获取归属部门
                        this.cateMsg = res.data.data.detail.docPreTypeId;//上级分类名称
                        debugger
                    }
                })
            },

            /**
             * @Type:提交
             * @Description:新增和修改
             */
            onSubmit() {
                let _this = this;
                if(_this.formProps.methods ==='put'){

//                    _this.form.docPreTypeId = _this.DocSelectData; //上级分类
//                    _this.form.docTypeDeptId = _this.preSelectData;//归属部门

                    if(_this.DocSelectData===undefined){
                        _this.form.docPreTypeId= _this.cateMsg;
                    }else{
                        _this.form.docPreTypeId = _this.DocSelectData;//上级分类
                    }
                    if(_this.preSelectData===undefined){
                        _this.form.docTypeDeptId= _this.departMsg;
                    }else{
                        _this.form.docTypeDeptId = _this.preSelectData;//归属部门
                    }
//                    this.$refs.form.validate((valid) => {
//                        if (valid) {
                    _this.$axios.put('/api/DocTypeConfig' + '?docTypeId=' + _this.formProps.docTypeId, _this.form).then((res) => {
                                if (res.data.result === false) {
                                    _this.$message.error(res.data.tips);
                                } else {
                                    _this.$message({
                                        message: res.data.tips,
                                        type: 'success'
                                    });
                                    _this.$emit('docFormSubmitSuccess');
                                }
                            })
//                        }
//                    })


                }
                if(_this.formProps.methods ==='post'){

//                    this.form.docPreTypeId =  this.preSelectData;
//                    this.form.docTypeDeptId = this.DocSelectData;

                    _this.form.docPreTypeId = _this.DocSelectData; //上级分类
                    _this.form.docTypeDeptId = _this.preSelectData;//归属部门

                    _this.$refs.form.validate((valid) => {
                            if (valid) {
                    _this.$axios.post('/api/DocTypeConfig', _this.form).then((res) => {
                                    if (res.data.result === false) {
                                        _this.$message.error(res.data.tips);
                                    } else {
                                        _this.$message({
                                            message: res.data.tips,
                                            type: 'success'
                                        });
                                        _this.$emit('docFormSubmitSuccess');
                                    }
                                })
                            }
                    })
              }
            },




            docClose() {
                this.$emit('docClose')
            },
            /**
             * 表单重置
             */
            resetForm() {
                debugger
                this.$refs.form.resetFields();

            },


        },
        watch: {
            /* ['userForm.role']:function (val) {
                 this.userForm.funIds = []
             }*/
        }
    };

</script>

<style scoped lang="less">
    .depart-form {
        .depart-el-select {
            width: 100%;
        }
    }

</style>

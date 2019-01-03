<template>
    <div class="doc-list-operate">
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-setting"></i>文档管理</el-breadcrumb-item>
                <el-breadcrumb-item>文档详情修改</el-breadcrumb-item>
            </el-breadcrumb>
        </div>

        <div class="container">
            <el-form label-width="70px" ref="form" :model="form" :rules="rules">
                <el-form-item label="排序号" prop="docNum">
                    <el-input v-model="form.docNum"></el-input>
                    <!--<el-input v-model.number="form.docNum" type="number" onkeypress="return( /[\d]/.test(String.fromCharCode(event.keyCode) ) )"></el-input>-->
                </el-form-item>
                <el-form-item label="分类名称">
                    <xy-category-tree-select
                        @cateDTSelect="getCateTSData"
                        :cateMsg="cateMsg"
                    ></xy-category-tree-select>
                </el-form-item>
                <el-form-item label="文档名称" prop="docTitle">
                    <el-input placeholder="安卓设备管理" v-model="form.docTitle"></el-input>
                </el-form-item>

                <el-form-item label="归属部门">
                    <xy-depart-tree-select
                        @transferDTSelect="getTreeSelectData"
                        :departMsg="departMsg"
                    ></xy-depart-tree-select>
                </el-form-item>

                <el-form-item label="查看权限" prop="viewRoleList">
                    <xy-role-select
                        @input="getViewRole"
                        :value="form.viewRoleList"
                    ></xy-role-select>
                </el-form-item>

                <el-form-item label="编辑权限" prop="viewRoleList">
                    <xy-role-select-edit
                        @input="getEditRole"
                        :value="form.editRoleList"
                    ></xy-role-select-edit>
                </el-form-item>

                <el-form-item label="游客查看">
                    <el-switch v-model="form.isDefaultRole"></el-switch>
                </el-form-item>

                <!--<el-form-item label="共享部门">-->
                    <!--<el-input v-model="form.docShareDeptList"></el-input>-->
                    <!--&lt;!&ndash;<el-input v-model="departForm.role"></el-input>&ndash;&gt;-->
                <!--</el-form-item>-->
                <div class="doc-edit-box">
                    <label>文档内容</label>
                    <xy-doc-edit
                        @docEditor="docEditor"
                        @docEditorText="docEditorText"
                        :docContent="docContent"
                        v-model="form.docContent"></xy-doc-edit>

                    <!--<el-input @docEditorText="docEditorText" v-model="form.docFullText" style="display: none"></el-input>-->
                </div>
                <!--<el-form-item label="操作人">-->
                    <!--<el-input v-model="value"></el-input>-->
                    <!--&lt;!&ndash;<el-input v-model="departForm.role"></el-input>&ndash;&gt;-->
                <!--</el-form-item>-->
                <el-form-item label="文档上传">
                    <!--<el-input v-model="value1"></el-input>-->
                    <!--<el-upload-->
                        <!--class="upload-demo"-->
                        <!--ref="upload"-->
                        <!--action="api/File"-->
                        <!--:on-preview="handlePreview"-->
                        <!--:on-remove="handleRemove"-->
                        <!--:before-remove="beforeRemove"-->
                        <!--multiple-->
                        <!--:limit="3"-->
                        <!--:auto-upload="false"-->
                        <!--:on-change="handleChange"-->
                        <!--:on-exceed="handleExceed"-->
                        <!--:on-success="handleSuccess"-->
                    <!--&gt;-->
                    <el-upload
                        class="upload-demo"
                        ref="upload"
                        action="api/File"
                        multiple
                        :limit="3"
                        :auto-upload="false"
                        :on-change="handleChange"
                        :on-success="handleSuccess"
                    >
                        <el-button type="primary">选择文件</el-button>
                    </el-upload>

                    <!--<el-input v-model="departForm.role"></el-input>-->
                </el-form-item>

                <el-form-item>
                    <el-button @click="cancel">取消</el-button>
                    <el-button type="primary" @click="onSubmit">确定</el-button>
                </el-form-item>
            </el-form>
        </div>

    </div>
</template>

<script>
    import xyDocEdit from './DocEditor.vue';
    import xyDepartTreeSelect from '@/components/departManage/DepartTreeSelect.vue';
    import xyCategoryTreeSelect from '@/components/docManage/helpDocList/CategoryTreeSelect.vue';
    import {mapGetters} from 'vuex'

    import xyRoleSelectEdit from './roleSelectEdit.vue';
    import xyRoleSelect from './roleSelect.vue';
    export default {
        name: 'docListOperateModify',

        data() {
            return {
                deptIdData:'',
                docTypeIdData:'',
                selectValue: null,
                selectSortValue:null,
                loading: true,
                options:[],
                docUpLoad:'',
                optionsSort:[],
                normalizer(node) {
                    return {
                        id: node.deptId,
                        label: node.deptName,
                        children: node.child,
                    }
                },
                normalizerSort(node) {
                    return {
                        id: node.deptId,
                        label: node.deptName,
                        children: node.child,
                    }
                },
                value: '',
                value1: '',
                form: {
                    viewRoleList:[],
                    editRoleList:[]
                },
                docContent:'',
                departMsg:'',
                cateMsg:'',
                rules: {
                    docNum: [
                       {required: true, message: '请填写排序号', trigger: 'blur' },
                        { validator: this.pattern.isPositiveNumber,message: '请输入正数', trigger: ['blur', 'change']  },
                    ],
                    docTitle: [
                        { required: true, message: '请填写文章标题', trigger: 'blur' }
                    ]
                }
            }
        },
        created() {
            this.id = this.$route.params.id;
//            this.id = this.$route.query.id;
            this.loadData();
        },
        mounted() {
            this.getTreeSelectData();
            this.getCateTSData();
        },
        components: {
            xyDocEdit,
            xyDepartTreeSelect,
            xyCategoryTreeSelect,
            xyRoleSelect,
            xyRoleSelectEdit
//            xyTreeSelect: TreeSelect,
        },
        computed: {
            ...mapGetters({
                tagIndex:'SET_MSGTAG'
            })
        },
        methods: {

            /*获取角色select*/
            getViewRole(val){
                this.form.viewRoleList = val
            },
            getEditRole(val){
                this.form.editRoleList = val
            },
            /**
             * @Type:数据-接值
             * @Description:接受treeSelect子组件传过来的数据
             */
            getTreeSelectData(msg){
                this.deptIdData = msg;

            },

            getCateTSData(msg){
                this.docTypeIdData = msg;

            },

            docEditor(html) {
                this.docContent = html;
                this.form.docContent = html
            },

            docEditorText(text){
                this.form.docFullText = text
            },
            handleSuccess(res, file) {
                this.docUpLoad = res.data;
                this.fileList = []
            },

            async handleChange() {
                this.$refs.upload.submit();
            },

            loadData() {
                this.$axios.get('/api/HelpDoc', {
                    params: {
                        helpDocId: this.id
                    }
                }).then((res) => {
                    if (!res.data.result) {
                        this.$message.error(res.data.tips);
                    } else {
                        this.form = res.data.data;
                        this.departMsg = res.data.data.docDeptId;//部门分类
                        this.cateMsg = res.data.data.docTypeId; //文档分类
//
//                        debugger
                        this.docContent = res.data.data.docContent;//富文本编辑器
                        this.docUpLoad = res.data.data.docAttachment//上传文件
                    }
                })
            },
            cancel(){
                this.$bus.$emit('closeTags',this.tagIndex);//关闭当前标签
                this.$router.push('./docList');
            },
            onSubmit() {
//
                this.form.docShareDeptList=[];
                if( this.docTypeIdData==undefined){
                    this.form.docTypeId = this.cateMsg;//把原来的分类又赋给它如果没有做修改
                }else{
                    this.form.docTypeId =  this.docTypeIdData;  //文档分类
                }
                if( this.deptIdData==undefined){
                    this.form.docDeptId = this.departMsg;
                }else{
                    this.form.docDeptId = this.deptIdData; //部门分类
                }

                this.form.docAttachment = this.docUpLoad;//上传的文件链接
//                this.$refs.upload.submit();//上传文件

                this.$refs.form.validate((valid) => {
                    if (valid) {
                        this.$axios.put('/api/HelpDoc' + '?helpDocId=' + this.id, this.form).then((res) => {
                            if (!res.data.result) {
                                this.$message.error(res.data.tips);
                            } else {
                                this.$message({
                                    message: res.data.tips,
                                    type: 'success'
                                });
                                this.$bus.$emit('closeTags', this.tagIndex);//关闭当前标签
                                this.$router.push('./docList');
                                this.$bus.$emit('DocListOperateAdd');
                            }
                        })
                    }
                })
            },

        },
        deactivated() {
            this.$destroy()
        }
    }

</script>

<style  lang="less">
    .doc-list-operate {
    .el-upload--text {
        background-color: #fff;
        border: none !important;
        border-radius: 6px;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
        width: auto;
        height: auto;
        text-align: center;
        cursor: pointer;
        position: relative;
        overflow: hidden;
    }
    .el-select {
        width:100%;
    }
    .el-form{
        width: 30%;
    }
    .handle-search-box span {
        line-height: 35px;
        margin-right: 15px;
    }
    .doc-edit-box {
        width: 150%;
        margin-bottom: 20px;
    }
    .doc-edit-box label {
        text-align: right;
        font-size: 14px;
        color: #606266;
        line-height: 40px;
        padding: 0 12px 0 0;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
    }
    }

</style>






































<template>
    <div class="doc-list-operate">
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-setting"></i>文档管理</el-breadcrumb-item>
                <el-breadcrumb-item>文档详情查看</el-breadcrumb-item>
            </el-breadcrumb>
        </div>

        <div class="container">
            <el-form label-width="70px" ref="form" :model="form">
                <el-form-item label="排序号" prop="docNum">
                    <el-input v-model="form.docNum"  :disabled="true"></el-input>
                </el-form-item>

                <el-form-item label="分类名称">
                    <xy-category-tree-select
                        @cateDTSelect="getCateTSData"
                        :cateMsg="cateMsg"
                    ></xy-category-tree-select>
                </el-form-item>


                <el-form-item label="文档名称" prop="docTitle">
                    <el-input placeholder="安卓设备管理" v-model="form.docTitle"  :disabled="true"></el-input>
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
                    <el-switch v-model="form.isDefaultRole"  :disabled="true"></el-switch>
                </el-form-item>

                <div class="doc-edit-box">
                    <label>文档内容</label>
                    <xy-doc-edit
                        @docEditor="docEditor"
                        @docEditorText="docEditorText"
                        :docContent="docContent"
                        v-model="form.docContent"></xy-doc-edit>
                </div>

                <el-form-item label="文档上传">
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
                </el-form-item>

                <!--<el-form-item  style="text-align: right">-->
                    <!--<el-button type="info" @click="cancel" style="width: 150px">退出</el-button>-->
                <!--</el-form-item>-->
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
        name: 'docListOperateView',

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
                cateMsg:''
            }
        },
        created() {
//            this.id = this.$route.query.id;
            this.id = this.$route.params.id;
            this.loadData();
        },
        mounted() {

        },
        components: {
            xyDocEdit,
            xyDepartTreeSelect,
            xyCategoryTreeSelect,
            xyRoleSelect,
            xyRoleSelectEdit
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
              let _this=this;
                _this.$axios.get('/api/HelpDoc', {
                    params: {
                        helpDocId: _this.id
                    }
                }).then((res) => {
                  debugger
                    if (!res.data.result) {
                        _this.$message.error(res.data.tips);
                    } else {
                        _this.form = res.data.data;
                        _this.departMsg = res.data.data.docDeptId;//部门分类
                        _this.cateMsg = res.data.data.docTypeId; //文档分类
//                        debugger
                        _this.docContent = res.data.data.docContent;//富文本编辑器
                        _this.docUpLoad = res.data.data.docAttachment//上传文件
                    }
                })
            },
            cancel(){
                this.$bus.$emit('closeTags',this.tagIndex);//关闭当前标签
                this.$router.push('./docList');
            },

        },
        watch:{

        },
        deactivated() {
            this.$destroy()
        }
    }

</script>

<style  lang="less">
    .doc-list-operate {
    .ql-container{
        max-height: 400px;
        overflow: auto;
    }
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






































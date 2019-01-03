<template>
    <div class="doc-list-operate">
        <div class="crumbs">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><i class="el-icon-setting"></i>文档管理
                </el-breadcrumb-item>
                <el-breadcrumb-item>文档详情新增</el-breadcrumb-item>
            </el-breadcrumb>
        </div>


        <div class="container">
            <el-form label-width="80px" ref="form" :model="form" :rules="rules">
                <el-form-item label="排序号" prop="docNum">
                    <el-input v-model="form.docNum"></el-input>
                    <!--<el-input v-model.number="form.docNum" type="number" onkeypress="return( /[\d]/.test(String.fromCharCode(event.keyCode) ) )"></el-input>-->
                </el-form-item>
                <el-form-item label="分类名称">
                    <xy-category-tree-select
                        @cateDTSelect="getCateTSData"
                    ></xy-category-tree-select>
                </el-form-item>
                <el-form-item label="文档名称" prop="docTitle">
                    <el-input placeholder="" v-model="form.docTitle"></el-input>
                </el-form-item>

                <el-form-item label="归属部门">
                    <xy-depart-tree-select
                        @transferDTSelect="getTreeSelectData"
                    ></xy-depart-tree-select>
                </el-form-item>

                <el-form-item label="查看权限" prop="viewRoleList">
                    <xy-role-select
                        @input="getViewRole"
                        :value="form.viewRoleList"
                    ></xy-role-select>
                </el-form-item>

                <el-form-item label="编辑权限" prop="viewRoleList">
                    <xy-role-select
                        @input="getEditRole"
                        :value="form.editRoleList"
                    ></xy-role-select>
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
                        v-model="form.docContent"></xy-doc-edit>

                    <!--<el-input  v-model="form.docFullText" style="display: none"></el-input>-->
                </div>
                <!--<el-form-item label="操作人">-->
                    <!--<el-input v-model="value"></el-input>-->
                    <!--&lt;!&ndash;<el-input v-model="departForm.role"></el-input>&ndash;&gt;-->
                <!--</el-form-item>-->
                <el-form-item label="文档上传">
                    <!--<el-input v-model="value1"></el-input>-->
                    <el-upload
                        class="upload-demo"
                        ref="upload"
                        action="api/File"
                        :on-preview="handlePreview"
                        :on-remove="handleRemove"
                        :before-remove="beforeRemove"
                        multiple
                        :limit="3"
                        :auto-upload="false"
                        :on-change="handleChange"
                        :on-exceed="handleExceed"
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


    import {mapGetters} from 'vuex'
    import xyDepartTreeSelect from '@/components/departManage/DepartTreeSelect.vue';
    import xyCategoryTreeSelect from '@/components/docManage/helpDocList/CategoryTreeSelect.vue';
    import xyRoleSelect from './roleSelect.vue'



    /*文档新增和修改组件*/
    import xyDocEdit from './DocEditor.vue';
    import TreeSelect from '@riophae/vue-treeselect';
    import '@riophae/vue-treeselect/dist/vue-treeselect.css';


    export default {
        name: 'docListOperateAdd',

        data() {
            return {
                value: '',
                value1: '',
                form: {
                    docNum:'',
                    docTitle:'',
                    isDefaultRole: true,
                    viewRoleList:[],
                    editRoleList:[],
                    docContent:'',
                    docFullText:''
                },
                deptIdData:'',
                docTypeIdData:'',
                docUpLoad:'',
                rules: {
                    docNum: [
                        {required: true, message: '请填写排序号', trigger: 'blur' },
//                        { type: 'number', message: '排序号必须为数字值', trigger: 'blur' },
                        { validator: this.pattern.isPositiveNumber,message: '请输入正数', trigger: ['blur', 'change']  },
                    ],
                    docTitle: [
                        { required: true, message: '请填写文章标题', trigger: 'blur' }
                    ]
                }
            }
        },
        components: {
            xyDocEdit,
            xyDepartTreeSelect,
            xyCategoryTreeSelect,
            xyRoleSelect
//            xyTreeSelect: TreeSelect,

        },
        mounted() {
            this.$refs.form.resetFields();
            this.getTreeSelectData();
            this.getCateTSData();
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
                console.log(msg);
            },

            getCateTSData(msg){
                this.docTypeIdData = msg;
            },

            docEditor(html) {
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

            cancel(){
                this.$router.push('./docList');
                this.$bus.$emit('closeTags',this.tagIndex);
            },
            onSubmit() {
              let _this = this;
                _this.form.docShareDeptList=[];
                _this.form.docTypeId = _this.docTypeIdData;  //文档分类
                _this.form.docDeptId = _this.deptIdData;//部门分类
                _this.form.docAttachment = _this.docUpLoad;//上传的文件链接

                _this.$refs.form.validate((valid) => {
                    if (valid) {
                      if(_this.form.docTypeId != undefined && _this.form.docDeptId != undefined && _this.form.docContent!= ''){
                          _this.$axios.post('/api/HelpDoc', _this.form).then((res) => {
                              if (!res.data.result) {
                                  _this.$message.error(res.data.tips);
                              } else {
                                  _this.$message({
                                      message: res.data.tips,
                                      type: 'success'
                                  });
                                  _this.$bus.$emit('closeTags',_this.tagIndex);//关闭当前标签
                                  _this.$router.push('./docList');
                                  _this.$bus.$emit('DocListOperateAdd');

                              }
                          })
                      }else{
                          if(_this.form.docTypeId==undefined){
                              _this.$message.error('文档分类未选择');
                              return false;
                          }
                          if(_this.form.docDeptId==undefined){
                              _this.$message.error('部门分类未选择');
                              return false;
                          }
                          if(_this.form.docContent==''){
                              _this.$message.error('请填写文章内容');
                              return false;
                          }
                      }
                    }else {
                        _this.$message.error('添加失败');
                        return false;
                    }
                });
            },




            /* 文件上传处理*/
            handleRemove(file, fileList) {
                console.log(file, fileList);
            },
            handlePreview(file) {
                console.log(file);
            },
            handleExceed(files, fileList) {
                this.$message.warning(`当前限制选择 3 个文件，本次选择了 ${files.length} 个文件，共选择了 ${files.length + fileList.length} 个文件`);
            },
            beforeRemove(file, fileList) {
                return this.$confirm(`确定移除 ${ file.name }？`);
            }
        },
        deactivated() {
            this.$destroy()
        }
    }

</script>

<style lang="less">



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






































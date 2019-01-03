webpackJsonp([10],{"9pAb":function(t,e,o){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var a=o("lC5x"),i=o.n(a),r=o("J0Oq"),l=o.n(r),s=o("4YfN"),n=o.n(s),c=o("92M0"),d=o("oweW"),u=o("Q+Tu"),m=o("9rMa"),f=o("xHuF"),p=o("8Fwj"),h={name:"docListOperateModify",data:function(){return{deptIdData:"",docTypeIdData:"",selectValue:null,selectSortValue:null,loading:!0,options:[],docUpLoad:"",optionsSort:[],normalizer:function(t){return{id:t.deptId,label:t.deptName,children:t.child}},normalizerSort:function(t){return{id:t.deptId,label:t.deptName,children:t.child}},value:"",value1:"",form:{viewRoleList:[],editRoleList:[]},docContent:"",departMsg:"",cateMsg:"",rules:{docNum:[{required:!0,message:"请填写排序号",trigger:"blur"},{validator:this.pattern.isPositiveNumber,message:"请输入正数",trigger:["blur","change"]}],docTitle:[{required:!0,message:"请填写文章标题",trigger:"blur"}]}}},created:function(){this.id=this.$route.params.id,this.loadData()},mounted:function(){this.getTreeSelectData(),this.getCateTSData()},components:{xyDocEdit:c.a,xyDepartTreeSelect:d.a,xyCategoryTreeSelect:u.a,xyRoleSelect:p.a,xyRoleSelectEdit:f.a},computed:n()({},Object(m.b)({tagIndex:"SET_MSGTAG"})),methods:{getViewRole:function(t){this.form.viewRoleList=t},getEditRole:function(t){this.form.editRoleList=t},getTreeSelectData:function(t){this.deptIdData=t},getCateTSData:function(t){this.docTypeIdData=t},docEditor:function(t){this.docContent=t,this.form.docContent=t},docEditorText:function(t){this.form.docFullText=t},handleSuccess:function(t,e){this.docUpLoad=t.data,this.fileList=[]},handleChange:function(){var t=this;return l()(i.a.mark(function e(){return i.a.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:t.$refs.upload.submit();case 1:case"end":return e.stop()}},e,t)}))()},loadData:function(){var t=this;this.$axios.get("/api/HelpDoc",{params:{helpDocId:this.id}}).then(function(e){e.data.result?(t.form=e.data.data,t.departMsg=e.data.data.docDeptId,t.cateMsg=e.data.data.docTypeId,t.docContent=e.data.data.docContent,t.docUpLoad=e.data.data.docAttachment):t.$message.error(e.data.tips)})},cancel:function(){this.$bus.$emit("closeTags",this.tagIndex),this.$router.push("./docList")},onSubmit:function(){var t=this;this.form.docShareDeptList=[],void 0==this.docTypeIdData?this.form.docTypeId=this.cateMsg:this.form.docTypeId=this.docTypeIdData,void 0==this.deptIdData?this.form.docDeptId=this.departMsg:this.form.docDeptId=this.deptIdData,this.form.docAttachment=this.docUpLoad,this.$refs.form.validate(function(e){e&&t.$axios.put("/api/HelpDoc?helpDocId="+t.id,t.form).then(function(e){e.data.result?(t.$message({message:e.data.tips,type:"success"}),t.$bus.$emit("closeTags",t.tagIndex),t.$router.push("./docList"),t.$bus.$emit("DocListOperateAdd")):t.$message.error(e.data.tips)})})}},deactivated:function(){this.$destroy()}},v={render:function(){var t=this,e=t.$createElement,o=t._self._c||e;return o("div",{staticClass:"doc-list-operate"},[o("div",{staticClass:"crumbs"},[o("el-breadcrumb",{attrs:{separator:"/"}},[o("el-breadcrumb-item",[o("i",{staticClass:"el-icon-setting"}),t._v("文档管理")]),t._v(" "),o("el-breadcrumb-item",[t._v("文档详情修改")])],1)],1),t._v(" "),o("div",{staticClass:"container"},[o("el-form",{ref:"form",attrs:{"label-width":"70px",model:t.form,rules:t.rules}},[o("el-form-item",{attrs:{label:"排序号",prop:"docNum"}},[o("el-input",{model:{value:t.form.docNum,callback:function(e){t.$set(t.form,"docNum",e)},expression:"form.docNum"}})],1),t._v(" "),o("el-form-item",{attrs:{label:"分类名称"}},[o("xy-category-tree-select",{attrs:{cateMsg:t.cateMsg},on:{cateDTSelect:t.getCateTSData}})],1),t._v(" "),o("el-form-item",{attrs:{label:"文档名称",prop:"docTitle"}},[o("el-input",{attrs:{placeholder:"安卓设备管理"},model:{value:t.form.docTitle,callback:function(e){t.$set(t.form,"docTitle",e)},expression:"form.docTitle"}})],1),t._v(" "),o("el-form-item",{attrs:{label:"归属部门"}},[o("xy-depart-tree-select",{attrs:{departMsg:t.departMsg},on:{transferDTSelect:t.getTreeSelectData}})],1),t._v(" "),o("el-form-item",{attrs:{label:"查看权限",prop:"viewRoleList"}},[o("xy-role-select",{attrs:{value:t.form.viewRoleList},on:{input:t.getViewRole}})],1),t._v(" "),o("el-form-item",{attrs:{label:"编辑权限",prop:"viewRoleList"}},[o("xy-role-select-edit",{attrs:{value:t.form.editRoleList},on:{input:t.getEditRole}})],1),t._v(" "),o("el-form-item",{attrs:{label:"游客查看"}},[o("el-switch",{model:{value:t.form.isDefaultRole,callback:function(e){t.$set(t.form,"isDefaultRole",e)},expression:"form.isDefaultRole"}})],1),t._v(" "),o("div",{staticClass:"doc-edit-box"},[o("label",[t._v("文档内容")]),t._v(" "),o("xy-doc-edit",{attrs:{docContent:t.docContent},on:{docEditor:t.docEditor,docEditorText:t.docEditorText},model:{value:t.form.docContent,callback:function(e){t.$set(t.form,"docContent",e)},expression:"form.docContent"}})],1),t._v(" "),o("el-form-item",{attrs:{label:"文档上传"}},[o("el-upload",{ref:"upload",staticClass:"upload-demo",attrs:{action:"api/File",multiple:"",limit:3,"auto-upload":!1,"on-change":t.handleChange,"on-success":t.handleSuccess}},[o("el-button",{attrs:{type:"primary"}},[t._v("选择文件")])],1)],1),t._v(" "),o("el-form-item",[o("el-button",{on:{click:t.cancel}},[t._v("取消")]),t._v(" "),o("el-button",{attrs:{type:"primary"},on:{click:t.onSubmit}},[t._v("确定")])],1)],1)],1)])},staticRenderFns:[]};var g=o("C7Lr")(h,v,!1,function(t){o("CyCa")},null,null);e.default=g.exports},CyCa:function(t,e){},"DM+h":function(t,e){},xHuF:function(t,e,o){"use strict";var a={props:{value:{required:!0}},mounted:function(){this.loadSelectData()},components:{},data:function(){return{role:[],currentValue:this.value}},computed:{},methods:{onChange:function(t){this.$emit("input",t)},loadSelectData:function(){var t=this;this.$axios.get("api/Role/getAllList").then(function(e){e.data.result?(t.role=e.data.data,console.log(t.role)):t.$message.info(e.data.tips)}).catch(function(t){console.log("下拉框异常"),console.log(t)})}},watch:{value:function(t){this.currentValue=t}},filters:{},beforeDestroy:function(){}},i={render:function(){var t=this,e=t.$createElement,o=t._self._c||e;return o("el-select",{attrs:{multiple:"",placeholder:"请选择角色"},on:{change:t.onChange},model:{value:t.currentValue,callback:function(e){t.currentValue=e},expression:"currentValue"}},t._l(t.role,function(t){return o("el-option",{key:t.roleId,attrs:{label:t.roleName,value:t.roleId}})}),1)},staticRenderFns:[]};var r=o("C7Lr")(a,i,!1,function(t){o("DM+h")},"data-v-255fd9cf",null);e.a=r.exports}});
webpackJsonp([6],{Igyo:function(e,t){},X8uN:function(e,t,s){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var a={props:{value:{required:!0}},mounted:function(){this.loadSelectData()},components:{},data:function(){return{role:[],currentValue:this.value}},computed:{},methods:{onChange:function(e){this.$emit("input",e)},loadSelectData:function(){var e=this;this.$axios.get("api/Role/getAllList").then(function(t){t.data.result?(e.role=t.data.data,console.log(e.role)):e.$message.info(t.data.tips)}).catch(function(e){console.log("下拉框异常"),console.log(e)})}},watch:{},filters:{},beforeDestroy:function(){}},r={render:function(){var e=this,t=e.$createElement,s=e._self._c||t;return s("el-select",{attrs:{multiple:"",placeholder:"请选择角色"},on:{change:e.onChange},model:{value:e.currentValue,callback:function(t){e.currentValue=t},expression:"currentValue"}},e._l(e.role,function(e){return s("el-option",{key:e.roleId,attrs:{label:e.roleName,value:e.roleId}})}),1)},staticRenderFns:[]};var o=s("C7Lr")(a,r,!1,function(e){s("sB4a")},"data-v-c5c18cd4",null).exports,i={props:{userManageForm:{type:Object},formProps:{type:Object,default:function(){return{method:void 0}}}},mounted:function(){this.loadData()},components:{xyUserManageRoleSelect:o},data:function(){return{userManageForm2:{usrPhoneNum:"",usrAccount:"",usrPsw:"",usrName:"",roleIdList:[],usrType:2,usrDeptId:"",usrVerifyState:1,usrState:1},umfOptions:[],role:"",rules:{usrPhoneNum:[{required:!0,message:"请输入手机号",trigger:"blur"}],usrAccount:[{required:!0,message:"请输入账号",trigger:"blur"}],usrPsw:[{required:!0,message:"请输入密码",trigger:"blur"}],usrName:[{required:!0,message:"请输入昵称",trigger:"blur"}]}}},methods:{loadData:function(e){var t=this,s=e||this.formProps.usrId||1;this.$axios.get("/api/User",{params:{usrId:s}}).then(function(e){e.data.result&&(t.userManageForm2=e.data.data)}).catch(function(e){console.log("DepartPeopleFrom的loadRowData方法catch到异常了"),console.log(e)})},getUserRole:function(e){this.userManageForm2.roleIdList=e},onSubmit:function(){if("post"===this.formProps.methods){var e=this,t=this.userManageForm2;this.$refs.form.validate(function(s){if(!s)return e.$message.error("添加失败"),!1;e.$axios.post("api/User",t).then(function(t){var s=t.data;0==s.result?e.$message.error(s.tips):e.$message(s.tips),e.$emit("modifyUmfSuccess")}).catch(function(e){console.log(e)})})}if("put"===this.formProps.methods){var s=this;s.$axios.put("api/User?UsrId="+this.formProps.usrId,this.userManageForm2).then(function(e){var t=e.data;0==t.result?s.$message.error(t.tips):(s.$message({message:e.data.tips,type:"success"}),s.$emit("modifyUmfSuccess"))}).catch(function(e){console.log(e)})}},umClose:function(){this.$emit("umClose")},resetForm:function(){this.$refs.form.resetFields()}},watch:{}},n={render:function(){var e=this,t=e.$createElement,s=e._self._c||t;return s("div",{staticClass:"depart-form"},[s("el-form",{ref:"form",attrs:{"label-width":"80px",model:e.userManageForm2,rules:e.rules}},[s("el-form-item",{attrs:{label:"手机号",prop:"usrPhoneNum"}},[s("el-input",{attrs:{placeholder:"请输入手机号"},model:{value:e.userManageForm2.usrPhoneNum,callback:function(t){e.$set(e.userManageForm2,"usrPhoneNum",t)},expression:"userManageForm2.usrPhoneNum"}})],1),e._v(" "),s("el-form-item",{attrs:{label:"账号",prop:"usrAccount"}},[s("el-input",{attrs:{placeholder:"请输入账号"},model:{value:e.userManageForm2.usrAccount,callback:function(t){e.$set(e.userManageForm2,"usrAccount",t)},expression:"userManageForm2.usrAccount"}})],1),e._v(" "),s("el-form-item",{attrs:{label:"密码",prop:"usrPsw"}},[s("el-input",{attrs:{placeholder:"请输入登录密码"},model:{value:e.userManageForm2.usrPsw,callback:function(t){e.$set(e.userManageForm2,"usrPsw",t)},expression:"userManageForm2.usrPsw"}})],1),e._v(" "),s("el-form-item",{attrs:{label:"昵称",prop:"usrName"}},[s("el-input",{attrs:{placeholder:"请输入昵称"},model:{value:e.userManageForm2.usrName,callback:function(t){e.$set(e.userManageForm2,"usrName",t)},expression:"userManageForm2.usrName"}})],1),e._v(" "),s("el-form-item",{attrs:{label:"角色",prop:"roleIdList"}},[s("xy-user-manage-role-select",{attrs:{value:e.userManageForm2.roleIdList},on:{input:e.getUserRole}})],1),e._v(" "),s("el-form-item",{attrs:{label:"状态",prop:"usrState"}},[s("el-radio-group",{model:{value:e.userManageForm2.usrState,callback:function(t){e.$set(e.userManageForm2,"usrState",t)},expression:"userManageForm2.usrState"}},[s("el-radio",{attrs:{label:1}},[e._v("启用")]),e._v(" "),s("el-radio",{attrs:{label:0}},[e._v("禁用")])],1)],1),e._v(" "),s("el-form-item",{staticStyle:{display:"none"},attrs:{label:"用户类型",prop:"usrType"}},[s("el-input",{model:{value:e.userManageForm2.usrType,callback:function(t){e.$set(e.userManageForm2,"usrType",t)},expression:"userManageForm2.usrType"}})],1),e._v(" "),s("el-form-item",{staticStyle:{display:"none"},attrs:{label:"用户部门id",prop:"UsrDeptId"}},[s("el-input",{model:{value:e.userManageForm2.UsrDeptId,callback:function(t){e.$set(e.userManageForm2,"UsrDeptId",t)},expression:"userManageForm2.UsrDeptId"}})],1),e._v(" "),s("el-form-item",{staticStyle:{display:"none"},attrs:{label:"审核状态",prop:"usrVerifyState"}},[s("el-radio-group",{model:{value:e.userManageForm2.usrVerifyState,callback:function(t){e.$set(e.userManageForm2,"usrVerifyState",t)},expression:"userManageForm2.usrVerifyState"}},[s("el-radio",{attrs:{label:"1"}},[e._v("审核通过")]),e._v(" "),s("el-radio",{attrs:{label:"0"}},[e._v("审核不通过")])],1)],1),e._v(" "),s("el-form-item",{staticStyle:{"text-align":"right"}},[s("el-button",{on:{click:e.umClose}},[e._v("取 消")]),e._v(" "),s("el-button",{attrs:{type:"primary"},on:{click:e.onSubmit}},[e._v("确 定")])],1)],1)],1)},staticRenderFns:[]};var l=s("C7Lr")(i,n,!1,function(e){s("keP2")},"data-v-549cd7fc",null).exports,u=s("aA9S"),c=s.n(u),m={mounted:function(){this.uMloadData()},props:{modifyVisible:"",resetVisible:"",dptinput:""},components:{},data:function(){return{selectionCol:!1,tableData:[],userManageForm:{usrName:"",usrAccount:"",usrPsw:"",usrType:0,usrVerifyState:0,usrPhoneNum:"",UsrDeptId:"a",roleIdList:[],usrState:0},idx:-1,judgePage:0,currentPage:1,pageTotal:0,pageSize:10}},computed:{},methods:{uMloadData:function(e){var t=this,s=c()({},{rowCount:t.pageSize,pageIndex:t.currentPage,usrType:"1,2,3,4"},e);this.$axios.get("/api/User/pagelist",{params:s}).then(function(e){var s=e.data.data;s.info.forEach(function(e){1===e.usrState?e.usrState="启用":0===e.usrState&&(e.usrState="禁用")}),t.tableData=s.info,t.pageTotal=s.totalCount,t.judgePage=0})},searchDptData:function(e,t){var s=this;s.judgePage=1,void 0!=e&&(s.dptinput=e);var a=c()({},{rowCount:s.pageSize,pageIndex:s.currentPage,userName:s.dptinput,usrType:"1,2,3,4"},t);this.$axios.get("api/User/keyword/username",{params:a}).then(function(e){var t=e.data.data;t.info.forEach(function(e){1===e.usrVerifystate?e.usrVerifystate="审核通过":0===e.usrVerifystate&&(e.usrVerifystate="审核不通过")}),s.tableData=t.info,s.pageTotal=t.totalCount,s.judgePage=1}).catch(function(e){console.log(e)})},handleCurrentChange:function(e){this.currentPage=e,0==this.judgePage?this.uMloadData():1==this.judgePage&&this.searchDptData()},handleModify:function(e,t){this.$emit("modifyVisibleChange",t)},handleModifyPass:function(e,t){this.$emit("resetVisibleChange",!0),this.idx=e;var s=this.tableData[e];this.userManageForm=s,this.$emit("umtEvent",this.userManageForm)},handleDelete:function(e){var t=this,s=this,a={usrId:e.usrId};console.log(a),this.$confirm("你真的删除这个部门吗?","提示",{confirmButtonText:"确定",cancelButtonText:"取消",type:"warning"}).then(function(){s.$axios.delete("api/User",{params:a}).then(function(e){var a=e.data;console.log(e),!1===e.result?s.$message.error(a.tips):(s.$message(a.tips),t.uMloadData())})})},handleDisable:function(e){var t=this,s=this;"禁用"==e.usrState?e.usrState=1:e.usrState=0,s.$axios.put("api/User/enable/userid?UsrId="+e.usrId+"&usrState="+e.usrState).then(function(e){s.$message({message:e.data.tips,type:"success"}),t.uMloadData()}).catch(function(e){console.log(e)})}},watch:{},beforeDestroy:function(){}},d={render:function(){var e=this,t=e.$createElement,s=e._self._c||t;return s("div",[s("el-table",{ref:"multipleTable",staticClass:"table",attrs:{data:e.tableData,border:""}},[s("el-table-column",{attrs:{prop:"crdt",label:"创建时间",align:"center"}}),e._v(" "),s("el-table-column",{attrs:{prop:"usrPhoneNum",label:"手机号",align:"center"}}),e._v(" "),s("el-table-column",{attrs:{prop:"usrAccount",label:"账号",align:"center"}}),e._v(" "),s("el-table-column",{attrs:{prop:"usrState",label:"状态",align:"center"}}),e._v(" "),s("el-table-column",{attrs:{prop:"usrName",label:"昵称",align:"center"}}),e._v(" "),s("el-table-column",{attrs:{prop:"roleName",label:"角色",align:"center"}}),e._v(" "),s("el-table-column",{attrs:{label:"操作",width:"280",align:"center"},scopedSlots:e._u([{key:"default",fn:function(t){return[s("el-button",{attrs:{type:"text"},on:{click:function(s){e.handleModify(t.$index,t.row)}}},[e._v("修改")]),e._v(" "),s("el-button",{attrs:{type:"text"},on:{click:function(s){e.handleModifyPass(t.$index,t.row)}}},[e._v("密码重置")]),e._v(" "),s("el-button",{staticClass:"red",attrs:{type:"text"},on:{click:function(s){e.handleDelete(t.row)}}},[e._v("删除")]),e._v(" "),s("el-button",{staticClass:"red",attrs:{type:"text"},on:{click:function(s){e.handleDisable(t.row)}}},[e._v("禁用")])]}}])})],1),e._v(" "),s("div",{staticClass:"pagination"},[s("el-pagination",{attrs:{"page-size":e.pageSize,layout:"total, prev, pager, next",total:e.pageTotal},on:{"current-change":e.handleCurrentChange}})],1)],1)},staticRenderFns:[]};var p={props:{userManageForm:{}},data:function(){var e=this;return{umPassForm:{pass:"",checkPass:""},rules2:{pass:[{validator:function(t,s,a){""===s?a(new Error("请输入密码")):(""!==e.umPassForm.checkPass&&e.$refs.umPassForm.validateField("checkPass"),a())},trigger:"blur"}],checkPass:[{validator:function(t,s,a){""===s?a(new Error("请再次输入密码")):s!==e.umPassForm.pass?a(new Error("两次输入密码不一致!")):a()},trigger:"blur"}]}}},methods:{umPassSubmit:function(){var e=this,t=this;console.log(this.userManageForm),t.$axios.put("api/User/resetpsw/account?usrAccount="+this.userManageForm.usrAccount+"&Password="+this.umPassForm.pass).then(function(s){var a=s.data;0==a.result?t.$message.error(a.tips):e.$message({showClose:!0,message:a.tips,type:"success"})}).catch(function(e){console.log(e)})}}},f={render:function(){var e=this,t=e.$createElement,s=e._self._c||t;return s("el-form",{ref:"umPassForm",staticClass:"demo-ruleForm",attrs:{model:e.umPassForm,"status-icon":"",rules:e.rules2,"label-width":"100px"}},[s("el-form-item",{attrs:{label:"密码",prop:"pass"}},[s("el-input",{attrs:{type:"password",autocomplete:"off"},model:{value:e.umPassForm.pass,callback:function(t){e.$set(e.umPassForm,"pass",t)},expression:"umPassForm.pass"}})],1),e._v(" "),s("el-form-item",{attrs:{label:"确认密码",prop:"checkPass"}},[s("el-input",{attrs:{type:"password",autocomplete:"off"},model:{value:e.umPassForm.checkPass,callback:function(t){e.$set(e.umPassForm,"checkPass",t)},expression:"umPassForm.checkPass"}})],1)],1)},staticRenderFns:[]},h={name:"userMange",data:function(){var e=this;return{formProps:{method:"put"},tip:"",dptinput:"",tableData:[],cur_page:1,multipleSelection:[],select_cate:"",select_word:"",del_list:[],is_search:!1,addVisible:!1,modifyVisible:!1,resetVisible:!1,delVisible:!1,userManageForm:{usrName:"",usrAccount:"",usrPsw:"",usrType:0,usrVerifyState:0,usrPhoneNum:"",UsrDeptId:"a",roleIdList:"",usrState:0,usrId:""},idx:-1,ruleForm2:{pass:"",checkPass:"",age:""},rules2:{pass:[{validator:function(t,s,a){""===s?a(new Error("请输入密码")):(""!==e.ruleForm2.checkPass&&e.$refs.ruleForm2.validateField("checkPass"),a())},trigger:"blur"}],checkPass:[{validator:function(t,s,a){""===s?a(new Error("请再次输入密码")):s!==e.ruleForm2.pass?a(new Error("两次输入密码不一致!")):a()},trigger:"blur"}]}}},created:function(){},components:{xyUserManageForm:l,xyUserManageTable:s("C7Lr")(m,d,!1,function(e){s("Igyo")},"data-v-f210b968",null).exports,xyUserManagePass:s("C7Lr")(p,f,!1,null,null,null).exports},computed:{data:function(){}},methods:{getUmData:function(e){this.userManageForm=e,this.$refs.umModify&&this.$refs.umModify.loadData(e.usrId)},uMloadData:function(){this.$refs.umTable.uMloadData()},modifyUmfSuccess:function(){this.$refs.umTable.uMloadData(),this.addVisible=!1},dsmSearch:function(){this.$refs.umTable.searchDptData()},addUM:function(){this.tip="用户新增",this.formProps.methods="post",this.addVisible=!0,this.$refs.umFrom&&this.$refs.umFrom.resetForm()},modifyVisibleFun:function(e){this.tip="用户修改",this.formProps=e,this.formProps.methods="put",this.$refs.umFrom&&this.$refs.umFrom.loadData(e.usrId),this.addVisible=!0},umClose:function(){this.addVisible=!1},resetVisibleFun:function(e){this.resetVisible=e},onUmfSubmit:function(){this.$refs.umAdd.onUmfSubmit(),this.$refs.umTable.uMloadData(),this.addVisible=!1},modifyUmfSubmit:function(){this.$refs.umModify.modifyUmfSubmit(this.userManageForm)},umPassSubmit:function(){this.$refs.umPass.umPassSubmit(),this.resetVisible=!1},deleteRow:function(){this.tableData.splice(this.idx,1),this.$message.success("删除成功"),this.delVisible=!1}}},g={render:function(){var e=this,t=e.$createElement,s=e._self._c||t;return s("div",{staticClass:"table"},[s("div",{staticClass:"crumbs"},[s("el-breadcrumb",{attrs:{separator:"/"}},[s("el-breadcrumb-item",[s("i",{staticClass:"el-icon-lx-calendar"}),e._v(" 账户管理")]),e._v(" "),s("el-breadcrumb-item",[e._v("用户管理")])],1)],1),e._v(" "),s("div",{staticClass:"container"},[s("el-row",{attrs:{gutter:20}},[s("el-col",{attrs:{span:8}},[s("div",{staticClass:"handle-box"},[s("el-button",{staticClass:"handle-add",attrs:{type:"primary",icon:"add"},on:{click:function(t){e.addUM()}}},[e._v("新增")])],1)]),e._v(" "),s("el-col",{attrs:{span:16}},[s("div",{staticClass:"handle-search-box"},[s("span",[e._v("昵称:")]),e._v(" "),s("el-input",{staticClass:"search-w search",attrs:{size:"small",clearable:"",placeholder:"请输入关键字进行搜索"},on:{clear:e.uMloadData},model:{value:e.dptinput,callback:function(t){e.dptinput=t},expression:"dptinput"}}),e._v(" "),s("el-button",{attrs:{type:"primary"},on:{click:e.dsmSearch}},[e._v("搜索")]),e._v(" "),s("el-button",{attrs:{type:"primary"},on:{click:e.uMloadData}},[e._v("重置")])],1)])],1),e._v(" "),s("xy-user-manage-table",{ref:"umTable",attrs:{dptinput:e.dptinput},on:{modifyVisibleChange:function(t){e.modifyVisibleFun(t)},resetVisibleChange:function(t){e.resetVisibleFun(t)},umtEvent:e.getUmData}})],1),e._v(" "),s("el-dialog",{attrs:{title:e.tip,visible:e.addVisible,width:"30%"},on:{"update:visible":function(t){e.addVisible=t}}},[s("xy-user-manage-form",{ref:"umFrom",attrs:{formProps:e.formProps,userManageForm:e.userManageForm},on:{modifyUmfSuccess:e.modifyUmfSuccess,umClose:e.umClose}})],1),e._v(" "),s("el-dialog",{attrs:{title:"修改密码",visible:e.resetVisible,width:"30%"},on:{"update:visible":function(t){e.resetVisible=t}}},[s("xy-user-manage-pass",{ref:"umPass",attrs:{userManageForm:e.userManageForm}}),e._v(" "),s("span",{staticClass:"dialog-footer",attrs:{slot:"footer"},slot:"footer"},[s("el-button",{on:{click:function(t){e.resetVisible=!1}}},[e._v("返回")]),e._v(" "),s("el-button",{attrs:{type:"primary"},on:{click:function(t){e.umPassSubmit()}}},[e._v("提交")])],1)],1),e._v(" "),s("el-dialog",{attrs:{title:"提示",visible:e.delVisible,width:"300px",center:""},on:{"update:visible":function(t){e.delVisible=t}}},[s("div",{staticClass:"del-dialog-cnt"},[e._v("删除不可恢复，是否确定删除？")]),e._v(" "),s("span",{staticClass:"dialog-footer",attrs:{slot:"footer"},slot:"footer"},[s("el-button",{on:{click:function(t){e.delVisible=!1}}},[e._v("取 消")]),e._v(" "),s("el-button",{attrs:{type:"primary"},on:{click:e.deleteRow}},[e._v("确 定")])],1)])],1)},staticRenderFns:[]};var b=s("C7Lr")(h,g,!1,function(e){s("a5JA")},"data-v-0b1d922d",null);t.default=b.exports},a5JA:function(e,t){},keP2:function(e,t){},sB4a:function(e,t){}});
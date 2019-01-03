webpackJsonp([4],{KnFx:function(e,t){},mIKP:function(e,t,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r={mounted:function(){this.loadData()},data:function(){return{treeData:[],crUsrId:"",defaultProps:{children:"child",label:"deptName"}}},props:{},methods:{loadData:function(){var e=this;this.$axios.get("api/Dept/me").then(function(t){e.treeData=t.data.data.detail}).catch(function(e){console.log(e)})},loadChild:function(e,t){if(e.parent)if(e.data.child&&0!==e.data.child.length)t(e.data.child);else{var a=this,r={deptId:e.data.deptId};a.$axios.get("api/Dept",{params:r}).then(function(e){0==e.result?a.$message.error(e.tips):t(e.data.data.childNodes)})}},handleNodeClick:function(e,t,a){this.crUsrId=e.crUsrId,this.$emit("nodeClick",e,t,a)}}},s={render:function(){var e=this.$createElement,t=this._self._c||e;return t("div",[t("el-tree",{ref:"tree",attrs:{"node-key":"deptId",data:this.treeData,props:this.defaultProps,lazy:"",load:this.loadChild},on:{"node-click":this.handleNodeClick}})],1)},staticRenderFns:[]};var o=a("C7Lr")(r,s,!1,function(e){a("zlji")},"data-v-20cc4650",null).exports,i=a("aA9S"),l=a.n(i),n={mounted:function(){this.dpfLoadData()},props:{modifyVisible:"",resetVisible:"",dptinput:"",judgePageInIt:""},components:{},data:function(){return{selectionCol:!1,tableData:[],departPeopleForm:{preDeptId:"",deptName:"",deptAccount:"",deptPsw:""},judgePage:0,currentPage:1,pageTotal:0,pageSize:10,treeNodeDeptId:""}},computed:{},methods:{dpfLoadData:function(e){var t=this,a=l()({},{rowCount:t.pageSize,pageIndex:t.currentPage},e);this.$axios.get("api/Dept/userlist",{params:a}).then(function(e){var a=e.data.data;a.info.forEach(function(e){1===e.usrVerifystate?e.usrVerifystate="审核通过":0===e.usrVerifystate&&(e.usrVerifystate="审核不通过")}),t.tableData=a.info,t.pageTotal=a.totalCount,t.judgePage=0})},searchDptData:function(e,t){var a=this;a.judgePage=1,void 0!=e&&(a.dptinput=e);var r=l()({},{rowCount:a.pageSize,pageIndex:a.currentPage,userName:a.dptinput},t);this.$axios.get("api/User/keyword/username",{params:r}).then(function(e){var t=e.data.data;t.info.forEach(function(e){1===e.usrVerifystate?e.usrVerifystate="审核通过":0===e.usrVerifystate&&(e.usrVerifystate="审核不通过")}),a.tableData=t.info,a.pageTotal=t.totalCount,a.judgePage=1}).catch(function(e){console.log(e)})},searchTreeDptData:function(e){var t=this,a={rowCount:t.pageSize,pageIndex:t.currentPage,deptId:e};t.judgePage=2,t.treeNodeDeptId=e,this.$axios.get("api/Dept/userlist",{params:a}).then(function(e){var a=e.data.data;a.info.forEach(function(e){1===e.usrVerifystate?e.usrVerifystate="审核通过":0===e.usrVerifystate&&(e.usrVerifystate="审核不通过")}),t.tableData=a.info,t.pageTotal=a.totalCount}).catch(function(e){console.log(e)})},handleCurrentChange:function(e){this.currentPage=e,0==this.judgePage?this.dpfLoadData():1==this.judgePage?this.searchDptData():2==this.judgePage&&this.searchTreeDptData(this.treeNodeDeptId)},modifyTableDpf:function(e,t){this.$emit("modifyVisibleChange",t)},modifyTableDpfPass:function(e,t){this.$emit("resetVisibleChange",!0),this.idx=e;var a=this.tableData[e];this.departPeopleForm=a,this.$emit("dpfEvent",this.departPeopleForm)},dpfTableDelete:function(e){var t=this,a=this;e.usrId;console.log(e.usrid),this.$confirm("你真的删除这个用户吗?","提示",{confirmButtonText:"确定",cancelButtonText:"取消",type:"warning"}).then(function(){a.$axios.delete("api/User?usrId="+e.usrid).then(function(e){var r=e.data;console.log(e),!1===e.result?a.$message.error(r.tips):(a.$message(r.tips),t.dpfLoadData())})})},dpfTableDisable:function(e){var t=this,a=this;"启用"==e.usrVerifystate?e.usrVerifystate=0:e.usrVerifystate=1,a.$axios.put("api/User/enable/userid?UsrId="+e.usrid,{usrVerifystate:e.usrVerifystate}).then(function(e){0==data.result?a.$message.error(data.tips):a.$message({message:e.data.tips,type:"success"}),t.dpfLoadData()}).catch(function(e){console.log(e)})}},watch:{},beforeDestroy:function(){}},d={render:function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("div",[a("el-table",{ref:"multipleTable",staticClass:"table",attrs:{data:e.tableData,border:""}},[a("el-table-column",{attrs:{prop:"crdt",label:"创建时间",align:"center"}}),e._v(" "),a("el-table-column",{attrs:{prop:"deptName",label:"部门名称",align:"center"}}),e._v(" "),a("el-table-column",{attrs:{prop:"usrName",label:"人员名称",align:"center"}}),e._v(" "),a("el-table-column",{attrs:{prop:"usrVerifystate",label:"审核状态",align:"center"}}),e._v(" "),a("el-table-column",{attrs:{prop:"usrAccount",label:"账号",align:"center"}}),e._v(" "),a("el-table-column",{attrs:{label:"操作",width:"280",align:"center"},scopedSlots:e._u([{key:"default",fn:function(t){return[a("el-button",{attrs:{type:"text"},on:{click:function(a){e.modifyTableDpf(t.$index,t.row)}}},[e._v("修改")]),e._v(" "),a("el-button",{attrs:{type:"text"},on:{click:function(a){e.modifyTableDpfPass(t.$index,t.row)}}},[e._v("密码重置")]),e._v(" "),a("el-button",{staticClass:"red",attrs:{type:"text"},on:{click:function(a){e.dpfTableDelete(t.row)}}},[e._v("删除")])]}}])})],1),e._v(" "),a("div",{staticClass:"pagination"},[a("el-pagination",{attrs:{"current-page":e.currentPage,"page-size":e.pageSize,layout:"total, prev, pager, next",total:e.pageTotal},on:{"current-change":e.handleCurrentChange,"update:currentPage":function(t){e.currentPage=t}}})],1)],1)},staticRenderFns:[]};var p=a("C7Lr")(n,d,!1,function(e){a("qv69")},"data-v-c801a5a4",null).exports,u=a("oweW"),c={props:{departPeopleForm:{type:Object},formProps:{type:Object,default:function(){return{method:void 0}}}},mounted:function(){this.loadRowData(),this.getTreeSelectData()},components:{xyDepartTreeSelect:u.a},data:function(){return{dpfForm:{usrAccount:"",usrDeptId:"",usrName:"",usrPsw:"",usrType:2,usrVerifyState:1,usrPhoneNum:1,roleIdList:["string"],usrState:0},treeSelectData:"",treeSelectName:"",departMsg:"",dpfRules:{usrName:[{required:!0,message:"请输入名字",trigger:"blur"}],usrAccount:[{required:!0,message:"请输入登录账号",trigger:"blur"}],usrPsw:[{required:!0,message:"请输入登录密码",trigger:"blur"}],usrType:[{required:!0,message:"请选择人员类型",trigger:"blur"}],usrVerifyState:[{required:!0,message:"请选择审核状态",trigger:"blur"}]}}},methods:{loadRowData:function(e){var t=this,a=e||this.formProps.usrid||1;this.$axios.get("/api/User",{params:{usrId:a}}).then(function(e){e.data.result&&(t.dpfForm=e.data.data,t.departMsg=e.data.data.usrDeptId)}).catch(function(e){console.log("DepartPeopleFrom的loadRowData方法catch到异常了"),console.log(e)})},getTreeSelectData:function(e){this.treeSelectData=e,console.log(e)},onSubmit:function(){var e=this;if("post"===this.formProps.methods){var t=this;t.dpfForm.usrDeptId=t.treeSelectData;var a=t.dpfForm;this.$refs.form.validate(function(r){if(!r)return t.$message.error("添加失败"),!1;t.$axios.post("api/User",a).then(function(a){console.log(a);var r=a.data;0==r.result?t.$message.error(r.tips):t.$message(r.tips),e.$emit("dpfSubmitSuccess")}).catch(function(e){console.log(e)})})}if("put"===this.formProps.methods){var r=this;void 0==r.treeSelectData?r.dpfForm.usrDeptId=this.departMsg:r.dpfForm.usrDeptId=r.treeSelectData,r.$axios.put("api/User?UsrId="+this.formProps.usrid,this.dpfForm).then(function(t){var a=t.data;0==a.result?r.$message.error(a.tips):(r.$message({message:t.data.tips,type:"success"}),e.$emit("dpfSubmitSuccess"))}).catch(function(e){console.log(e)})}},dpfClose:function(){this.$emit("dpfClose")},resetForm:function(){this.$refs.form.resetFields()}},watch:{}},f={render:function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("div",{staticClass:"depart-form"},[a("el-form",{ref:"form",attrs:{"label-width":"80px",model:e.dpfForm,rules:e.dpfRules}},[a("el-form-item",{attrs:{label:"部门名称",prop:"usrDeptId"}},[a("el-input",{staticStyle:{display:"none"},model:{value:e.dpfForm.usrDeptId,callback:function(t){e.$set(e.dpfForm,"usrDeptId",t)},expression:"dpfForm.usrDeptId"}}),e._v(" "),a("xy-depart-tree-select",{attrs:{departMsg:e.departMsg},on:{transferDTSelect:e.getTreeSelectData}})],1),e._v(" "),a("el-form-item",{attrs:{label:"人员名称",prop:"usrName"}},[a("el-input",{attrs:{placeholder:"请输入名字"},model:{value:e.dpfForm.usrName,callback:function(t){e.$set(e.dpfForm,"usrName",t)},expression:"dpfForm.usrName"}})],1),e._v(" "),a("el-form-item",{attrs:{label:"账号",prop:"usrAccount"}},[a("el-input",{attrs:{placeholder:"请输入登录账号"},model:{value:e.dpfForm.usrAccount,callback:function(t){e.$set(e.dpfForm,"usrAccount",t)},expression:"dpfForm.usrAccount"}})],1),e._v(" "),a("el-form-item",{attrs:{label:"密码",prop:"usrPsw"}},[a("el-input",{attrs:{placeholder:"请输入登录密码"},model:{value:e.dpfForm.usrPsw,callback:function(t){e.$set(e.dpfForm,"usrPsw",t)},expression:"dpfForm.usrPsw"}})],1),e._v(" "),a("el-form-item",{attrs:{label:"人员类型",prop:"usrType"}},[a("el-radio-group",{model:{value:e.dpfForm.usrType,callback:function(t){e.$set(e.dpfForm,"usrType",t)},expression:"dpfForm.usrType"}},[a("el-radio",{attrs:{label:2}},[e._v("正式人员")]),e._v(" "),a("el-radio",{attrs:{label:3}},[e._v("试用人员")])],1)],1),e._v(" "),a("el-form-item",{attrs:{label:"审核状态",prop:"usrVerifyState"}},[a("el-radio-group",{model:{value:e.dpfForm.usrVerifyState,callback:function(t){e.$set(e.dpfForm,"usrVerifyState",t)},expression:"dpfForm.usrVerifyState"}},[a("el-radio",{attrs:{label:1}},[e._v("审核通过")]),e._v(" "),a("el-radio",{attrs:{label:0}},[e._v("审核不通过")])],1)],1),e._v(" "),a("el-form-item",{staticStyle:{display:"none"},attrs:{label:"电话",prop:"usrPhoneNum"}},[a("el-input",{model:{value:e.dpfForm.usrPhoneNum,callback:function(t){e.$set(e.dpfForm,"usrPhoneNum",t)},expression:"dpfForm.usrPhoneNum"}})],1),e._v(" "),a("el-form-item",{staticStyle:{display:"none"},attrs:{label:"角色数组",prop:"roleIdList"}},[a("el-input",{model:{value:e.dpfForm.roleIdList,callback:function(t){e.$set(e.dpfForm,"roleIdList",t)},expression:"dpfForm.roleIdList"}})],1),e._v(" "),a("el-form-item",{staticStyle:{display:"none"},attrs:{label:"用户状态",prop:"usrState"}},[a("el-input",{model:{value:e.dpfForm.usrState,callback:function(t){e.$set(e.dpfForm,"usrState",t)},expression:"dpfForm.usrState"}})],1),e._v(" "),a("el-form-item",{staticStyle:{"text-align":"right"}},[a("el-button",{on:{click:e.dpfClose}},[e._v("取 消")]),e._v(" "),a("el-button",{attrs:{type:"primary"},on:{click:e.onSubmit}},[e._v("确 定")])],1)],1)],1)},staticRenderFns:[]};var m={props:{departPeopleForm:{}},data:function(){var e=this;return{ruleForm2:{pass:"",checkPass:""},rules2:{pass:[{validator:function(t,a,r){""===a?r(new Error("请输入密码")):(""!==e.ruleForm2.checkPass&&e.$refs.ruleForm2.validateField("checkPass"),r())},trigger:"blur"}],checkPass:[{validator:function(t,a,r){""===a?r(new Error("请再次输入密码")):a!==e.ruleForm2.pass?r(new Error("两次输入密码不一致!")):r()},trigger:"blur"}]}}},methods:{onPassSubmit:function(){var e=this,t=this;this.ruleForm2.pass;t.$axios.put("/api/User/resetpsw/account?usrAccount="+this.departPeopleForm.usrAccount+"&Password="+this.ruleForm2.pass).then(function(a){var r=a.data;0==r.result?t.$message.error(r.tips):e.$message({showClose:!0,message:r.tips,type:"success"})}).catch(function(e){console.log(e)})}}},h={render:function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("el-form",{ref:"ruleForm2",staticClass:"demo-ruleForm",attrs:{model:e.ruleForm2,"status-icon":"",rules:e.rules2,"label-width":"100px"}},[a("el-form-item",{attrs:{label:"密码",prop:"pass"}},[a("el-input",{attrs:{type:"password",autocomplete:"off"},model:{value:e.ruleForm2.pass,callback:function(t){e.$set(e.ruleForm2,"pass",t)},expression:"ruleForm2.pass"}})],1),e._v(" "),a("el-form-item",{attrs:{label:"确认密码",prop:"checkPass"}},[a("el-input",{attrs:{type:"password",autocomplete:"off"},model:{value:e.ruleForm2.checkPass,callback:function(t){e.$set(e.ruleForm2,"checkPass",t)},expression:"ruleForm2.checkPass"}})],1)],1)},staticRenderFns:[]},b={name:"departStaff",mounted:function(){},data:function(){return{modifyVisible:!1,addVisible:!1,resetVisible:!1,delVisible:!1,dptinput:"",departPeopleForm:{preDeptId:"",deptName:"",usrName:"",usrAccount:"",usrPsw:"",usrType:0,usrVerifyState:0,usrPhoneNum:1,usrDeptId:1,roleIdList:[1],usrState:0},formProps:{method:"put"},tip:""}},components:{xyDepartPeopleTree:o,xyDepartPeopleTable:p,xyDepartPeopleForm:a("C7Lr")(c,f,!1,function(e){a("KnFx")},"data-v-f597fcaa",null).exports,xyDepartPeoplePass:a("C7Lr")(m,h,!1,null,null,null).exports},computed:{data:function(){}},methods:{dsfLoadTableData:function(){this.$refs.dpftable.dpfLoadData()},dpfSubmitSuccess:function(){this.$refs.dpftable.dpfLoadData(),this.addVisible=!1},dsmSearch:function(){this.$refs.dpftable.searchDptData()},getDpfData:function(e){this.departPeopleForm=e,this.$refs.dpfFrom&&this.$refs.dpfFrom.loadRowData(e.usrid)},addHandle:function(){this.tip="新增部门人员",this.formProps.methods="post",this.addVisible=!0,this.$refs.dpfFrom&&this.$refs.dpfFrom.resetForm()},modifyVisibleFun:function(e){this.tip="部门人员修改",this.formProps=e,this.formProps.methods="put",this.$refs.dpfFrom&&this.$refs.dpfFrom.loadRowData(e.usrid),this.addVisible=!0},dpfClose:function(){this.addVisible=!1},resetVisibleFun:function(e){this.resetVisible=e},onDpfSubmit:function(){this.$refs.dpfAdd.onDpfSubmit(),this.$refs.dpftable.dpfLoadData(),this.addVisible=!1},modifyDpfSubmit:function(){this.$refs.dpmModify.modifyDpfSubmit(),this.modifyVisible=!1},onPassSubmit:function(){this.$refs.dpfPass.onPassSubmit(),this.resetVisible=!1},nodeClickHandle:function(e,t,a){this.$refs.dpftable.searchTreeDptData(e.deptId)}}},g={render:function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("div",[a("div",{staticClass:"crumbs"},[a("el-breadcrumb",{attrs:{separator:"/"}},[a("el-breadcrumb-item",[a("i",{staticClass:"el-icon-setting"}),e._v("部门管理")]),e._v(" "),a("el-breadcrumb-item",[e._v("部门人员管理")])],1)],1),e._v(" "),a("div",{staticClass:"container"},[a("el-row",{attrs:{gutter:20}},[a("el-col",{attrs:{span:4}},[a("el-container",{staticStyle:{"max-height":"636px"}},[a("el-main",[a("el-scrollbar",{staticClass:"custom-scrollbar",staticStyle:{"min-height":"587px"}},[a("xy-depart-people-tree",{ref:"unitTree",attrs:{checkStrictly:!0},on:{nodeClick:e.nodeClickHandle}})],1)],1)],1)],1),e._v(" "),a("el-col",{attrs:{span:20}},[a("div",{staticClass:"handle-box"},[a("el-button",{staticClass:"handle-box-add",attrs:{icon:"el-icon-plus",type:"primary"},on:{click:e.addHandle}},[e._v("\n                        添加\n                    ")]),e._v(" "),a("div",{staticClass:"handle-search-box"},[a("span",[e._v("人员名称:")]),e._v(" "),a("el-input",{staticClass:"search",attrs:{size:"small",clearable:"",placeholder:"请输入关键字进行搜索"},on:{clear:e.dsfLoadTableData},model:{value:e.dptinput,callback:function(t){e.dptinput=t},expression:"dptinput"}}),e._v(" "),a("el-button",{attrs:{type:"primary"},on:{click:e.dsmSearch}},[e._v("搜索")]),e._v(" "),a("el-button",{attrs:{type:"primary"},on:{click:e.dsfLoadTableData}},[e._v("重置")])],1)],1),e._v(" "),a("xy-depart-people-table",{ref:"dpftable",attrs:{resetVisible:e.resetVisible,dptinput:e.dptinput},on:{dpfEvent:e.getDpfData,modifyVisibleChange:function(t){e.modifyVisibleFun(t)},resetVisibleChange:function(t){e.resetVisibleFun(t)}}})],1)],1)],1),e._v(" "),a("el-dialog",{attrs:{title:e.tip,visible:e.addVisible,width:"30%"},on:{"update:visible":function(t){e.addVisible=t}}},[a("xy-depart-people-form",{ref:"dpfFrom",attrs:{formProps:e.formProps,departPeopleForm:e.departPeopleForm},on:{dpfSubmitSuccess:e.dpfSubmitSuccess,dpfClose:e.dpfClose}})],1),e._v(" "),a("el-dialog",{attrs:{title:"重置密码",visible:e.resetVisible,width:"30%"},on:{"update:visible":function(t){e.resetVisible=t}}},[a("xy-depart-people-pass",{ref:"dpfPass",attrs:{departPeopleForm:e.departPeopleForm}}),e._v(" "),a("span",{staticClass:"dialog-footer",attrs:{slot:"footer"},slot:"footer"},[a("el-button",{on:{click:function(t){e.resetVisible=!1}}},[e._v("取 消")]),e._v(" "),a("el-button",{attrs:{type:"primary"},on:{click:e.onPassSubmit}},[e._v("确 定")])],1)],1)],1)},staticRenderFns:[]};var v=a("C7Lr")(b,g,!1,function(e){a("mptV")},"data-v-ba92caf0",null);t.default=v.exports},mptV:function(e,t){},qv69:function(e,t){},zlji:function(e,t){}});
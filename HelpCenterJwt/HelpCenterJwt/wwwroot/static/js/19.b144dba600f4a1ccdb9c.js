webpackJsonp([19],{Yvry:function(e,t){},wYWi:function(e,t,o){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var a={mounted:function(){this.loadUrtData(),console.log(this.roleMsg.roleId)},props:{roleMsg:""},components:{},data:function(){return{roleName:"",pageIndex:1,rowCount:10,treeData:[],defaultProps:{children:"subs",label:"moduleName"},RoleModuleId:[]}},computed:{},methods:{loadUrtData:function(e){var t=this;t.$axios.get("api/SysModule/sysmoduletree").then(function(e){t.treeData=e.data.data})},LoadRoleModule:function(e){var t=this,o=[];this.$axios.get("api/RoleSysModule/roleid?roleId="+e).then(function(e){e.data.data.forEach(function(e,t){o.push(e.sysModuleId)}),t.RoleModuleId=o}).catch(function(e){console.log(e)})},authorizeCancel:function(){this.$emit("authorizationClose")},authorizeConfirm:function(){var e=this.$refs.tree.getCheckedKeys(),t=this.$refs.tree.getHalfCheckedKeys(),o=this,a={sysModuleIdList:e.concat(t),roleSysModuleState:1};o.$axios.post("api/RoleSysModule?roleId="+this.roleMsg.roleId,a).then(function(e){var t=e.data;0==t.result?o.$message.error(t.tips):o.$message({message:e.data.tips,type:"success"})}).catch(function(e){console.log(e)}),this.$emit("authorizationClose")},resetChecked:function(){this.$refs.tree.setCheckedKeys([])}},watch:{},beforeDestroy:function(){}},s={render:function(){var e=this,t=e.$createElement,o=e._self._c||t;return o("div",{staticClass:"authorization-box"},[o("p",{staticClass:"p-title"},[e._v("授权的角色:"),o("span",[e._v(e._s(e.roleMsg.roleName))])]),e._v(" "),o("el-tree",{ref:"tree",attrs:{data:e.treeData,"show-checkbox":"","node-key":"moduleId","default-expand-all":"","default-checked-keys":e.RoleModuleId,props:e.defaultProps}}),e._v(" "),o("div",{staticClass:"authorization-footer"},[o("el-button",{on:{click:function(t){e.authorizeCancel()}}},[e._v("取消")]),e._v(" "),o("el-button",{attrs:{type:"primary"},on:{click:function(t){e.authorizeConfirm()}}},[e._v("确定")])],1)],1)},staticRenderFns:[]};var r=o("C7Lr")(a,s,!1,function(e){o("Yvry")},"data-v-2270726e",null);t.default=r.exports}});
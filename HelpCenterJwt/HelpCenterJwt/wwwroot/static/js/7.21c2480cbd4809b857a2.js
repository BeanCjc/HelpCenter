webpackJsonp([7],{"8+FI":function(t,e,s){"use strict";var n=new(s("n+6Y").default);e.a=n},LrfK:function(t,e){},MpTN:function(t,e,s){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var n=s("8+FI"),a={data:function(){return{collapse:!1,fullscreen:!1,name:"游客",message:2}},computed:{username:function(){var t=JSON.parse(localStorage.getItem("userMsg")).username;return t||this.name}},methods:{handleCommand:function(t){"loginout"===t?(localStorage.removeItem("username"),localStorage.removeItem("token"),this.$router.push("/docCenterSort")):"backShow"===t&&this.$router.push("/docCenterSort")},collapseChage:function(){this.collapse=!this.collapse,n.a.$emit("collapse",this.collapse)},handleFullScreen:function(){var t=document.documentElement;this.fullscreen?document.exitFullscreen?document.exitFullscreen():document.webkitCancelFullScreen?document.webkitCancelFullScreen():document.mozCancelFullScreen?document.mozCancelFullScreen():document.msExitFullscreen&&document.msExitFullscreen():t.requestFullscreen?t.requestFullscreen():t.webkitRequestFullScreen?t.webkitRequestFullScreen():t.mozRequestFullScreen?t.mozRequestFullScreen():t.msRequestFullscreen&&t.msRequestFullscreen(),this.fullscreen=!this.fullscreen}},mounted:function(){document.body.clientWidth<1500&&this.collapseChage()}},l={render:function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("div",{staticClass:"header"},[s("div",{staticClass:"collapse-btn",on:{click:t.collapseChage}},[s("i",{staticClass:"el-icon-menu"})]),t._v(" "),s("div",{staticClass:"logo"},[t._v("帮助中心")]),t._v(" "),s("div",{staticClass:"header-right"},[s("div",{staticClass:"header-user-con"},[s("div",{staticClass:"btn-fullscreen",on:{click:t.handleFullScreen}},[s("el-tooltip",{attrs:{effect:"dark",content:t.fullscreen?"取消全屏":"全屏",placement:"bottom"}},[s("i",{staticClass:"el-icon-rank"})])],1),t._v(" "),s("div",{staticClass:"btn-bell"},[s("el-tooltip",{attrs:{effect:"dark",content:t.message?"有"+t.message+"条未读消息":"消息中心",placement:"bottom"}},[s("router-link",{attrs:{to:"/tabs"}},[s("i",{staticClass:"el-icon-bell"})])],1),t._v(" "),t.message?s("span",{staticClass:"btn-bell-badge"}):t._e()],1),t._v(" "),t._m(0),t._v(" "),s("el-dropdown",{staticClass:"user-name",attrs:{trigger:"click"},on:{command:t.handleCommand}},[s("span",{staticClass:"el-dropdown-link"},[t._v("\n                    "+t._s(t.username)+" "),s("i",{staticClass:"el-icon-caret-bottom"})]),t._v(" "),s("el-dropdown-menu",{attrs:{slot:"dropdown"},slot:"dropdown"},[s("el-dropdown-item",{attrs:{command:"backShow"}},[t._v("回到前台")]),t._v(" "),s("el-dropdown-item",{attrs:{divided:"",command:"loginout"}},[t._v("退出登录")])],1)],1)],1)])])},staticRenderFns:[function(){var t=this.$createElement,e=this._self._c||t;return e("div",{staticClass:"user-avator"},[e("img",{attrs:{src:"static/img/img.jpg"}})])}]};var o=s("C7Lr")(a,l,!1,function(t){s("LrfK")},"data-v-4390a089",null).exports,i={mounted:function(){this.itemData()},data:function(){return{collapse:!1,items:[]}},computed:{onRoutes:function(){return this.$route.path.replace("/","")}},methods:{itemData:function(){var t=this;this.$axios.get("api/RoleSysModule/userid").then(function(e){e.data.data.length,t.items=e.data.data}).catch(function(t){console.log(t)})}},created:function(){var t=this;n.a.$on("collapse",function(e){t.collapse=e})}},c={render:function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("div",{staticClass:"sidebar"},[s("el-menu",{staticClass:"sidebar-el-menu",attrs:{"default-active":t.onRoutes,collapse:t.collapse,"background-color":"#324157","text-color":"#bfcbd9","active-text-color":"#20a0ff","unique-opened":"",router:""}},[t._l(t.items,function(e){return[e.subs?[s("el-submenu",{key:e.moduleCode,attrs:{index:e.moduleCode}},[s("template",{slot:"title"},[s("i",{class:e.moduleImgPath}),t._v(" "),s("span",[t._v(t._s(e.moduleName))])]),t._v(" "),t._l(e.subs,function(e){return[e.subs?s("el-submenu",{key:e.moduleCode,attrs:{index:e.moduleCode}},[s("template",{slot:"moduleName"},[t._v(t._s(e.moduleName))]),t._v(" "),t._l(e.subs,function(e,n){return s("el-menu-item",{key:n,attrs:{index:e.moduleCode}},[t._v("\n                            "+t._s(e.moduleName)+"\n                        ")])})],2):s("el-menu-item",{key:e.moduleCode,attrs:{index:e.moduleCode}},[t._v("\n                        "+t._s(e.moduleName)+"\n                    ")])]})],2)]:[s("el-menu-item",{key:e.moduleCode,attrs:{index:e.moduleCode}},[s("i",{class:e.moduleImgPath}),t._v(" "),s("span",{attrs:{slot:"title"},slot:"title"},[t._v("\n                    "+t._s(e.moduleName)+"\n                ")])])]]})],2)],1)},staticRenderFns:[]};var r=s("C7Lr")(i,c,!1,function(t){s("oNUA")},"data-v-45c7a249",null).exports,u=(s("9rMa"),{data:function(){return{tagsList:[]}},mounted:function(){var t=this;this.$bus.$on("closeTags",function(e){t.closeTags(e),console.log(e)})},methods:{getIndex:function(t){this.tagIndex=t},isActive:function(t){return t===this.$route.fullPath},closeTags:function(t){var e=this.tagsList.splice(t,1)[0],s=this.tagsList[t]?this.tagsList[t]:this.tagsList[t-1];s?e.path===this.$route.fullPath&&this.$router.push(s.path):this.$router.push("/")},closeAll:function(){this.tagsList=[],this.$router.push("/")},closeOther:function(){var t=this,e=this.tagsList.filter(function(e){return e.path===t.$route.fullPath});this.tagsList=e},setTags:function(t){this.tagIndex=this.tagsList.length,this.$store.commit("SET_MSGTAG",this.tagIndex),this.tagsList.some(function(e){return e.path===t.fullPath})||(this.tagsList.length>=8&&this.tagsList.shift(),this.tagsList.push({title:t.meta.title,path:t.fullPath,name:t.matched[1].components.default.name})),n.a.$emit("tags",this.tagsList)},handleTags:function(t){"other"===t?this.closeOther():this.closeAll()}},computed:{showTags:function(){return this.tagsList.length>0}},watch:{$route:function(t,e){this.setTags(t)}},created:function(){this.setTags(this.$route)}}),d={render:function(){var t=this,e=t.$createElement,s=t._self._c||e;return t.showTags?s("div",{staticClass:"tags"},[s("ul",t._l(t.tagsList,function(e,n){return s("li",{key:n,staticClass:"tags-li",class:{active:t.isActive(e.path)},on:{click:function(e){t.getIndex(n)}}},[s("router-link",{staticClass:"tags-li-title",attrs:{to:e.path}},[t._v("\n                "+t._s(e.title)+"\n            ")]),t._v(" "),s("span",{staticClass:"tags-li-icon",on:{click:function(e){t.closeTags(n)}}},[s("i",{staticClass:"el-icon-close"})])],1)})),t._v(" "),s("div",{staticClass:"tags-close-box"},[s("el-dropdown",{on:{command:t.handleTags}},[s("el-button",{attrs:{size:"mini",type:"primary"}},[t._v("\n                标签选项"),s("i",{staticClass:"el-icon-arrow-down el-icon--right"})]),t._v(" "),s("el-dropdown-menu",{attrs:{slot:"dropdown",size:"small"},slot:"dropdown"},[s("el-dropdown-item",{attrs:{command:"other"}},[t._v("关闭其他")]),t._v(" "),s("el-dropdown-item",{attrs:{command:"all"}},[t._v("关闭所有")])],1)],1)],1)]):t._e()},staticRenderFns:[]};var m={name:"Home",data:function(){return{tagsList:[],collapse:!1}},components:{vHead:o,vSidebar:r,vTags:s("C7Lr")(u,d,!1,function(t){s("SgNs")},null,null).exports},created:function(){var t=this;n.a.$on("collapse",function(e){t.collapse=e}),n.a.$on("tags",function(e){for(var s=[],n=0,a=e.length;n<a;n++)e[n].name&&s.push(e[n].name);t.tagsList=s})}},h={render:function(){var t=this.$createElement,e=this._self._c||t;return e("div",{staticClass:"wrapper"},[e("v-head"),this._v(" "),e("v-sidebar"),this._v(" "),e("div",{staticClass:"content-box",class:{"content-collapse":this.collapse}},[e("v-tags"),this._v(" "),e("div",{staticClass:"content"},[e("transition",{attrs:{name:"move",mode:"out-in"}},[e("keep-alive",{attrs:{include:this.tagsList}},[e("router-view")],1)],1)],1)],1)],1)},staticRenderFns:[]},v=s("C7Lr")(m,h,!1,null,null,null);e.default=v.exports},SgNs:function(t,e){},oNUA:function(t,e){}});
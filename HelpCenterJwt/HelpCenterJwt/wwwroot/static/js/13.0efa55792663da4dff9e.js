webpackJsonp([13],{PxPc:function(t,e){},WY0H:function(t,e){t.exports="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAB50lEQVR4Xu2ZbU7CQBCGZ6DFC/htqoYfgjFeQBMOhAfS62jUE5Ag/iDRShSP0ELHQEQhStpt9yNNX/52dtj32Znp7JSp4j+uuH4CAERAxQkgBSoeACiCSAGkQMUJOE2B/i4d1zyKW280cnUOzgAM9+go8hoPQjTdmESd5ju9uIDgBMBCPDEdzEULjRqT6NIFBOsA/ohfHLsjCFYBrBXvEII1AKniHUGwAuD5kJqJNO5+cj6t2llMB+MAZuKn5D8y8U6a7pXnliAYBZBbvMV0MAagsHhLEIwA0CZ+CUKNo87JKw2V0iiDsXYA2sV/ixCScZ3iC90QtAIwJf43EPRD0AbgaZ9a4vm3ytU+Q5gum+iOBC0AZuKp7t8T86ainlzmOiEUBmBbvO50KATAlXidEAoBGAT+VcK8lRbHLNKlDHarfuRTiK8z+P5oh/FNmt2654UAZP3TfuD3mPksq/18RCDSOw3jc5U1eWwBIA811TWIAKQAagCKIN4CeA2iD0AjpNI/oBNEK4y7AC5DuA3iOox5AAYiKr1DHltMhPJQU12DiRAmQhWfCA0Cv5swb6ukDouM22Gc+l1Axed/tlaKYNFNmlwPACbplsE3IqAMp2Ryj4gAk3TL4BsRUIZTMrlHRIBJumXw/QVzKl9QaWx69QAAAABJRU5ErkJggg=="},pWA8:function(t,e,a){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var n=a("aA9S"),o=a.n(n),i={name:"docCenter",mounted:function(){this.loadDC(),this.mostViewData(),this.hasToken()},data:function(){return{activeIndex2:"1",login:!0,hasLogin:!1,name:"游客",currentPage:1,pageSize:10,mostVData:[],keyword:"",token:null,searchDoc:!0}},computed:{username:function(){var t=JSON.parse(localStorage.getItem("userMsg")).username;return t||this.name},unreadNum:function(){return this.unread.length}},methods:{hasToken:function(){this.token=localStorage.getItem("token")},handleLoginOut:function(){localStorage.removeItem("username"),localStorage.removeItem("token"),this.$router.push("/login")},docSearch:function(){this.$router.push({path:"/docCenterSearchResults",query:{keyword:this.keyword}})},mostViewData:function(t){var e=this,a=o()({},{rowCount:this.pageSize,pageIndex:this.currentPage},t);this.$axios.get("api/HelpDoc/pagelist",{params:a}).then(function(t){var a=t.data.data;e.mostVData=a.info}).catch(function(t){console.log(t)})},loadDC:function(){localStorage.getItem("token")&&(this.login=!1,this.hasLogin=!0)},handDocDetail:function(t){this.$router.push({path:"/docCenterDetail",query:{id:t.docId}})},goManage:function(){localStorage.getItem("token")?this.$router.push("/docList"):(this.$message("请先登录"),this.$router.push("/login"))},handDocCenter:function(){this.$router.push({path:"/docCenterSort"})},handleLogin:function(){this.$router.push("/login")},handRegiste:function(){this.$router.push({path:"/login",query:{flat:!1}})},handleSelect:function(t,e){console.log(t,e)}}},s={render:function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("div",{staticClass:"doc-center"},[n("el-container",[n("div",{staticClass:"doc-mian-box"},[n("el-header",[n("el-menu",{staticClass:"el-menu",attrs:{"default-active":t.activeIndex2,mode:"horizontal","background-color":"#1c2327","text-color":"#fff","active-text-color":"#ffd04b"},on:{select:t.handleSelect}},[n("el-menu-item",{attrs:{index:"1"},on:{click:t.handDocCenter}},[t._v("文档中心")]),t._v(" "),t.token?n("el-menu-item",{attrs:{index:"2"},on:{click:t.goManage}},[t._v("\n                    后台管理\n                ")]):t._e(),t._v(" "),t.login?n("el-menu-item",{staticClass:"login-registered-box",attrs:{index:"3"}},[n("span",{staticClass:"cp",on:{click:t.handleLogin}},[t._v("登录")])]):t.hasLogin?n("el-menu-item",{staticClass:"login-registered-box",staticStyle:{color:"#fff !important","border-bottom":"none!important","background-color":"rgb(28, 35, 39)",cursor:"inherit!important"},attrs:{index:"3"}},[n("span",[t._v(t._s(t.username))]),t._v(",你好\n                    "),n("span",{staticClass:"cp",staticStyle:{color:"#00c1de","margin-left":"15px"},on:{click:t.handleLoginOut}},[t._v("退出")])]):t._e()],1)],1),t._v(" "),t.searchDoc?n("el-container",{staticClass:"search-container"},[n("div",{staticClass:"search-container-box"},[n("p",{staticClass:"search-title"},[t._v("文档中心")]),t._v(" "),n("el-input",{staticClass:"search",attrs:{size:"large",clearable:"",placeholder:"请输入关键字进行搜索"},model:{value:t.keyword,callback:function(e){t.keyword=e},expression:"keyword"}}),t._v(" "),n("el-button",{attrs:{icon:"el-icon-search"},on:{click:t.docSearch}})],1)]):t._e(),t._v(" "),n("el-container",{staticClass:"el-container-conetnt"},[n("el-main",[n("router-view",{key:t.$route.fullPath})],1),t._v(" "),n("el-aside",{staticClass:"el-aside-ol",attrs:{width:"300px"}},[n("div",{staticClass:"el-aside-box"},[n("p",{staticClass:"title"},[t._v("浏览最多")]),t._v(" "),n("ul",{staticClass:"a"},t._l(t.mostVData,function(e){return n("li",{on:{click:function(a){t.handDocDetail(e)}}},[t._v("\n                           "+t._s(e.docTitle)+"\n                           "),n("span",{staticClass:"span"},[n("span",[t._v(t._s(e.docCount))]),t._v(" "),n("img",{staticStyle:{"vertical-align":"middle","margin-bottom":"4px"},attrs:{src:a("WY0H"),width:"20px"}})])])}),0)])])],1)],1),t._v(" "),n("el-footer",[t._v("Copyright © 2017 轩亚科技提供技术支持   全国服务热线:400-6677370")])],1)],1)},staticRenderFns:[]};var c=a("C7Lr")(i,s,!1,function(t){a("PxPc")},null,null);e.default=c.exports}});
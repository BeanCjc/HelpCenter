webpackJsonp([16],{"7SEX":function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0});var o={name:"docCenterSort",data:function(){return{pageIndex:1,rowCount:10,docCateData:""}},mounted:function(){this.docTreeData()},methods:{docTreeData:function(){var t=this;this.$axios.get("/api/DocTypeConfig/homepage").then(function(e){t.docCateData=e.data.data}).catch(function(t){console.log(t)})},handDocSeeMore:function(t,e){this.$router.push({path:"/docCenterSeeMore",query:{docId:t.docTypeId}})},handDocDetailReceive:function(t,e){t.docList=t.docList.slice(0,8),t.isShowTxt=!0},handDocDetail:function(t){this.$router.push({path:"/docCenterDetail",query:{id:t.docId}})}},computed:{unreadNum:function(){return this.unread.length}}},a={render:function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("div",{staticClass:"dcsBox"},t._l(t.docCateData,function(e){return n("el-row",{attrs:{gutter:20}},[e.subs?n("h1",{staticClass:"doc-title"},[t._v("\n            "+t._s(e.docTypeName)+"\n        ")]):t._e(),t._v(" "),e.subs?t._l(e.subs,function(e,o){return n("el-col",{attrs:{span:8}},[n("div",{staticClass:"grid-content"},[n("div",{staticClass:"header"},[n("span",[n("img",{attrs:{src:e.docTypeImg}}),t._v("\n                    "+t._s(e.docTypeName)+"\n                ")]),t._v(" "),n("span",{staticClass:"doc-more",on:{click:function(n){t.handDocSeeMore(e,o)}}},[t._v("查看更多+")])]),t._v(" "),n("div",{staticClass:"main"},[n("ul",{staticClass:"mian-ul"},t._l(e.docList,function(e){return n("li",{on:{click:function(n){t.handDocDetail(e)}}},[t._v("\n                        "+t._s(e.docTitle)+"\n                        ")])}))])])])}):t._e()],2)}))},staticRenderFns:[]};var c=n("C7Lr")(o,a,!1,function(t){n("pHIZ")},null,null);e.default=c.exports},pHIZ:function(t,e){}});
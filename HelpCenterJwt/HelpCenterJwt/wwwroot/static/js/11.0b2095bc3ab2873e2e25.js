webpackJsonp([11],{G1sM:function(e,t){},WMPn:function(e,t,a){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var n={name:"docCenterSeeMore",data:function(){return{activeIndex:"1",activeIndex2:"1",docId:"",currentPage:1,pageTotal:0,pageSize:10,docSMData:[]}},created:function(){this.docId=this.$route.query.docId,this.loadData()},components:{xyDocCenterDetailTree:a("wyZe").a},methods:{handDocDetail:function(e){this.$router.push({path:"/docCenterDetail",query:{id:e.docId}})},handDocCenter:function(){this.$router.push({path:"/docCenterSort"})},loadData:function(){var e=this;this.$axios.get("api/HelpDoc/doctypeid",{params:{rowCount:this.pageSize,pageIndex:this.currentPage,docTypeId:this.docId,isRecursion:!0}}).then(function(t){t.data.result&&(e.docSMData=t.data.data.info,e.pageTotal=t.data.data.totalCount)})},nodeClickHandle:function(e,t,a){var n=this;this.$axios.get("api/HelpDoc/doctypeid",{params:{rowCount:this.pageSize,pageIndex:this.currentPage,docTypeId:e.docTypeId,isRecursion:!0}}).then(function(e){e.data.result&&(n.docSMData=e.data.data.info,n.pageTotal=e.data.data.totalCount)})},handleCurrentChange:function(e){this.currentPage=e,this.loadData()}},computed:{unreadNum:function(){return this.unread.length}}},o={render:function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("div",{staticClass:"doc-center-see-more"},[a("div",{staticClass:"article-path-box"},[a("span",{staticClass:"article-path"},[a("span",{staticClass:"doc-center-title",on:{click:e.handDocCenter}},[e._v("文档中心")]),e._v(" "),a("span",[e._v(" >")]),e._v("\n           查看更多\n       ")])]),e._v(" "),a("div",{staticClass:"article-conent"},[a("el-container",[a("el-aside",{attrs:{width:"200px"}},[a("xy-doc-center-detail-tree",{attrs:{docId:e.docId},on:{nodeClick:e.nodeClickHandle}})],1),e._v(" "),a("el-main",e._l(e.docSMData,function(t){return a("div",{staticClass:"docSeeMore",on:{click:function(a){e.handDocDetail(t)}}},[a("h4",[e._v(e._s(t.docTitle))])])}))],1),e._v(" "),a("div",{staticClass:"pagination"},[a("el-pagination",{attrs:{"page-size":e.pageSize,layout:"total, prev, pager, next",total:e.pageTotal},on:{"current-change":e.handleCurrentChange}})],1)],1)])},staticRenderFns:[]};var i=a("C7Lr")(n,o,!1,function(e){a("G1sM")},null,null);t.default=i.exports},rWWL:function(e,t){},wyZe:function(e,t,a){"use strict";var n={props:{docId:""},mounted:function(){this.loadData()},data:function(){return{currentPage:1,pageTotal:0,pageSize:10,treeData:[],defaultProps:{children:"subs",label:"docTypeName",isLeaf:"isLeaf"},highCurr:!0,docIdArr:[]}},created:function(){var e=this;this.docIdArr.push(this.docId),console.log(this.docIdArr),setTimeout(function(){e.$refs.tree.setCurrentKey(e.docId)},300)},methods:{getCheckedNodes:function(){console.log(this.$refs.tree.getCheckedNodes())},getCheckedKeys:function(){console.log(this.$refs.tree.getCheckedKeys())},loadData:function(){var e=this;this.$axios.get("api/DocTypeConfig/alllisttree").then(function(t){e.treeData=t.data.data}).catch(function(e){console.log(e)})},handleNodeClick:function(e,t,a){this.crUsrId=e.crUsrId,this.$emit("nodeClick",e,t,a)}}},o={render:function(){var e=this.$createElement,t=this._self._c||e;return t("div",{staticClass:"doc-detail-tree"},[t("el-tree",{ref:"tree",attrs:{"node-key":"docTypeId",data:this.treeData,"default-expanded-keys":this.docIdArr,props:this.defaultProps,"highlight-current":this.highCurr},on:{"node-click":this.handleNodeClick}})],1)},staticRenderFns:[]};var i=a("C7Lr")(n,o,!1,function(e){a("rWWL")},null,null);t.a=i.exports}});
<template>
   <div class="doc-center-see-more">
       <div class="article-path-box">
           <span class="article-path">
               <span class="doc-center-title" @click="handDocCenter">文档中心</span>
               <span> ></span>
               查看更多
           </span>
       </div>
       <div class="article-conent">

           <div class="docSeeMore"  v-for="item in docSMData" @click="handDocDetail(item)">
               <h4>{{ item.docTitle }}</h4>
               <!--<p>{{ item.docFullText }}</p>-->
           </div>

           <div class="pagination">
               <el-pagination
                   @current-change="handleCurrentChange"
                   :page-size="pageSize"
                   layout="total, prev, pager, next"
                   :total="pageTotal">
               </el-pagination>
           </div>

       </div>
   </div>
</template>

<script>
    export default {
        name: 'docCenterSearchResults',
        data() {
            return {
                activeIndex: '1',
                activeIndex2: '1',
                docId:'',
                currentPage: 1,
                pageTotal: 0,
                pageSize: 10,
                docSMData:[]
            }
        },
        created(){
            this.docId = this.$route.query.id;
            this.loadDataMore();
        },
        methods: {
            handDocDetail(row){
                this.$router.push({
                    path: '/docCenterDetail',
                    query:{
                        id:row.docId
                    }
                })
            },
            handDocCenter(){
                this.$router.push({
                    path: '/docCenterSort'
                })
            },
            handleSelect(key, keyPath) {
                console.log(key, keyPath);
            },


            loadDataMore(){
               let keyword = this.$route.query.keyword
                console.log( this.$route.query.keyword);
                this.$axios.get('api/HelpDoc/pagelist',{
                    params:{
                        pageIndex:this.currentPage,
                        rowCount:this.pageSize,
                        keyword:keyword
                    }
                }).then((res)=>{
                    if(res.data.result){
                        this.docSMData = res.data.data.info;
                        this.pageTotal = res.data.data.totalCount;
                    }
                })
            },

            /**
             * 分页事件
             */
            handleCurrentChange: function (val) {
                this.currentPage = val;
                this.loadDataMore();
            },
        },
        computed: {
            unreadNum(){
                return this.unread.length;
            }
        }
    }

</script>

<style lang="less">
    .doc-center-see-more{
        .el-main{
            padding: 20px 0 20px 20px;
        }
        .article-path-box{
            background-color: #f2f2f2;
            height: 30px;
            line-height: 30px;
            font-size: 14px;
            padding-left: 10px;
            padding-right: 10px;
        }
        .article-time{
            float: right;
        }
        .article-conent{
            margin-top: 20px;
        }
        .doc-center-title{
            cursor: pointer;
        }
        .docSeeMore{
            cursor: pointer;
            margin-bottom: 20px;
        }
    }

</style>












































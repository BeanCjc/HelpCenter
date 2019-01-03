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

            <el-container>
                <el-aside width="200px">
                    <xy-doc-center-detail-tree
                        :docId="docId"
                        @nodeClick="nodeClickHandle"
                    >
                    </xy-doc-center-detail-tree>
                </el-aside>
                <el-main>
                    <div class="docSeeMore"  v-for="item in docSMData" @click="handDocDetail(item)">
                        <h4>{{ item.docTitle }}</h4>
                        <!--<p>{{ item.docFullText }}</p>-->
                    </div>
                </el-main>
            </el-container>

            <div class="pagination">
                <el-pagination
                    @current-change="handleCurrentChange"
                    :current-page.sync="currentPage"
                    :page-size="pageSize"
                    layout="total, prev, pager, next"
                    :total="pageTotal">
                </el-pagination>
            </div>

        </div>
    </div>
</template>

<script>
    import xyDocCenterDetailTree from './DocCenterDetailTree.vue';
    export default {
        name: 'docCenterSeeMore',
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
            this.docId = this.$route.query.docId;
            this.loadData();
//            this.loadDataMore();
        },
        components: {
            xyDocCenterDetailTree
        },
        methods: {
            /**
             * @Type:
             * @Position:
             * @Description:跳到文章详情
             */
            handDocDetail(row){
                this.$router.push({
                    path: '/docCenterDetail',
                    query:{
                        id:row.docId
                    }
                })
            },
            /**
             * @Type:
             * @Position:
             * @Description:跳到文档中心
             */
            handDocCenter(){
                this.$router.push({
                    path: '/docCenterSort'
                })
            },
            /**
             * @Type:
             * @Position:
             * @Description:显示所有列表
             */
            loadData(){
                let _this = this;
                this.$axios.get('api/HelpDoc/doctypeid',{
                    params:{
                        rowCount: _this.pageSize,
                        pageIndex: _this.currentPage,
                        docTypeId:_this.docId,
                        isRecursion:true
                    }
                }).then((res)=>{
                    if(res.data.result){
                        this.docSMData = res.data.data.info;
                        this.pageTotal = res.data.data.totalCount;
                    }
                })
//                显示所有列表
//                this.$axios.get('/api/HelpDoc/pagelist',{
//                    params:{
//                        pageIndex:this.currentPage,
//                        rowCount:this.pageSize
//                    }
//                }).then((res)=>{
//                    if(res.data.result){
//                        this.docSMData = res.data.data.info;
//                        this.pageTotal = res.data.data.totalCount;
//                    }
//                })
            },
            /**
             * @Type:树插件
             * @Position:
             * @Description:点击树节点显示列表
             */
            nodeClickHandle(data, node, self){
              let _this = this;
                  _this.docId=data.docTypeId;
                  this.currentPage=1;
                this.$axios.get('api/HelpDoc/doctypeid',{
                    params:{
                        rowCount: _this.pageSize,
                        pageIndex: _this.currentPage,
                        docTypeId:data.docTypeId,
                        isRecursion:true
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
                this.loadData();
            }
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
    .el-tree{
        background: none;
    }
    .el-pager li {
       background: none;
    }
    .el-pagination button:disabled {
       background: none;
    }
    .el-pagination .btn-next, .el-pagination .btn-prev {
       background: none;
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












































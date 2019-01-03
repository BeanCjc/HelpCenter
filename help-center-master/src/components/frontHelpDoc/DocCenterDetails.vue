<template>
   <div class="doc-detail">
       <div class="article-path-box">
           <span class="article-path">
               <span class="doc-center-title" @click="handDocCenter">文档中心</span>
               <span> ></span>
               {{detailsData.docTitle}}
           </span>
           <span class="article-time">
               发布时间:{{detailsData.updt}}
           </span>
       </div>
       <div class="article-conent">
           <span v-if="detailsData.docAttachment" >
               <i class="el-icon-edit" style="color: #66b1ff;"></i>
               <a  @click="exportData" style="color: #66b1ff;cursor: pointer">附件</a>
           </span>
           <h4>{{detailsData.docTitle}}</h4>


           <div class="images" v-viewer="{movable: false}">
               <p v-html="detailsData.docContent"></p>
               <!--<p v-html="detailsData.docContent"></p>-->
               <!--<p>-->
                   <!--<img src="../../assets/banner-new.png">-->
               <!--</p>-->
               <!--<img src="../../assets/banner-new.png">-->
           </div>


       </div>
   </div>


</template>

<script>
    import xyDocCenterDetailTree from './DocCenterDetailTree.vue';

    export default {

        name: 'docCenterDetail',
        data() {
            return {
                activeIndex: '1',
                activeIndex2: '1',
                docId:'',
                detailsData:{}
            }
        },
        created(){
            this.docId = this.$route.query.id;
            this.loadData();
            this.show();

        },
        components: {
            xyDocCenterDetailTree,
        },
        methods: {

            show () {
                const viewer = this.$el.querySelector('.images').$viewer
                viewer.show()
            },

            /**
             * @Type:
             * @Position:
             * @Description:文章详情数据
             */
            loadData(){
                this.$axios.get('/api/HelpDoc',{
                    params:{
                        helpDocId:this.docId
                    }
                }).then((res) =>{
                    if(res.data.result){
//                    debugger
                        this.detailsData = res.data.data;
//                      this.detailsData.docAttachment = "\\upload\\2018-11-14\\636778263548769742_1024.jpg";
                    }
                })
            },
          /**
           * @Type:
           * @Position:
           * @Description:附件下载
           */
            exportData() {
//                location.href="\\upload\\2018-11-14\\636778263548769742_1024.jpg";
                location.href = this.detailsData.docAttachment;
            },
            /**
             * @Type:
             * @Position:
             * @Description:跳回文档中心
             */
            handDocCenter(){
                this.$router.push({
                    path: '/docCenterSort'
                })
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
    p{
        line-height: 33px;
        text-indent: 2em;
    }
    .doc-detail{
        padding-top:40px
    }
    .doc-detail{
         .el-main{
            padding: 20px 0 20px 20px;
        }
        .article-path-box{
            /*background-color: #f2f2f2;*/
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
            background: #fff;
            padding: 20px;
            min-height: 600px;
        }
        .article-conent h4{
            text-align: center;
            height: 60px;
            line-height: 60px;
            font-size: 20px;
        }
        .doc-center-title{
            cursor: pointer;
        }
        .article-conent img{
            width: 500px;
        }
    }

</style>












































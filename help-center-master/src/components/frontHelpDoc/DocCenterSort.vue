<template>
    <div class="dcsBox">
        <el-row :gutter="20" v-for="item in docCateData">
            <h1 class="doc-title" v-if="item.subs">
                {{item.docTypeName}}
            </h1>
            <template v-if="item.subs">
             <el-col :span="8" v-for="(item,index) of  item.subs">
                <div class="grid-content">
                    <div class="header">
                    <span>
                        <!--<i class="iconfont icon-biaoti" style="color: #00c1de"></i>-->
                        <img :src="item.docTypeImg" />
                        {{item.docTypeName}}
                    </span>
                    <!--<span  v-if="item.isShowTxt" class="doc-more" @click="handDocSeeMore(item,index)">更多+</span>-->
                    <!--<span  v-else class="doc-more" @click="handDocDetailReceive(item,index)">收起-</span>-->
                        <span  class="doc-more" @click="handDocSeeMore(item,index)">查看更多+</span>
                    </div>
                    <div class="main">
                        <ul class="mian-ul">
                            <li v-for="item in item.docList" @click="handDocDetail(item)">
                            {{ item.docTitle }}
                            </li>
                        </ul>
                    </div>
                    <!--<p class="bottom" @click="handDocSeeMore">查看更多+</p>-->
                </div>

           </el-col>
            </template>
       </el-row>
    </div>
</template>



<script>
    export default {
        name: 'docCenterSort',
        data() {

            return {
                pageIndex:1,
                rowCount:10,
                docCateData:'',
            }
        },
        mounted(){
            this.docTreeData();
        },
        methods: {

            /**
             * @Type:
             * @Position:
             * @Description: 获取分类数据
             */
            docTreeData() {
                this.$axios.get('/api/DocTypeConfig/homepage')
                    .then((res) => {
//                        res.data.data.forEach(function(value,index){
//                          if(value.subs!=null){
//                              value.subs.map(el => {
//                                el.isShowTxt = true;//为了控制更多和收起新增一个字段
//                            });
//                          }
//                        });
                        this.docCateData = res.data.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },

            /**
             * @Type:
             * @Position:
             * @Description:查看更多
             */
            handDocSeeMore(item,index){
//              debugger
//                this.$axios.get('/api/HelpDoc/doctypeid'+"?docTypeId="+ item.docTypeId,{
//                    params:{
//                        pageIndex:1,
//                        rowCount:1000
//                    }
//                }).then((res)=>{
//                    if(res.data.result){
//                        item.docList = res.data.data.info;
//                        item.isShowTxt = false;
//                    }
//                })
                this.$router.push({
                    path: '/docCenterSeeMore',
                    query:{
                        docId:item.docTypeId
                    }
                })
            },
            /**
             * @Type:
             * @Position:
             * @Description:收到更多
             */
            handDocDetailReceive(item,index){
                item.docList = item.docList.slice(0,8)
                item.isShowTxt = true;
            },
            /**
             * @Type:
             * @Position:
             * @Description:查看文字详情
             */
            handDocDetail(row){
                this.$router.push({
                    path: '/docCenterDetail',
                    query:{
                        id:row.docId
                    }
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

<style>
    .dcsBox .el-aside, .el-main {
        overflow-y: auto;
        overflow: hidden;
        -webkit-box-sizing: border-box;
    }
    .dcsBox .doc-title{
        text-align: left;
        padding-left: 10px;
        font-size: 28px;
        height: 50px;
    }
    .dcsBox .doc-more{
        float: right;
        color: #00c1de;
        cursor: pointer;
    }

    .grid-content{
        position: relative;
        min-height: 300px;
        background: #fff;
        margin-bottom: 20px;
    }
    .grid-content .header{
        height: 50px;
        background: #fff;
        line-height: 50px;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
        padding-left: 15px;
        padding-right: 15px;
        font-size: 18px;
        font-weight: 600;
        /*border-bottom: 1px solid #f0f0f0;*/
    }
    .grid-content .header img{
        width: 16px;
        height: 16px;
    }
    .grid-content .mian-title{
        height: 35px;
        line-height: 35px;
        padding-left: 10px;
        font-weight: 600;
    }
    .grid-content .bottom{
        cursor: pointer;
        position: absolute;
        background-color: #e5f0ff;
        width: 100%;
        height: 50px;
        line-height: 50px;
        text-align: center;
        bottom: 0;
        color: #096ab5;
    }
    .grid-content .mian-ul{
        min-height: 155px;
        padding-left: 15px;
        padding-right: 15px;
        overflow: hidden;
        list-style-position:inside;
    }
    .grid-content .mian-ul li{
        cursor: pointer;
        height: 31px;
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;
    }


</style>








































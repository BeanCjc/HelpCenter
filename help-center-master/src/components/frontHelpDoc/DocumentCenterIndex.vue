<template>
    <div class="doc-center">
        <el-container>
            <div class="doc-mian-box">
            <el-header>
                <el-menu
                    :default-active="activeIndex2"
                    class="el-menu"
                    mode="horizontal"
                    @select="handleSelect"
                    background-color="#1c2327"
                    text-color="#fff"
                    active-text-color="#ffd04b"
                >
                    <el-menu-item index="1" @click="handDocCenter">文档中心</el-menu-item>

                    <el-menu-item index="2" @click="goManage" v-if="token">
                        后台管理
                    </el-menu-item>

                    <el-menu-item index="3" v-if="login" class="login-registered-box" >
                        <span class="cp" @click="handleLogin">登录</span>

                        <!--<span @click="handRegiste">注册</span>-->
                    </el-menu-item>
                    <el-menu-item
                        index="3"
                        v-else-if="hasLogin"
                        class="login-registered-box"
                        style="color:#fff !important;
                        border-bottom:none!important;
                        background-color: rgb(28, 35, 39);
                        cursor: inherit!important"
                    >

                        <span>{{username}}</span>,你好
                        <span @click="handleLoginOut" class="cp" style="color: #00c1de;margin-left: 15px;">退出</span>
                    </el-menu-item>

                </el-menu>
            </el-header>

            <el-container v-if="searchDoc" class="search-container">
                <div class="search-container-box">
                    <p class="search-title">文档中心</p>
                    <el-input class="search" size="large" clearable placeholder="请输入关键字进行搜索" v-model="keyword">
                    </el-input>
                    <el-button icon="el-icon-search" @click="docSearch"></el-button>
                </div>
            </el-container>

            <el-container class="el-container-conetnt">
                <el-main>

                    <router-view :key="$route.fullPath"></router-view>

                </el-main>
                <el-aside class="el-aside-ol" width="300px">
                    <div class="el-aside-box">
                        <p class="title">浏览最多</p>
                        <ul class="a">
                           <li v-for="item in mostVData" @click="handDocDetail(item)">
                               {{item.docTitle}}
                               <span class="span">
                                   <span>{{item.docCount}}</span>
                                   <img src="../../assets/up.png" width="20px"
                                        style="vertical-align: middle;margin-bottom: 4px;">
                               </span>
                           </li>
                        </ul>
                    </div>
                </el-aside>
            </el-container>
            </div>
            <el-footer>Copyright © 2017 轩亚科技提供技术支持 &nbsp;&nbsp;全国服务热线:400-6677370</el-footer>
        </el-container>
    </div>

</template>

<script>
    export default {
        name: 'docCenter',
        mounted(){
            this.loadDC();
            this.mostViewData();
            this.hasToken();
        },
        data() {
            return {
//                activeIndex: '1',
                activeIndex2: '1',
                login:true,
                hasLogin:false,
                name: '游客',
                currentPage: 1,
                pageSize: 10,
                mostVData:[],
                keyword:'',
                token:null,
                searchDoc:true

            }
        },
        computed:{
            username(){
                let username = JSON.parse(localStorage.getItem("userMsg")).username;
                return username ? username : this.name;
            },
            unreadNum(){
                return this.unread.length;
            }
        },
        methods: {


              /**
               * @Type:
               * @Description:是否存在token
               */
              hasToken(){
                this.token = localStorage.getItem('token');
              },
            /**
             * @Type:
             * @Position:
             * @Description:退出
             */
            handleLoginOut(){
                localStorage.removeItem('username');
                localStorage.removeItem('token');
                    this.$router.push('/login');
            },
                /**
                 * @Type:数据
                 * @Description:主页搜索，展示数据
                 */
                docSearch(){
                    this.$router.push({
                        path: '/docCenterSearchResults',
                        query:{
                            keyword:this.keyword
                        }
                    })
                },

            /**
             * @Type:数据
             * @Description:获取浏览最多
             */
            mostViewData(filter){
                let _this = this;
                let params = Object.assign({}, {
                    rowCount: _this.pageSize,
                    pageIndex: _this.currentPage
                }, filter);
                _this.$axios.get('api/HelpDoc/pagelist',{
                    params: params,
                }).then(res => {
                    let data = res.data.data

                    this.mostVData = data.info;
//                    debugger
                })
                    .catch(function (error) {
                        console.log(error);
                    });
                },
            /**
             * @Type:
             * @Description:判断是否登录
             */
              loadDC(){
                  if(localStorage.getItem('token')){
                    this.login = false;
                    this.hasLogin = true;

                  }
              },
            /**
             * @Type:
             * @Description:点击浏览更多显示文章详情
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
             * @Description:如果登录去后台管理
             */
            goManage(){
                if(localStorage.getItem('token')){
                    this.$router.push('/docList')
                }else{
                    this.$message('请先登录');
                    this.$router.push('/login');
                }

            },
            /**
             * @Type:
             * @Description:在文字详情点击文档中心返回文档分类
             */
            handDocCenter(){
                this.$router.push({
                    path: '/docCenterSort'
                })
            },
            /**
             * @Type:
             * @Description:去登录
             */
            handleLogin(){
                this.$router.push('/login');
            },
            /**
             * @Type:
             * @Description:去注册
             */
            handRegiste(){
                this.$router.push({
                    path: '/login',
                    query: {
                        flat: false
                    }
                })
            },
            /**
             * @Type:
             * @Position:顶部导航
             * @Description:
             */
            handleSelect(key, keyPath) {
                console.log(key, keyPath);
            }

        }
    }

</script>

<style lang="less">
    .doc-center{
        position: relative;
        height: 100%;
        overflow: auto;
        background: #f5f6f7;
        body{
            background:#f7f7f7;
        }
        .doc-mian-box{
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 60px;
            overflow: auto;
        }
        /*重写*/
        .el-input__inner{
            border-top-left-radius: 3px;
            border-top-right-radius: 0;
            border-bottom-right-radius:0;
            border-bottom-left-radius:3px;
        }
        .el-button--small, .el-button--small.is-round {
            padding: 7px 25px;
            margin-left: -7px;
        }
        .el-header{
            background-color:#1c2327;
            /*background-color: rgb(84, 92, 100);*/
            color: #333;
            text-align: center;
        }
        .el-footer {
            background-color:#1c2327;
            line-height: 60px;
            /*background-color: rgb(84, 92, 100);*/
            color: #fff;
            text-align: center;
            position: absolute;
            bottom: 0;
            left: 0;
            right: 0;
        }
        .el-container-conetnt{
            width: 1366px;
            margin: 0 auto;
        }
        /*.el-aside {*/
            /*cursor: pointer;*/
            /*color: #333;*/
            /*padding-top: 70px;*/
        /*}*/
        .el-main {
            color: #333;
        }
        .el-menu{
            width: 1200px;
            margin: 0 auto;
        }
        .el-menu.el-menu--horizontal {
            background-color:#000;
            border-bottom: none;
        }

        .search-container{
            /*width: 1200px;*/
            width: 100%;
            height: 200px;
            margin: 0 auto;
            background: url("../../../static/img/banner-help.png");
        }
        .search-container-box{
         margin: 0 auto;
            .search-title{
                height: 50px;
                line-height: 50px;
                text-align: center;
                font-size: 30px;
                color: #ffff;
            }
        }
        .search-container-box>.el-button--mini, .el-button--small {
            font-size: 20px;
            border-top-left-radius: 0;
            border-top-right-radius: 3px;
            border-bottom-right-radius: 3px;
            border-bottom-left-radius: 0;
            padding-bottom: 11px;
            background: #00c1de;
            border: 1px solid #00c1de;
            color: #fff;
        }
        .search-container-box>.el-button:active {
            color: #fff;
            border-color: #00c1de;
            outline: 0;
        }
        .search-container-box>.el-button:focus, .el-button:hover {
            color: #fff;
            border-color: #00c1de;
            background:rgba(0,193,222,0.9);
        }
        .search-container .search{
            width: 500px;
            height: 60px;
            line-height: 60px;
        }
        .el-aside-box{
            background: #fff;
            height: 100%;
        }
        .el-aside-ol{
            cursor: pointer;
            color: #333;
            padding-top: 70px;
        }
        .el-aside-ol .title{
            height: 50px;
            background: #f2f2f2;
            line-height: 50px;
            box-sizing: border-box;
            padding-left: 15px;
            padding-right: 15px;
            font-size: 18px;
        }
        .el-aside-ol ul{
            /*list-style-type:none;*/
            /*list-style-type:disc !important;*/
            /*counter-reset:sectioncounter;*/
            list-style-position:inside;
            padding: 10px;
        }
        .el-aside-ol ul li{
            position: relative;
            font-size: 14px;
            height: 25px;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
        }
        .el-aside-ol ul .span{
            position: absolute;
            right: 0;
        }
        /*.el-aside-ol ul li:before {*/
            /*content:counter(sectioncounter) "、";*/
            /*counter-increment:sectioncounter;*/
        /*}*/
        .login-registered-box{
            display: inline-block;
            float: right !important;
            line-height: 60px;
            color: #fff;
        }
        .cp{
            cursor: pointer;
        }
    }



</style>


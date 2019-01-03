<template>
    <div class="login-wrap">
        <div class="ms-login">
            <div class="ms-logo-box">
                <img src="../../assets/logo-new.png" class="ms-logo-img">
            </div>
            <div class="ms-title">
                <span class="ms-title-text">帮助中心</span>
                <!--<span>注册</span>-->
            </div>
            <!--<i class="iconfont icon-qiehuan" @click="switchover()"></i>-->
            <component v-bind:is="xyMain"></component>
        </div>
    </div>
</template>

<script>
    import xyLogin from './Login.vue'
    import xyRegister from './Register.vue'
    export default {
        mounted(){
            this.flat = this.$route.query.flat;
            if(this.flat==false){
                this.xyMain = xyRegister;
            }
        },
        components:{
            xyLogin,
            xyRegister
        },
        data: function(){
            return {
                xyMain:xyLogin,
                flat:true
            }
        },
        methods: {
            switchover(){
                this.flat = !this.flat;
                if (this.flat){
                    this.xyMain = xyLogin;
                }else {
                    this.xyMain = xyRegister;
                }
            }
        }
    }
</script>

<style scoped>
    .login-wrap{
        position: relative;
        width:100%;
        height:100%;
        background-image: url(../../assets/banner-new.png);
        background-size: 100%;
    }
    .ms-title{
        width:100%;
        line-height: 50px;
        text-align: center;
        font-size:20px;
        color: #000;
        /*border-bottom: 1px solid #ddd;*/
        position: relative;
    }
    .ms-title-text{
        font-size: 24px;
    }
    .iconfont{
        color: #ffa200;
        position: absolute;
        right: 15px;
        font-size: 26px;
        top: 125px;
        cursor: pointer;
        font-weight: 600;
    }
    .ms-login{
        position: absolute;
        left:70%;
        top:40%;
        width:400px;
        height: 400px;
        /*width:350px;*/
        margin:-190px 0 0 -175px;
        border-radius: 5px;
        background:#fff;
        /*background: rgba(255,255,255, 0.3);*/
        overflow: hidden;
    }
    .ms-logo-box{
        text-align: center;
        padding-top: 20px;
        padding-bottom: 10px;
        border-bottom: 1px solid #ddd
    }
    .ms-logo-img{
        width:110px;
    }
    .ms-content{
        padding: 30px 30px;
    }
    .login-btn{
        text-align: center;
    }
    .login-btn button{
        width:100%;
        height:36px;
        margin-bottom: 10px;
    }
    .login-tips{
        font-size:12px;
        line-height:30px;
        /*color:#fff;*/
    }
</style>

<template>
    <div>
        <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="0px" class="ms-content">
            <el-form-item prop="username">
                <el-input v-model="ruleForm.username" placeholder="请输入账号">
                    <el-button slot="prepend" icon="el-icon-lx-people"></el-button>
                </el-input>
            </el-form-item>
            <el-form-item prop="password">
                <el-input type="password" placeholder="请输入密码" v-model="ruleForm.password" @keyup.enter.native="submitForm('ruleForm')">
                    <el-button slot="prepend" icon="el-icon-lx-lock"></el-button>
                </el-input>
            </el-form-item>
            <div class="login-btn">
                <el-button type="primary" @click="submitForm('ruleForm')">登录</el-button>
                <el-button type="primary" @click="backLogin()">返回</el-button>
            </div>
            <p class="login-tips">Copyright © 2018轩亚科技提供技术支持</p>
        </el-form>
    </div>
</template>

<script>
//    import qs from 'qs';
    export default {
        data() {
            return {
                ruleForm: {
                    username: '',
                    password: '',
                    rememberMe: false
                },
                rules: {
                    user: [
                        { required: true, message: '请输入用户名', trigger: 'blur' }
                    ],
                    password: [
                        { required: true, message: '请输入密码', trigger: 'blur' }
                    ]
                }
            }
        },
        mounted() {

        },
        methods: {
            backLogin(){
                this.$router.push({
                    path: '/docCenterSort'
                })
            },
            submitForm(formName) {
                let _this = this;
                let param = {
                    'user': _this.ruleForm.username,
                    'password': _this.ruleForm.password
                };

                _this.$refs.ruleForm.validate((valid) => {
                    if (!valid) {
                        return false;
                    }
                    const loading = _this.$loading({
                        lock: true,
                        text: 'Loading',
                        spinner: 'el-icon-loading',
                        background: 'rgba(0, 0, 0, 0.7)'
                    });
                    //最多持续3秒
                    setTimeout(() => {
                        loading.close();
                    }, 3000);
                    _this.$axios.post('/api/Auth',param).then((res)=>{
//                      debugger
                       console.log(res);
//                       debugger
                         loading.close();
                            if (res.data.result=== true) {
                                let data = res.data;

                                var userMsg = {
                                  id :data.data.crUsrId,
                                  username: data.data.usrName,
                                }
                                _this.$store.commit('set_token',data.token);
                                _this.$store.commit('contextUser',userMsg);
                                localStorage.setItem("userMsg",JSON.stringify(userMsg));//用户信息
                                localStorage.setItem("token",data.token);//token
//                                console.log(this.$store.state)
                                if(_this.$store.state.token){
//                                    _this.$router.push('/departManage');
                                    _this.$router.push('/docCenterSort');
//                                    console.log(this.$store.state.token)
                                }else{
                                    _this.$router.replace('/login');
                                }
                                _this.$bus.$emit(_this.$env.LOGIN_SUCCESS, _this.ruleForm.username, res.data.data, _this.ruleForm.rememberMe);

//                                console.log(_this.$env.LOGIN_SUCCESS);
                                return;
                            }else if(res.data.result=== false){
                                _this.$message.error(res.data.tips);
                            }
                        }).catch(function (error) {
                        loading.close();
                        if (error.response) {
                            // 请求已发出，但服务器响应的状态码不在 2xx 范围内
                            _this.$alert("【" + error.response.status + "】请求出错了" + JSON.stringify(error.response.data));
                        } else {
                            _this.$alert("请求出错了：" + error.message);
                        }
                        return Promise.reject(error);
                    });
                });
            }
        }
    }
</script>

<style scoped>
    .ms-login{
        position: absolute;
        left:50%;
        top:50%;
        width:350px;
        margin:-190px 0 0 -175px;
        border-radius: 5px;
        background: rgba(255,255,255, 0.3);
        overflow: hidden;
    }
    .ms-content{
        padding: 20px 60px;
    }
    .login-btn{
        text-align: center;
    }
    .login-btn button{
        width:46%;
        margin-right: 1%;
        /*width:100%;*/
        height:36px;
        margin-bottom: 10px;
    }
    .login-tips{
        font-size: 12px;
        line-height: 30px;
        color: #ccc;
        text-align: center;
    }
</style>

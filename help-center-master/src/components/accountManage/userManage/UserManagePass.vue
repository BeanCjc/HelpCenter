<template>
    <el-form :model="umPassForm" status-icon :rules="rules2" ref="umPassForm" label-width="100px" class="demo-ruleForm">
    <el-form-item label="密码" prop="pass">
        <el-input type="password" v-model="umPassForm.pass" autocomplete="off"></el-input>
    </el-form-item>
    <el-form-item label="确认密码" prop="checkPass">
        <el-input type="password" v-model="umPassForm.checkPass" autocomplete="off"></el-input>
    </el-form-item>
</el-form>
</template>
<script>
    export default {
        props: {
            userManageForm:{}
        },
        data() {
            var validatePass = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('请输入密码'));
                } else {
                    if (this.umPassForm.checkPass !== '') {
                        this.$refs.umPassForm.validateField('checkPass');
                    }
                    callback();
                }
            };
            var validatePass2 = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('请再次输入密码'));
                } else if (value !== this.umPassForm.pass) {
                    callback(new Error('两次输入密码不一致!'));
                } else {
                    callback();
                }
            };
            return {
                umPassForm: {
                    pass: '',
                    checkPass: ''
                },
                rules2: {
                    pass: [
                        { validator: validatePass, trigger: 'blur' }
                    ],
                    checkPass: [
                        { validator: validatePass2, trigger: 'blur' }
                    ]
                }
            };
        },
        methods: {
            umPassSubmit: function () {
                let _this = this;
                console.log(this.userManageForm);
                _this.$axios.put('api/User/resetpsw/account'+"?usrAccount="+this.userManageForm.usrAccount+"&Password="+this.umPassForm.pass)
//                _this.$axios.put('api/User/resetpsw/account'+"?usrAccount="+this.userManageForm.usrAccount,this.umPassForm)
                    .then(res => {
                        let data = res.data
                        if (data.result == false) {
                            _this.$message.error(data.tips);
                        } else {
//                            _this.$message(data.tips); //添加成功
                            this.$message({
                                showClose: true,
                                message: data.tips,
                                type: 'success'
                            });
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                    });


            },
        }
    }
</script>






























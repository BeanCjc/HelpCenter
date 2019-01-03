<template>
    <el-form :model="ruleForm2" status-icon :rules="rules2" ref="ruleForm2" label-width="100px" class="demo-ruleForm">
    <el-form-item label="密码" prop="pass">
        <el-input type="password" v-model="ruleForm2.pass" autocomplete="off"></el-input>
    </el-form-item>
    <el-form-item label="确认密码" prop="checkPass">
        <el-input type="password" v-model="ruleForm2.checkPass" autocomplete="off"></el-input>
    </el-form-item>
</el-form>
</template>
<script>
    export default {
        props: {
            departPeopleForm:{}
        },
        data() {
            var validatePass = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('请输入密码'));
                } else {
                    if (this.ruleForm2.checkPass !== '') {
                        this.$refs.ruleForm2.validateField('checkPass');
                    }
                    callback();
                }
            };
            var validatePass2 = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('请再次输入密码'));
                } else if (value !== this.ruleForm2.pass) {
                    callback(new Error('两次输入密码不一致!'));
                } else {
                    callback();
                }
            };
            return {
                ruleForm2: {
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
            onPassSubmit: function () {
                let _this = this;
//                console.log(this.departPeopleForm);
                let param ={
                    Password:this.ruleForm2.pass

                }
//                _this.$axios.put('/api/User/resetpsw/account'+"?usrAccount="+this.departPeopleForm.usrAccount,param)
                _this.$axios.put('/api/User/resetpsw/account'+"?usrAccount="+this.departPeopleForm.usrAccount+"&Password="+this.ruleForm2.pass)
                    .then(res => {
                        let data = res.data
                        if (data.result == false) {
                            _this.$message.error(data.tips);
                        } else {
//                            _this.$message(data.tips); //修改成功
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






























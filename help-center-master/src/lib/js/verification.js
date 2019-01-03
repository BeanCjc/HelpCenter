import Vue from 'vue'
const validator =  {
    template(reg,msg,value,callback){
        if (reg.test(value)) {
            callback();
        } else {
            return callback(new Error(msg));
        }
    },
    isChinese(rule, value, callback){
        validator.template(/^[a-zA-Z\u4e00-\u9fa5]+$/,'必须为中文',value,callback);
    },
    isAlphaNumber(rule, value, callback){
        validator.template(/^[0-9a-zA_Z]+$/,'必须为数字或英文',value,callback);
    },
    Alpha(rule, value, callback){
        validator.template(/^[a-zA_Z]+$/,'必须英文',value,callback);
    },
    isIP(rule, value, callback){
        validator.template(/(\d+)\.(\d+)\.(\d+)\.(\d+)/g,'请输入正确的ip地址',value,callback);
    },
    isNumber(rule, value, callback){
        validator.template(/[^\d]/g,'只能输入数字',value,callback);
    },
    isPhone(rule, value, callback){
        validator.template(/^1[345678][0-9]{9}$/,'手机格式错误',value,callback);
    },
    isQQ(rule, value, callback){
        validator.template(/^[1-9]\d{4,9}$/,'QQ格式错误',value,callback);
    },
    isWX(rule, value, callback){
        validator.template(/^[a-z_\d]+$/,'微信格式错误',value,callback);
    },
    isPositiveNumber(rule, value, callback){
        validator.template(/^[0-9]\d*$/,'请输入合法数字',value,callback);
    },

};


export default validator

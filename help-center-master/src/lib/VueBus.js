/**
 * bus
 * @Author Canaan
 * @Date 2018/6/7.
 */
const install = function (Vue) {

    /**
     * 环境
     * @type {{}}
     */
    Vue.prototype.$env = {
        APP_INIT_COMPLETE: 'onAppInitComplete',      //项目初始化完成【事件】
        LOGIN_SUCCESS: 'onLoginSuccess',            //登录成功【事件】
        LOGIN_OUT_SUCCESS: 'onLoginOutSuccess',     //登出【事件】
        TOKEN_INVALID: 'onTokenInvalid',            //token 失败 【事件】
        COLLAPSE: 'onCollapse',                       //侧边折叠
        TAGS: 'onTags',                              //设置标签

        LOGIN_TOKEN: 'signToken',                   //签名
        LOGIN_USER: 'loginUsername',                //登录用户名
    };

    Vue.prototype.$bus = new Vue({
        method: {
            emit(event, ...args) {
                this.$emit(event, ...args);
            },
            on(event, callback) {
                this.$on(event, callback);
            },
            off(event, callback) {
                this.$off(event, callback);
            }
        }
    });

};

export default install;


/**
 * 应用初始化
 * @param Vue
 * @param instance vue的实例
 * @Author Canaan
 * @Date 2018/6/28.
 */

const initial = function (Vue, instance) {
    //清理进度条
    instance.$store.commit('appInitCompleteFlag', false);
    instance.$store.commit('clearAppInitProgress');

    //初始化功能
    initAuth(Vue,instance);
    //初始化字典
    initDic(Vue, instance);
    //初始化用户
    initUser(Vue, instance);

    // 最多延时4秒，不管数据是否加载完成，都强制触发完成
    setTimeout(function () {
        if (!instance.$store.state.appInitCompleteFlag) {
            instance.$bus.$emit(instance.$env.APP_INIT_COMPLETE);
            instance.$store.commit('appInitCompleteFlag');
        }
    }, 4000);

};

/**
 * 初始化字典
 */
const initDic = function (Vue, instance) {
    instance.$ajax("/dictionary/static/all")
        .then(res => {
            if (res.code !== "0") {
                instance.$alert(res.msg);
                return;
            }

            instance.$store.commit('dict', res.data);

            //加载全局的filter
            for (let arr in res.data) {
                Vue.filter(arr, function (value) {
                    let dic = res.data[arr];
                    if (dic) {
                        return dic[value];
                    }
                    return '没有该字典';
                });
            }

            instance.$store.commit('appInitProgress');
        });
};

/**
 * 初始化用户
 */
const initUser = function (Vue, instance) {
    instance.$ajax("auth/user/currentUser").then(res => {
        if (res.code !== "0") {
            instance.$alert(res.msg);
            return;
        }

        instance.$store.commit('contextUser', res.data);
        instance.$store.commit('appInitProgress');
    });
};

/**
 * 初始化权限
 * @param Vue
 * @param instance
 */
const initAuth = function (Vue, instance) {

    instance.$ajax("auth/currentUnit/nav").then(res => {
        if (res.code !== "0") {
            instance.$alert(res.msg);
            return;
        }
        instance.$store.commit('navMenu', res.data);
        instance.$store.commit('appInitProgress');
    });

};

export default initial;

/**
 * 基于Vuex 的全局仓库
 * @Author Canaan
 * @Date 2018/6/28.
 */
import Vuex from 'vuex'
import Vue from 'vue';
import actions from './Actions'
import mutations from './Mutations'
import getters from './Getters'


Vue.use(Vuex);

const state = {
    /*系统用户*/
        currentUser:null,
        isLogin:false,
        token:"",
        contextUser:{
            id: undefined,       //主键
            username: undefined, //账号
            // nickname: undefined, //呢称
            // role: undefined,     //角色
            // relation: undefined,  //关系
        },
        tagIndex:''
    // ,
    // storeData:[],
    // /*导航栏菜单*/
    // navMenu: [],
    // /*系统字典*/
    // dict: {},
    // /*项目是否初始化完成，true 完成，false未完成*/
    // appInitCompleteFlag: false,
    // /*项目初始进度*/
    // appInitProgress: 0,
    // /*进度数最大值*/
    // appInitMaxProgress: 3,
};






export default new Vuex.Store({
    state,
    actions,
    mutations,
    getters
});

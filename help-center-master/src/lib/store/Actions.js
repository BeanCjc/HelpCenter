/**
 * @param actions
 */
import Vuex from 'vuex'
import Vue from 'vue';

Vue.use(Vuex);
const actions ={

    "SET_MSGTAG": function(state, tagIndex) {
        console.log('获取', state.tagIndex)
        store.commit("SET_MSGTAG", tagIndex)
    },

    /**
     * 门店选择器数据接口
     */
        /*获取Url*/
        getUrl(context,{vm:vm}){
            let url='';
            console.log(vm.$store.state.contextUser.role)
            switch (vm.$store.state.contextUser.role) {
                case "BLOC":
                    url=  "rent/blocTree";break;
                case "BRAND":
                    url=  "rent/brandTree";break;
                case "AREA":
                    url=  "rent/areaTree";break;
                case "ADMIN":
                    url=  "rent/brandTree";break;
                case "AGENT":
                    url=  "rent/brandTree";break;
                default :
                    url  =  undefined;break;
            }
        },
        /*获取第二层数据*/
        // getData(context,{vm: vm, Url: Url, data: data}) {
        //     let arr = [];
        //     data.forEach((item) => {
        //         //请求接口
        //         vm.$ajax.get(Url, {
        //             params: {
        //                 id: item.id,
        //                 filterStore: false
        //             }
        //         }).then((res) => {
        //             arr = [];
        //             if (res.code !== '0') {
        //                 vm.$message.info(res.msg);
        //             } else {
        //                 if (res.data.length !== 0) {
        //                     arr = res.data;
        //                     arr.forEach(item => {
        //                         if (item.type !== 'STORE') {
        //                             item.child = null
        //                         }
        //                     });
        //                     return context.commit('setStoreData', arr)
        //                 }
        //
        //             }
        //         })
        //     });
        //
        // },
        // getStoreData(context, {vm: vm, Url: Url}) {
        //     let data = [];
        //     vm.$ajax.get(Url,{
        //         params: {
        //             lazy: true,
        //             filterStore: false
        //         }
        //     }).then((res) => {
        //         res.data.forEach(item => {
        //             let obj = {id: '', name: '', key: ''};
        //             obj.id = item.id;
        //             obj.name = item.name;
        //             obj.key = item.key;
        //             if (item.type !== "STORE") {
        //                 obj.child = null
        //             }
        //             data.push(obj);
        //         });
        //         context.commit('setStoreData', data);
        //         //查看登录是否为品牌，区域，为则去掉第一层
        //         if (context.state.contextUser.role === 'BRAND' || context.state.contextUser.role === 'AREA') {
        //             context.dispatch('getData', {vm: vm, Url: Url, data: data})
        //         }
        //     })
        // }
    };
export default actions;

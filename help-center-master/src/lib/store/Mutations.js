/**
 * @param mutations
 */
const mutations = {

    "SET_MSGTAG": function(state, tagIndex) {
        state.tagIndex = tagIndex
        console.log('保存', state.tagIndex)
    },

    set_token(state,token){
        state.token = token;
        // localStorage.token ='bearer'+' '+token;
        localStorage.token =token;
    },
    del_token(state){
        state.token = '';
        localStorage.removeItem('token')
    },
    contextUser: function (state, user) {
        state.contextUser = Object.assign({}, state.contextUser, user);
    },



    navMenu: function (state, nav) {
        state.navMenu = nav;
    },
    appInitCompleteFlag: function (store, val) {
        if (val !== undefined || val !== null) {
            store.appInitCompleteFlag = val;
        } else {
            store.appInitCompleteFlag = true;
        }
    },
    appInitProgress: function (store) {
        store.appInitProgress = store.appInitProgress + 1;
        if (store.appInitProgress === store.appInitMaxProgress) {
            store.appInitCompleteFlag = true;
        }
    },
    clearAppInitProgress: function (store) {
        store.appInitProgress = 0;
    },

    dict: function (store, val) {
        store.dict = val;
    },
    dictItem: function (store, key, val) {
        store.dict[key] = val;
    },
    setStoreData:function (state,data) {
        state.storeData = data;
    }
};
export default mutations;

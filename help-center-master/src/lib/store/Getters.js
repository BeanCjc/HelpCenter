/**
 * @param getters
 */
const getters = {

    "SET_MSGTAG": function(state) {
        console.log('获取', state.tagIndex)
        return state.tagIndex
    },

    /**
     * 当前用户所属品牌
     * @param state
     * @param getters
     */
    currentBrand: (state, getters) => {
        switch (state.contextUser.role) {
            case "BRAND":
            case 'AREA':
            case 'STORE':
                return state.contextUser.brand;
        }
        return {};
    },
    //当前用户关系
    currentRelation: (state, getters) => {
        switch (state.contextUser.role) {
            case "BRAND":
            case 'BLOC':
            case 'AREA':
                return state.contextUser.relation;
        }
        return {};
    }
};
export default getters;

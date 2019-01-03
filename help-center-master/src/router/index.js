import Vue from 'vue';
import Router from 'vue-router';
import store from '../lib/store/Store';



Vue.use(Router);

const routes =  [
    {
        path: '/',
        redirect: '/docCenterSort'
        // redirect: '/Home'
    },
    {
        path: '/docCenter',
        component: resolve => require(['../components/frontHelpDoc/DocumentCenterIndex.vue'], resolve),
        children:[
            {
                path: '/docCenterSort',
                component: resolve => require(['../components/frontHelpDoc/DocCenterSort.vue'], resolve),
                meta: { title: '文档分类' }
            },
            {
                path: '/docCenterDetail',
                component: resolve => require(['../components/frontHelpDoc/DocCenterDetails.vue'], resolve),
                meta: { title: '文档详情' }
            },
            {
                path: '/docCenterSeeMore',
                component: resolve => require(['../components/frontHelpDoc/DocCenterSeeMore.vue'], resolve),
                meta: { title: '查看更多' }
            },
            {
                path: '/docCenterSearchResults',
                component: resolve => require(['../components/frontHelpDoc/DocCenterSearchResults.vue'], resolve),
                meta: { title: '搜索结果' }
            }
        ]
    },
    {
        path: '/Home',
        component: resolve => require(['../components/common/Home.vue'], resolve),
        redirect: '/docCenterSort',
        meta: { title: '自述文件',requireAuth:true},
        children:[
            {
                path: '/roleMange',
                component: resolve => require(['../components/accountManage/roleManage/RoleManagementIndex.vue'], resolve),
                meta: { title: '角色管理',requireAuth:true }
            },
            {
                path: '/RoleAuthorization',
                component: resolve => require(['../components/accountManage/roleManage/Authorization.vue'], resolve),
                meta: { title: '角色授权',requireAuth:true }
            },

            {
                path: '/userMange',
                component: resolve => require(['../components/accountManage/userManage/UserManagementIndex.vue'], resolve),
                meta: { title: '用户管理' ,requireAuth:true}
            },
            {
                path: '/departManage',
                component: resolve => require(['../components/departManage/DepartManageIndex.vue'], resolve),
                meta: { title: '部门管理',requireAuth:true }
            },
            {
                path: '/departStaff',
                component: resolve => require(['../components/departManage/departPeople/DepartStaffManageIndex.vue'], resolve),
                meta: { title: '部门人员管理',requireAuth:true }
            },
            {
                path: '/sortSetting',
                component: resolve => require(['../components/docManage/helpDocSetting/DocumentSettingsIndex.vue'], resolve),
                meta: { title: '分类设置',requireAuth:true }
            },
            {
                path: '/docList',
                component: resolve => require(['../components/docManage/helpDocList/DocumentListIndex.vue'], resolve),
                meta: { title: '文档列表',requireAuth:true}
            },
            {
                path: '/docListOperateAdd',
                name: 'DocListOperateAdd',
                component: resolve => require(['../components/docManage/helpDocList/DocListOperateAdd.vue'], resolve),
                meta: { title: '新增' ,requireAuth:true}
            },
            {
                path: '/docListOperateModify',
                name: 'DocListOperateModify',
                component: resolve => require(['../components/docManage/helpDocList/DocListOperateModify.vue'], resolve),
                meta: { title: '修改' ,requireAuth:true}
            },
            {
                path: '/docListOperateView',
                name: 'DocListOperateView',
                component: resolve => require(['../components/docManage/helpDocList/DocListOperateView.vue'], resolve),
                meta: { title: '查看' ,requireAuth:true}
            },
            {
                path: '/404',
                component: resolve => require(['../components/page/404.vue'], resolve),
                meta: { title: '404' }
            },
            {
                path: '/403',
                component: resolve => require(['../components/page/403.vue'], resolve),
                meta: { title: '403' }
            }
        ]
    },

    {
        path: '/login',
        component: resolve => require(['../components/login/index.vue'], resolve)
    },
    {
        path: '*',
        redirect: '/404'
    }
]

// 页面刷新时，重新赋值token
if(localStorage.getItem('token')){
    store.commit('set_token',localStorage.getItem('token'))
}

const router = new Router({
    routes
});

router.beforeEach((to, from, next) => {
    // debugger
    if (to.matched.some(r => r.meta.requireAuth)) {
        if (store.state.token) {
            next();
        }
        else {
            next({
                path: '/login',
                query: {redirect: to.fullPath}
            })
        }
    }
    else {
        next();
    }
})

export default router;

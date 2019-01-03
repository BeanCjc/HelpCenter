import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);



export default new Router({
    routes: [
       {
            path: '/',
            redirect: '/login'
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
                }
            ]
        },
        {
            path: '/Home',
            component: resolve => require(['../components/common/Home.vue'], resolve),
            meta: { title: '自述文件',requireAuth:true},
            children:[
                {
                    path: '/roleMange',
                    component: resolve => require(['../components/accountManage/roleManage/RoleManagementIndex.vue'], resolve),
                    meta: { title: '角色管理',requireAuth:true }
                },

                {
                    path: '/userMange',
                    component: resolve => require(['../components/accountManage/userManage/UserManagementIndex.vue'], resolve),
                    meta: { title: '用户管理' ,requireAuth:true}
                },

                {
                    path: '/departStaff',
                    component: resolve => require(['../components/departManage/departPeople/DepartStaffManageIndex.vue'], resolve),
                    meta: { title: '部门管理',requireAuth:true }
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
                    path: '/docListOperate',
                    component: resolve => require(['../components/docManage/helpDocList/DocListOperateAdd.vue'], resolve),
                    meta: { title: '文档列表详情' ,requireAuth:true}
                },




                {
                    path: '/dashboard',
                    component: resolve => require(['../components/page/Dashboard.vue'], resolve),
                    meta: { title: '系统首页' }
                },
                {
                    path: '/icon',
                    component: resolve => require(['../components/page/Icon.vue'], resolve),
                    meta: { title: '自定义图标' }
                },
                {
                    path: '/table',
                    component: resolve => require(['../components/page/BaseTable.vue'], resolve),
                    meta: { title: '基础表格' }
                },
                {
                    path: '/tabs',
                    component: resolve => require(['../components/page/Tabs.vue'], resolve),
                    meta: { title: 'tab选项卡' }
                },
                {
                    path: '/form',
                    component: resolve => require(['../components/page/BaseForm.vue'], resolve),
                    meta: { title: '基本表单' }
                },
                {
                    // 富文本编辑器组件
                    path: '/editor',
                    component: resolve => require(['../components/page/VueEditor.vue'], resolve),
                    meta: { title: '富文本编辑器' }
                },
                {
                    // markdown组件
                    path: '/markdown',
                    component: resolve => require(['../components/page/Markdown.vue'], resolve),
                    meta: { title: 'markdown编辑器' }
                },
                {
                    // 图片上传组件
                    path: '/upload',
                    component: resolve => require(['../components/page/Upload.vue'], resolve),
                    meta: { title: '文件上传' }
                },
                {
                    // vue-schart组件
                    path: '/charts',
                    component: resolve => require(['../components/page/BaseCharts.vue'], resolve),
                    meta: { title: 'schart图表' }
                },
                {
                    // 拖拽列表组件
                    path: '/drag',
                    component: resolve => require(['../components/page/DragList.vue'], resolve),
                    meta: { title: '拖拽列表' }
                },
                {
                    // 权限页面
                    path: '/permission',
                    component: resolve => require(['../components/page/Permission.vue'], resolve),
                    meta: { title: '权限测试', permission: true }
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
})




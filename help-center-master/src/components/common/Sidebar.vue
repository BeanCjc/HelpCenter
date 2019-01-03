<template>
    <div class="sidebar">
        <!--<el-menu class="sidebar-el-menu" :default-active="onRoutes" :collapse="collapse" background-color="#324157"-->
            <!--text-color="#bfcbd9" active-text-color="#20a0ff" unique-opened router>-->
            <!--<template v-for="item in items">-->
                <!--<template v-if="item.subs">-->
                    <!--<el-submenu :index="item.index" :key="item.index">-->
                        <!--<template slot="title">-->
                            <!--<i :class="item.icon"></i><span slot="title">{{ item.title }}</span>-->
                        <!--</template>-->
                        <!--<template v-for="subItem in item.subs">-->
                            <!--<el-submenu v-if="subItem.subs" :index="subItem.index" :key="subItem.index">-->
                                <!--<template slot="title">{{ subItem.title }}</template>-->
                                <!--<el-menu-item v-for="(threeItem,i) in subItem.subs" :key="i" :index="threeItem.index">-->
                                    <!--{{ threeItem.title }}-->
                                <!--</el-menu-item>-->
                            <!--</el-submenu>-->
                            <!--<el-menu-item v-else :index="subItem.index" :key="subItem.index">-->
                                <!--{{ subItem.title }}-->
                            <!--</el-menu-item>-->
                        <!--</template>-->
                    <!--</el-submenu>-->
                <!--</template>-->
                <!--<template v-else>-->
                    <!--<el-menu-item :index="item.index" :key="item.index">-->
                        <!--<i :class="item.icon"></i><span slot="title">{{ item.title }}</span>-->
                    <!--</el-menu-item>-->
                <!--</template>-->
            <!--</template>-->
        <!--</el-menu>-->


<el-menu class="sidebar-el-menu" :default-active="onRoutes" :collapse="collapse" background-color="#324157"
         text-color="#bfcbd9" active-text-color="#20a0ff" unique-opened router>
    <template v-for="item in items">
        <template v-if="item.subs">
            <!--{{ item.moduleName }}-->
            <el-submenu :index="item.moduleCode" :key="item.moduleCode">

                <!--<template slot="moduleName">-->
                    <!--&lt;!&ndash;{{ item.moduleName }}&ndash;&gt;-->
                    <!--<i :class="item.moduleImgPath"></i><span slot="moduleName">{{ item.moduleName }}</span>-->
                <!--</template>-->

                <template slot="title">
                    <i :class="item.moduleImgPath"></i>
                    <span>{{ item.moduleName }}</span>
                </template>

                <template v-for="subItem in item.subs">
                    <el-submenu v-if="subItem.subs" :index="subItem.moduleCode" :key="subItem.moduleCode">
                        <template slot="moduleName">{{ subItem.moduleName }}</template>
                        <el-menu-item v-for="(threeItem,i) in subItem.subs" :key="i" :index="threeItem.moduleCode">
                            {{ threeItem.moduleName }}
                        </el-menu-item>
                    </el-submenu>
                    <el-menu-item v-else :index="subItem.moduleCode" :key="subItem.moduleCode">
                        {{ subItem.moduleName }}
                    </el-menu-item>
                </template>

            </el-submenu>
        </template>

        <template v-else>
            <el-menu-item :index="item.moduleCode" :key="item.moduleCode">
                <i :class="item.moduleImgPath"></i>
                <!--<span slot="moduleName">-->
                    <!--{{ item.moduleName }}-->
                <!--</span>-->
                <span slot="title">
                    {{ item.moduleName }}
                </span>
            </el-menu-item>
        </template>
    </template>
</el-menu>


    </div>
</template>

<script>
    import bus from '../common/bus';
    export default {
        mounted() {
            this.itemData();
        },
        data() {
            return {
                collapse: false,
                items:[
                ]


//                items: [
//
//                    {
//                        "moduleId": "14c470fa-11ad-492b-aca5-ef0946bb84b5",
//                        "moduleName": "文档管理",
//                        "preModuleId": null,
//                        "moduleRemark": null,
//                        "moduleState": 1,
//                        "crdt": null,
//                        "crUsrId": null,
//                        "updt": null,
//                        "upUsrId": null,
//                        "moduleImgPath": "el-icon-edit-outline",
//                        "moduleUrlPath": null,
//                        "moduleCode": "docManage",
//                        "subs": [
//                            {
//                                "moduleId": "91ed3071-cb5a-4a05-ab15-bf23b023849d",
//                                "moduleName": "文档列表",
//                                "preModuleId": "14c470fa-11ad-492b-aca5-ef0946bb84b5",
//                                "moduleRemark": null,
//                                "moduleState": 1,
//                                "crdt": null,
//                                "crUsrId": null,
//                                "updt": null,
//                                "upUsrId": null,
//                                "moduleImgPath": null,
//                                "moduleUrlPath": null,
//                                "moduleCode": "docList",
//                                "subs": null
//                            },
//                            {
//                                "moduleId": "b10b760d-f96e-4233-85ac-16fd7e56ec3d",
//                                "moduleName": "分类设置",
//                                "preModuleId": "14c470fa-11ad-492b-aca5-ef0946bb84b5",
//                                "moduleRemark": null,
//                                "moduleState": 1,
//                                "crdt": null,
//                                "crUsrId": null,
//                                "updt": null,
//                                "upUsrId": null,
//                                "moduleImgPath": null,
//                                "moduleUrlPath": null,
//                                "moduleCode": "sortSetting",
//                                "subs": null
//                            }
//                        ]
//                    },
//                    {
//                        "moduleId": "83bd6bcf-64ec-490a-a46c-34987bfb50c1",
//                        "moduleName": "账户管理",
//                        "preModuleId": null,
//                        "moduleRemark": null,
//                        "moduleState": 1,
//                        "crdt": null,
//                        "crUsrId": null,
//                        "updt": null,
//                        "upUsrId": null,
//                        "moduleImgPath": "el-icon-lx-calendar",
//                        "moduleUrlPath": null,
//                        "moduleCode": "3",
//                        "subs": [
//                            {
//                                "moduleId": "90e86163-ea78-4bc3-8132-4e0dc02f710a",
//                                "moduleName": "用户管理",
//                                "preModuleId": "83bd6bcf-64ec-490a-a46c-34987bfb50c1",
//                                "moduleRemark": null,
//                                "moduleState": 1,
//                                "crdt": null,
//                                "crUsrId": null,
//                                "updt": null,
//                                "upUsrId": null,
//                                "moduleImgPath": null,
//                                "moduleUrlPath": null,
//                                "moduleCode": "userMange",
//                                "subs": null
//                            }
//                        ]
//                    }
//
//                ]

//                items: [
//                    {
//                        icon: 'el-icon-lx-home',
//                        index: 'Home',
//                        title: '首页'
//                    },
//                    {
//                        icon: 'el-icon-lx-home',
//                        index: 'departManage',
//                        title: '部门管理',
//                        subs: [
//                            {
//                                index: 'departManage',
//                                title: '部门管理'
//                            },
//                            {
//                                index: 'departStaff',
//                                title: '部门人员管理'
//                            }
//                        ]
//                    },
//                    {
//                        icon: 'el-icon-lx-calendar',
//                        index: '3',
//                        title: '账户管理',
//                        subs: [
//                            {
//                                index: 'userMange',
//                                title: '用户管理'
//                            },
//                            {
//                                index: 'roleMange',
//                                title: '角色管理'
//                            }
//                        ]
//                    },
//                    {
//                        icon: 'el-icon-lx-home',
//                        index: 'docManage',
//                        title: '文档管理',
//                        subs: [
//                            {
//                                index: 'sortSetting',
//                                title: '分类设置'
//                            },
//                            {
//                                index: 'docList',
//                                title: '文档列表'
//                            }
//                        ]
//                    }
//                ]
            }
        },
        computed:{
            onRoutes(){
                return this.$route.path.replace('/','');
            }
        },

        methods: {
            /**
             * @Type:数据
             * @Description:初始化表格
             */
            itemData() {
               this.$axios.get('api/RoleSysModule/userid').then(res => {
                   if(res.data.data.length>1){
                       this.items = res.data.data;
//                       [this.items[0],this.items[2]] = [this.items[2],this.items[0]];
//                       [this.items[1],this.items[3]] = [this.items[3],this.items[1]];
                   }else {
                       this.items = res.data.data;
                   }
                })
                .catch(function (error) {
                    console.log(error);
                });
            },

        },
        created(){
            // 通过 Event Bus 进行组件间通信，来折叠侧边栏
            bus.$on('collapse', msg => {
                this.collapse = msg;
            })
        }
    }
</script>

<style scoped>
    .sidebar{
        display: block;
        position: absolute;
        left: 0;
        top: 70px;
        bottom:0;
        overflow-y: scroll;
    }
    .sidebar::-webkit-scrollbar{
        width: 0;
    }
    .sidebar-el-menu:not(.el-menu--collapse){
        width: 250px;
    }
    .sidebar > ul {
        height:100%;
    }
</style>

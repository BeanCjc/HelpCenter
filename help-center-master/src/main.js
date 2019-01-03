import Vue from 'vue';
import Viewer from 'v-viewer'

import App from './App';
import router from './router';
// import axios from 'axios';
import ElementUI from 'element-ui';
import 'viewerjs/dist/viewer.css'
import 'element-ui/lib/theme-chalk/index.css';    // 默认主题
// import '../static/css/theme-green/index.css';       // 浅绿色主题
import '../static/Iconfont/iconfont.css';
import '../static/css/icon.css';
import "babel-polyfill";
import axios from './lib/http'
import store from './lib/store/Store';
import VueBus from './lib/VueBus';

import pattern from './lib/js/verification'


Vue.use(Viewer);
Viewer.setDefaults({
    button: false,
    navbar: false,
    title: false,
    toolbar: true,
    tooltip: false,
    movable: false,
    zoomable: true,
    rotatable: false,
    scalable: false,
    transition: false,
    fullscreen: false,
    keyboard: true,
    zIndexInline: 2017


})
// Vue.use(Viewer, {
//     defaultOptions: {
//         zIndex: 9999
//     }
// })
Vue.use(VueBus);
Vue.use(ElementUI, { size: 'small' });
Vue.prototype.$axios = axios;

Vue.prototype.pattern = pattern;

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app');

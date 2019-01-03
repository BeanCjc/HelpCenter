
import axios from 'axios'
// import store from './store/store'
import router from '../router/index'

// axios 配置
// axios.defaults.timeout = 5000
// axios.defaults.baseURL = 'https://api.github.com'

// 添加请求拦截器 request 拦截器
axios.interceptors.request.use(
  config => {
      // 在发送请求之前做些什么
      // 判断是否存在token,如果存在将每个页面header都添加token
      // debugger
      let token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `bearer ${token}`
      // config.headers.Authorization = `token ${store.state.token}`
    }else{
          // alert('请先登录');
          // alert('token不存在');
    }
    return config
  },
  err => {
      // 对请求错误做些什么
    return Promise.reject(err)
  },
)

// http response 拦截器
axios.interceptors.response.use(
  response => {
    return response
  },
  error => {
    if (error.response) {
        console.log(error.response);
        if(error.response.status===401){
            /*401 清除token信息并跳转到登录页面*/
            localStorage.removeItem("token");
              /*    this.$store.commit('del_token');*/
               /* 只有在当前路由不是登录页面才跳转*/
            // router.currentRoute.path !== 'login' &&
            //   router.replace({
            //     path: 'login',
            //     query: { redirect: router.currentRoute.path },
            //   })

            router.replace({
                path: 'login',
                query: { redirect: router.currentRoute.path },
              })
        }
      // switch (error.response.status) {
      //   case 401:
      //     // 401 清除token信息并跳转到登录页面
      //       store.commit('del_token');
      //       // this.$store.commit('del_token');
      //     // 只有在当前路由不是登录页面才跳转
      //     router.currentRoute.path !== 'login' &&
      //       router.replace({
      //         path: 'login',
      //         query: { redirect: router.currentRoute.path },
      //       })
      // }
    }
    // console.log(JSON.stringify(error));//console : Error: Request failed with status code 402
    return Promise.reject(error.response.data)
  },
)

export default axios

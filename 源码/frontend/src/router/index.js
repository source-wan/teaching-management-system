import Vue from 'vue';
import VueRouter from 'vue-router';
import routes from './routes';
const originalPush = VueRouter.prototype.push

VueRouter.prototype.push = function push(location) {
  return originalPush.call(this, location).catch(err => err)
}

Vue.use(VueRouter);

let router = new VueRouter({
    mode: 'history',
    routes
})

router.beforeEach((to,from,next) => {
  const isLogin= localStorage.accessToken ? true:false;
  // const hasCookie=document.cookie
  if(to.path=="/login"){
    next();
  }else{
    isLogin? next(): next("/login");
  }
})

export default router;
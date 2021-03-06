import 'babel-polyfill'
import Vue from 'vue'
import VueRouter from 'vue-router'
import Vuetify from 'vuetify'

import store from './store'

import './stylus/main.styl'
import App from './components/app.vue'
import Home from './components/home.vue'
import Counter from './components/counter.vue'
import Values from './components/values.vue'

Vue.use(VueRouter)
Vue.use(Vuetify)

const routes = [
  { path: '/', component: Home },
  { path: '/counter', component: Counter },
  { path: '/values', component: Values }
]

const router = new VueRouter({
  mode: 'history',
  routes
})

// eslint-disable-next-line no-new
new Vue({
  el: '#app',
  store,
  router,
  render: h => h(App)
})

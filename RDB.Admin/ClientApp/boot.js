﻿import './css/site.css'

import 'babel-polyfill'
import Vue from 'vue'
import VueRouter from 'vue-router'

import App from './components/app.vue'
import Home from './components/home.vue'
import Counter from './components/counter.vue'
import Values from './components/values.vue'

Vue.use(VueRouter)

const routes = [
  { path: '/', component: Home },
  { path: '/counter', component: Counter },
  { path: '/values', component: Values }
]

const router = new VueRouter({
  mode: 'history',
  routes: routes
})

new Vue({
  el: '#app',
  router,
  render: h => h(App)
})
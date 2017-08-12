import Vue from 'vue'
import Vuex from 'vuex'
import counter from './counter'
import values from './values'

Vue.use(Vuex)

const store = new Vuex.Store({
  modules: {
    counter,
    values
  }
})

if (module.hot) {
  module.hot.accept([
    './counter',
    './values'
  ], () => {
    store.hotUpdate({
      modules: {
        counter: require('./counter').default,
        values: require('./values').default
      }
    })
  })
}

export default store

import Vue from 'vue'
import Vuex from 'vuex'
import counter from './counter'

Vue.use(Vuex)

const store = new Vuex.Store({
  modules: {
    counter
  }
})

if (module.hot) {
  module.hot.accept(['./counter'], () => {
    store.hotUpdate({
      modules: {
        counter: require('./counter').default
      }
    })
  })
}

export default store

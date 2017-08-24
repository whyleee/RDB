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
        /* eslint-disable global-require */
        counter: require('./counter').default,
        values: require('./values').default
        /* eslint-enable */
      }
    })
  })
}

export default store

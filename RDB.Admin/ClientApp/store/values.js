import api from '../lib/api'

export default {
  namespaced: true,
  state: {
    values: null
  },
  mutations: {
    setValues(state, values) {
      state.values = values
    },
    addValue(state, value) {
      state.values.push(value)
    },
    updateText(state, { value, text }) {
      value.text = text
    }
  },
  actions: {
    async loadValues({ commit }) {
      let response = await api.values.get()
      commit('setValues', response.data)
    },
    async addValue({ commit }, text) {
      let response = await api.values.post(text)
      commit('addValue', response.data)
    },
    async updateText({ commit }, { value, text }) {
      let response = await api.values.put(value.id, text)
      commit('updateText', { value, text })
    }
  }
}

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
    },
    deleteValue(state, id) {
      let i = state.values.findIndex(val => val.id == id)
      if (~i) {
        state.values.splice(i, 1)
      }
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
    },
    async deleteValue({ commit }, id) {
      let response = await api.values.delete(id)
      commit('deleteValue', id)
    }
  }
}

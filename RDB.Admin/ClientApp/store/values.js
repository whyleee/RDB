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
      const i = state.values.findIndex(val => val.id == id)
      if (~i) {
        state.values.splice(i, 1)
      }
    }
  },
  actions: {
    async loadValues({ commit }) {
      const values = await api.valuesGet()
      commit('setValues', values)
    },
    async addValue({ commit }, text) {
      const value = await api.valuesPost({ text })
      commit('addValue', value)
    },
    async updateText({ commit }, { value, text }) {
      await api.valuesByIdPut(value.id, { text })
      commit('updateText', { value, text })
    },
    async deleteValue({ commit }, id) {
      await api.valuesByIdDelete(id)
      commit('deleteValue', id)
    }
  }
}
